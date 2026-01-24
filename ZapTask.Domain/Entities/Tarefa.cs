using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

using ZapTask.Domain.Enums;

namespace ZapTask.Domain.Entities
{
    public class Tarefa
    {
        public Guid Id { get;  private set; }
        public string Titulo { get; private set; }
        public DateTime Prazo { get; private set; }
        public StatusTarefa Status { get; private set; }
        public int Tentativa { get; private set; }
        public string WhatsAppId { get; private set; }

        public Tarefa(string titulo,DateTime prazo, string whatsAppId)
        {
            Id = Guid.NewGuid();
        Titulo = titulo;
        Prazo = prazo;
        WhatsAppId = whatsAppId;
        Status = StatusTarefa.Pendente;
        Tentativa = 0;
        }

        

    }
}