namespace SaasAPI.Models
{

    public enum StatusAgendamento
    {
        Confirmado,
        Cancelado,
        Finalizado
    }

    public class Agendamento
    {
        public Guid Id { get; set; }
        public Guid BarbeariaId { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ServicoId { get; set; }
        public Guid BarbeiroId { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public StatusAgendamento Status { get; set; } = StatusAgendamento.Confirmado;

        // Relacionamentos
        public Barbearia Barbearia { get; set; }
        public Cliente Cliente { get; set; }
        public Servico Servico { get; set; }
        public Barbeiro Barbeiro { get; set; }

    }
}
