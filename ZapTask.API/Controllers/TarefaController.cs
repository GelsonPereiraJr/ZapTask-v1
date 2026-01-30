using Microsoft.AspNetCore.Mvc;
using ZapTask.Application.DTOs;
using ZapTask.Application.UseCases;

namespace ZapTask.API.Controller
{
    [ApiController]
    [Route("tarefas")]
    public class TarefaController : ControllerBase
    {
          private readonly CriarTarefaUseCase _criarUseCase;
          private readonly ConcluirTarefaUseCase _concluirUseCase;

          public TarefaController(CriarTarefaUseCase criarUseCase,
               ConcluirTarefaUseCase concluirUseCase)
          {
                 _criarUseCase = criarUseCase;
                 _concluirUseCase = concluirUseCase;
          }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarTarefaDto dto)
        {
           var id = await _criarUseCase.ExecutarAsync(dto);
            return Ok(new { id });
        }

        [HttpPost("{id}/concluir")]
        public async Task<IActionResult> Concluir(Guid id)
        {
             await _concluirUseCase.ExecutarAsync(id);
             return NoContent();
        }
    }
}