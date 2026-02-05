using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZapTask.Domain.Entities;


namespace ZapTask.Application.Interfaces
{
   public interface ITarefaRepository
    {
        Task SalvarAsync(Tarefa tarefa);
        Task AtualizarAsync(Tarefa tarefa);
        Task AdicionarAsync(Tarefa tarefa);
        Task<Tarefa> BuscarPorIdAsync(Guid id);
        Task<List<Tarefa>> ObterPendentesAsync();
        Task<Tarefa> ObterPorIdAsync(Guid id);
    } 
}