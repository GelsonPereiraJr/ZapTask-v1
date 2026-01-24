
using ZapTask.Application.Interfaces;
using ZapTask.Domain.Entities;
using ZapTask.Application.DTOs;

namespace ZapTask.Application.UseCases
{
    public class CriarTarefaUseCase
    {
        private readonly ITarefaRepository _repository;

        public CriarTarefaUseCase(ITarefaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> ExecutarAsync(CriarTarefaDto dto)
        {
            var tarefa = new Tarefa(
                dto.Titulo,
                dto.Prazo.ToLocalTime(),
                dto.WhatsAppId
            );
          
              await _repository.SalvarAsync(tarefa);
              return tarefa.Id;
        }
    }
}