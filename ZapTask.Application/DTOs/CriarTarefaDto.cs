using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZapTask.Application.DTOs
{
    public class CriarTarefaDto
    {
        public string Titulo { get; set; }
        public DateTime Prazo { get; set; }
        public string WhatsAppId { get; set; }
    }
}