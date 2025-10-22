using BarbeariaAPI.Data;
using Microsoft.EntityFrameworkCore;
using SaasAPI.DTos;
using SaasAPI.Models;

namespace SaasAPI.Services
{
    public class AgendamentoService
    {
        private readonly BarbeariaContext _context;

        public AgendamentoService(BarbeariaContext context)
        {
            _context = context;
        }

        // Pega todos os agendamentos de uma barbearia
        public async Task<List<Agendamento>> GetAgendamentosAsync(Guid barbeariaId)
        {
            return await _context.Agendamentos
                .Where(a => a.BarbeariaId == barbeariaId)
                .ToListAsync();
        }

        // Cria um novo agendamento
        public async Task<Agendamento> CriaAgendamentoAsync(Guid barbeariaId, AgendamentoDTO dto)
        {


            var agendamento = new Agendamento
            {
                Id = Guid.NewGuid(),
                BarbeariaId = barbeariaId,
                ClienteId = dto.ClienteId,
                ServicoId = dto.ServicoId,
                BarbeiroId = dto.BarbeiroId,
                Inicio = dto.Inicio,
                Fim = dto.Fim,
                Status = dto.Status
            };

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
            return agendamento;

        }
    }
}
