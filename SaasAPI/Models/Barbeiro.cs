namespace SaasAPI.Models
{
    public class Barbeiro
    {

        public Guid Id { get; set; }
        public Guid BarbeariaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefone { get; set; }

        // Relacionamentos
        public Barbearia Barbearia { get; set; }
        public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

    }
}
