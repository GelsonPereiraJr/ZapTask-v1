using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ZapTask.Infrastructure.Services
{
    public class WhatsAppService
    {
        private readonly HttpClient _httpClient;
        private readonly string _phoneNumberId;
        private readonly string _accessToken;

        public WhatsAppService(HttpClient httpClient, string phoneNumberId, string accessToken)
        {
            _httpClient = httpClient;
            _phoneNumberId = phoneNumberId;
            _accessToken = accessToken;
        }

        /// Envia uma mensagem de texto simples
        public async Task EnviarMensagemSimplesAsync(string whatsappId, string texto)
        {
            var payload = new
            {
                messaging_product = "whatsapp",
                to = whatsappId,
                type = "text",
                text = new { body = texto }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://graph.facebook.com/v17.0/{_phoneNumberId}/messages");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");
            request.Content = JsonContent.Create(payload);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        /// Envia uma mensagem interativa com botões para tarefas
        public async Task EnviarTarefaAsync(string whatsappId, string titulo, DateTime prazo, int tentativas)
        {
            var payload = new
            {
                messaging_product = "whatsapp",
                to = whatsappId,
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    body = new { text = $"🔔 Tarefa: {titulo}\n⏰ Prazo: {prazo:HH:mm}\nTentativas: {tentativas}" },
                    action = new
                    {
                        buttons = new[]
                        {
                            new { type = "reply", reply = new { id = "concluir", title = "✅ Concluir" } },
                            new { type = "reply", reply = new { id = "adiar", title = "⏸ Adiar 30min" } },
                            new { type = "reply", reply = new { id = "remarcar", title = "📅 Remarcar" } }
                        }
                    }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://graph.facebook.com/v17.0/{_phoneNumberId}/messages");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");
            request.Content = JsonContent.Create(payload);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
