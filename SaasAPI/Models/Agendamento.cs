namespace SaasAPI.Models
{

    public enum StatusAgendamento
    {
        Agendado = 1,
        Cancelado = 2,
        Concluido = 3
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
        public StatusAgendamento Status { get; set; } = StatusAgendamento.Agendado;

        // Relacionamentos
        public Barbearia Barbearia { get; set; }
        public Cliente Cliente { get; set; }
        public Servico Servicos { get; set; }
        public Barbeiro Barbeiro { get; set; }

    }
}
