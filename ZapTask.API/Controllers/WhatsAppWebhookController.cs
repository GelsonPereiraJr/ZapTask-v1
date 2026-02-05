using Microsoft.AspNetCore.Mvc;
using ZapTask.API.Models;
using ZapTask.Application.Commands.CriarTarefa;
using ZapTask.Infrastructure.Services;

namespace ZapTask.API.Controllers
{
    [ApiController]
    [Route("webhook/whatsapp")]
    public class WhatsAppWebhookController : ControllerBase
    {
        private const string VERIFY_TOKEN = "zaptask_verify_token";

        // GET usado pelo WhatsApp para verificar o webhook
        [HttpGet]
        public IActionResult Verify(
            [FromQuery(Name = "hub.mode")] string mode,
            [FromQuery(Name = "hub.verify_token")] string token,
            [FromQuery(Name = "hub.challenge")] string challenge)
        {
            if (mode == "subscribe" && token == VERIFY_TOKEN)
            {
                return Ok(challenge);
            }

            return Unauthorized();
        }

        // POST para receber mensagens do WhatsApp
        [HttpPost]
        public async Task<IActionResult> Receive(
            [FromBody] WhatsAppWebhookPayload payload,
            [FromServices] CriarTarefaCommandHandler criarTarefaHandler,
            [FromServices] WhatsAppService whatsAppService)
        {
            var entry = payload.Entry?.FirstOrDefault();
            var change = entry?.Changes?.FirstOrDefault();
            var value = change?.Value;
            var message = value?.Messages?.FirstOrDefault();

            if (message?.Text?.Body != null)
            {
                var texto = message.Text.Body;
                var from = message.From;

                if (texto.StartsWith("Criar tarefa", StringComparison.OrdinalIgnoreCase))
                {
                    var titulo = texto.Replace("Criar tarefa", "").Trim();

                    var command = new CriarTarefaCommand(titulo, from);
                    var tarefaId = await criarTarefaHandler.HandleAsync(command);

                    Console.WriteLine($"Tarefa criada: {tarefaId}");

                    // Envia notificação de criação de tarefa
                    await whatsAppService.EnviarTarefaAsync(
                        from!,
                        titulo,
                        DateTime.UtcNow.AddHours(1), // Ajuste o prazo conforme necessário
                        0
                    );
                }
                else
                {
                    // Responde mensagens simples
                    await whatsAppService.EnviarMensagemSimplesAsync(
                        from!,
                        $"Recebemos sua mensagem: {texto}"
                    );
                }
            }

            // Sempre retornar 200 OK para o WhatsApp
            return Ok();
        }
    }
}
