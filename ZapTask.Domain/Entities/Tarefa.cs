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
        public Guid Id { get; private set; }

        public string Titulo { get; private set; } = string.Empty;
        public string WhatsAppId { get; private set; } = string.Empty;

        public DateTime Prazo { get; private set; }
        public StatusTarefa Status { get; private set; }
        public int Tentativas { get; private set; }

        protected Tarefa() { } 

        public Tarefa(string titulo, DateTime prazo, string whatsAppId)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título é obrigatório", nameof(titulo));

            if (string.IsNullOrWhiteSpace(whatsAppId))
                throw new ArgumentException("WhatsAppId é obrigatório", nameof(whatsAppId));

            Id = Guid.NewGuid();
            Titulo = titulo;
            Prazo = prazo;
            WhatsAppId = whatsAppId;
            Status = StatusTarefa.Pendente;
            Tentativas = 0;
        }

        public bool EstaVencida()
            => DateTime.Now >= Prazo && Status == StatusTarefa.Pendente;

        public void RegistrarTentativa()
            => Tentativas++;
    }
}