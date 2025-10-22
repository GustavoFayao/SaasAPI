namespace SaasAPI.Models
{
    public class Barbearia
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string Plano { get; set; } = "free"; // default

        // Relacionamentos
        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
        public ICollection<Servico> Servicos { get; set; } = new List<Servico>();
        public ICollection<Barbeiro> Barbeiros { get; set; } = new List<Barbeiro>();
        public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }
}
