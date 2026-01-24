using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Localization;
using ZapTask.Application.DTOs;
using ZapTask.Application.UseCases;

namespace ZapTask.API.Controller
{
    [ApiController]
    [Route("tarefas")]
    public class TarefaController : ControllerBase
    {
         private readonly CriarTarefaUseCase _useCase;

         public TarefaController(CriarTarefaUseCase useCase)
        {
             _useCase = useCase;
        }
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarTarefaDto dto)
       {
        var tarefaId = await _useCase.ExecutarAsync(dto);
        return CreatedAtAction(nameof(Criar), new { id = tarefaId }, new { tarefaId });
       }

    }
}