using BarbeariaAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasAPI.DTos;
using SaasAPI.Models;

namespace SaasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentosController : ControllerBase
    {
        private readonly BarbeariaContext _context;
        private readonly ILogger<AgendamentosController> _logger;

        public AgendamentosController(BarbeariaContext context, ILogger<AgendamentosController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("{barbeariaId}")]
        public async Task<ActionResult<List<AgendamentoDTO>>> GetAgendamentosAsync(Guid barbeariaId)
        {
            var agendamentos = await _context.Agendamentos
                .Where(a => a.BarbeariaId == barbeariaId)
                .Include(a => a.Barbearia)
                .Include(a => a.Cliente)
                .Include(a => a.Servicos)
                .Include(a => a.Barbeiro)
                .Select(a => new AgendamentoDTO
                {
                    BarbeariaId = a.BarbeariaId,
                    ClienteId = a.ClienteId,
                    ServicoId = a.ServicoId,
                    BarbeiroId = a.BarbeiroId,
                    NomeCliente = a.Cliente.Nome,
                    NomeBarbeiro = a.Barbeiro.Nome,
                    NomeServico = a.Servicos.Nome,
                    NomeBarbearia = a.Barbearia.Nome,
                    Inicio = a.Inicio,
                    Fim = a.Fim,
                    Status = a.Status
                })
                .ToListAsync();

            if (agendamentos == null || agendamentos.Count == 0)
                return NotFound("Nenhum agendamento encontrado para essa barbearia.");

            return Ok(agendamentos);
        }


        [HttpPost("{barbeariaId}")]
        public async Task<ActionResult<Agendamento>> CriaAgendamentoAsync(Guid barbeariaId, [FromBody] AgendamentoDTO dto)
        {
            try
            {
                _logger.LogInformation("Iniciando criação de agendamento para barbearia {BarbeariaId}", barbeariaId);

                bool barbeariaExiste = await _context.Barbearia.AnyAsync(b => b.Id == barbeariaId);
                if (!barbeariaExiste)
                    return NotFound($"Barbearia com ID {barbeariaId} não encontrada.");

                bool barbeiroOcupado = await _context.Agendamentos.AnyAsync(a =>
                    a.BarbeariaId == barbeariaId &&
                    a.BarbeiroId == dto.BarbeiroId &&
                    a.Status != StatusAgendamento.Cancelado &&
                    (
                        (dto.Inicio >= a.Inicio && dto.Inicio < a.Fim) ||
                        (dto.Fim > a.Inicio && dto.Inicio <= a.Fim) ||
                        (dto.Inicio <= a.Inicio && dto.Fim >= a.Fim)
                    )
                );

                if (barbeiroOcupado)
                    return Conflict("O barbeiro já possui um agendamento nesse horário.");

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

                _logger.LogInformation("Agendamento criado com sucesso: {AgendamentoId}", agendamento.Id);

                return CreatedAtAction(nameof(GetAgendamentosAsync), new { barbeariaId = agendamento.BarbeariaId }, agendamento);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao salvar o agendamento no banco de dados");
                return BadRequest("Erro ao salvar agendamento no banco de dados. Verifique se as chaves estrangeiras estão corretas.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar o agendamento");
                return StatusCode(500, "Erro interno no servidor.");
            }
        }
    }
}
