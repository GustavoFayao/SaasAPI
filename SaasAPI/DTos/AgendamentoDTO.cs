using SaasAPI.Models;

namespace SaasAPI.DTos
{
    public class AgendamentoDTO
    {
        public Guid ClienteId { get; set; }
        public Guid BarbeiroId { get; set; }
        public Guid ServicoId { get; set; }
        public Guid BarbeariaId { get; set; }

        public string NomeCliente { get; set; } = string.Empty;
        public string NomeBarbeiro { get; set; } = string.Empty;
        public string NomeServico { get; set; } = string.Empty;
        public string NomeBarbearia { get; set; } = string.Empty;
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public StatusAgendamento Status { get; set; } = StatusAgendamento.Agendado;
    }
}
