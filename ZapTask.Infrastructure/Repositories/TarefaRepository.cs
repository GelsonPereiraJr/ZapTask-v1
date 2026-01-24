
using ZapTask.Application.Interfaces;
using ZapTask.Domain.Entities;

namespace ZapTask.Infrastructure.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private static readonly List<Tarefa> _tarefas = new();

        public Task SalvarAsync(Tarefa tarefa)
        {
            _tarefas.Add(tarefa);
            return Task.CompletedTask;
        }
    }
}