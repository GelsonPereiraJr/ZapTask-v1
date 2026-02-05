
namespace ZapTask.Application.Commands.CriarTarefa
{
    public class CriarTarefaCommand
    {
        public string Titulo { get; }
        public string WhatsAppId { get; }

        public CriarTarefaCommand(string titulo, string whatsAppId)
        {
            Titulo = titulo;
            WhatsAppId = whatsAppId;
        }
    }
}

