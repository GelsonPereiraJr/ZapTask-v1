
using ZapTask.Application.Interfaces;
using ZapTask.Domain.Entities;

namespace ZapTask.Infrastructure.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private static readonly List<Tarefa> _tarefas = new();

        public Task<List<Tarefa>> ObterPendentesAsync()
        {
           var pendentes = _tarefas
                .Where(t => t.Status == Domain.Enums.StatusTarefa.Pendente)
                .ToList();

            return Task.FromResult(pendentes);
        }

        public Task SalvarAsync(Tarefa tarefa)
        {
            _tarefas.Add(tarefa);
            return Task.CompletedTask;
        }
    }
}