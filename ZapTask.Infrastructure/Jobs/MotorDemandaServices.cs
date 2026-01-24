using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ZapTask.Infrastructure.Jobs
{
    public class MotorDemandasService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MotorDemandasService> _logger;

        public MotorDemandasService(
            IServiceScopeFactory scopeFactory,
            ILogger<MotorDemandasService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("🔥 Motor de Demandas iniciado");

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider
                    .GetRequiredService<ZapTask.Application.Interfaces.ITarefaRepository>();

                var tarefas = await repository.ObterPendentesAsync();

                foreach (var tarefa in tarefas)
                {
                    if (tarefa.EstaVencida())
                    {
                        tarefa.RegistrarTentativa();

                        _logger.LogInformation(
                            "🔔 Tarefa vencida: {Titulo} | Tentativas: {Tentativas}",
                            tarefa.Titulo,
                            tarefa.Tentativas
                        );
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
