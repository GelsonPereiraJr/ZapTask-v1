using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZapTask.Application.Interfaces;

namespace ZapTask.Application.UseCases
{
    public class ConcluirTarefaUseCase
    {
       private readonly ITarefaRepository _repository;

       public ConcluirTarefaUseCase(ITarefaRepository repository)
       {
        _repository = repository;
       }

        public async Task ExecutarAsync(Guid id)
        {
        var tarefa = await _repository.ObterPorIdAsync(id);

        if (tarefa is null)
            throw new Exception("Tarefa não encontrada");

        tarefa.Concluir();

        await _repository.AtualizarAsync(tarefa);
        }      
    }   
}