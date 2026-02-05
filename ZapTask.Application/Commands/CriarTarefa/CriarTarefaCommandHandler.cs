using ZapTask.Application.Interfaces;
using ZapTask.Domain.Entities;

namespace ZapTask.Application.Commands.CriarTarefa
{
    public class CriarTarefaCommandHandler
    {
        private readonly ITarefaRepository _repository;

        public CriarTarefaCommandHandler(ITarefaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> HandleAsync(CriarTarefaCommand command)
        {
            var tarefa = new Tarefa(
                command.Titulo,
                DateTime.UtcNow.AddHours(1), // prazo inicial padrão
                command.WhatsAppId
            );

            await _repository.AdicionarAsync(tarefa);

            return tarefa.Id;
        }
    }
}
