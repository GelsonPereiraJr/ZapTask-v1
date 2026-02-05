using ZapTask.Application.Interfaces;
using ZapTask.Domain.Entities;
using ZapTask.Domain.Enums;
using ZapTask.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;




namespace ZapTask.Infrastructure.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly ZapTaskDbContext _context;

        public TarefaRepository(ZapTaskDbContext context)
        {
            _context = context;
        }
        
        public async Task AdicionarAsync(Tarefa tarefa)
        {
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
        }

        public async Task SalvarAsync(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Tarefa>> ObterPendentesAsync()
        {
         return await _context.Tarefas
            .Where(t => t.Status != Tarefa.StatusTarefa.Concluida)
            .ToListAsync();
        }
        
        public async Task AtualizarAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task<Tarefa?> BuscarPorIdAsync(Guid id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<Tarefa?> ObterPorIdAsync(Guid id)
        {
            return await _context.Tarefas.FindAsync(id);
        }
    }
}
