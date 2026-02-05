using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZapTask.Application.Interfaces;
using ZapTask.Infrastructure.Services;

namespace ZapTask.Infrastructure.Jobs
{
    public class MotorDemandaServices : BackgroundService
    {
        private readonly ILogger<MotorDemandaServices> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly WhatsAppService _whatsAppService;

        public MotorDemandaServices(
            ILogger<MotorDemandaServices> logger,
            IServiceScopeFactory scopeFactory,
            WhatsAppService whatsAppService)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _whatsAppService = whatsAppService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Motor de Demandas iniciado");

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ITarefaRepository>();

                var tarefas = await repository.ObterPendentesAsync();

                foreach (var tarefa in tarefas)
                {
                    if (!tarefa.PodeInsistir(DateTime.UtcNow))
                        continue;

                    tarefa.RegistrarInsistencia();
                    await repository.AtualizarAsync(tarefa);
                   try
                   {
                       await _whatsAppService.EnviarTarefaAsync(
                          tarefa.WhatsAppId,
                          tarefa.Titulo,
                          tarefa.Prazo,
                          tarefa.Tentativas
                        );
                           _logger.LogInformation("Mensagem enviada via WhatsApp para: {Titulo}", tarefa.Titulo);
                   }
                   catch (Exception ex)
                   {
                          _logger.LogError(ex, "Falha ao enviar WhatsApp para: {Titulo}", tarefa.Titulo);
                   }

                }
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
