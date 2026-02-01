using Microsoft.AspNetCore.Mvc;

namespace ZapTask.API.Controllers
{
    
    [ApiController]
    [Route("webhook/whatsapp")]
    public class WhatsAppWebhookController : ControllerBase
    {
       private const string VERIFY_TOKEN = "zaptask_verify_token";

     // Meta chama esse endpoint pra validar o webhook
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

           // Aqui chegam mensagens e eventos
      [HttpPost]
         public IActionResult Receive([FromBody] object payload)
        {
            Console.WriteLine(payload.ToString());
            return Ok();
        }
    }
}