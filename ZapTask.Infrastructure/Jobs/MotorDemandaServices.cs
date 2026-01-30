using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZapTask.Application.Interfaces;

namespace ZapTask.Infrastructure.Jobs
{

  namespace ZapTask.Infrastructure.Jobs
{
    public class MotorDemandaServices : BackgroundService
    {
        private readonly ILogger<MotorDemandaServices> _logger;
        private readonly ITarefaRepository _repository;

        public MotorDemandaServices(
            ITarefaRepository repository,
            ILogger<MotorDemandaServices> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("🔥 Motor de Demandas iniciado");

            while (!stoppingToken.IsCancellationRequested)
            {
                var tarefas = await _repository.ObterPendentesAsync();

                foreach (var tarefa in tarefas)
                {
                    if (!tarefa.PodeInsistir(DateTime.UtcNow))
                        continue;

                    _logger.LogInformation(
                        "🔔 Enviando alerta: {Titulo} | Tentativas: {Tentativas}",
                        tarefa.Titulo,
                        tarefa.Tentativas + 1
                    );

                    tarefa.RegistrarInsistencia();
                    await _repository.AtualizarAsync(tarefa);
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}

}
