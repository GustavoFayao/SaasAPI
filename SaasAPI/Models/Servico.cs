namespace SaasAPI.Models
{
    public class Servico
    {
        public Guid Id { get; set; }
        public Guid BarbeariaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int DuracaoMin { get; set; }
        public decimal Preco { get; set; }

        // Relacionamentos
        public Barbearia Barbearia { get; set; }
        public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

    }
}
