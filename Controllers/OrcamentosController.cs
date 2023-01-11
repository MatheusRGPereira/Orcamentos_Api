using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodigoDoFuturoApi.Context;
using CodigoDoFuturoApi.Models;
using CodigoDoFuturoApi.Dtos;
using CodigoDoFuturoApi.Servicos;

namespace CodigoDoFuturoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrcamentosController : ControllerBase
    {
        private readonly DBContext _context;

        public OrcamentosController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Orcamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orcamento>>> GetOrcamentos()
        {
            var orcamentos = _context.Orcamentos.Include(p => p.PessoaFisica)
                .Include(j => j.PessoaJuridica);
            await orcamentos.ToListAsync();
            return StatusCode(200, orcamentos);

        }

        // GET: api/Orcamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orcamento>> GetOrcamento(int id)
        {
          if (_context.Orcamentos == null)
          {
              return NotFound();
          }
            var orcamento = await _context.Orcamentos.FindAsync(id);

            if (orcamento == null)
            {
                return NotFound();
            }

            return orcamento;
        }

        // PUT: api/Orcamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrcamento(int id, Orcamento orcamento)
        {
            if (id != orcamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(orcamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrcamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orcamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orcamento>> PostOrcamento(OrcamentoDto orcamentoDto)
        {
            var orcamento = BuilderService<Orcamento>.Builder(orcamentoDto);
            var dataAtual = DateTime.Now;
            orcamento.DataCriacao = dataAtual;
            orcamento.DataExpiracao = dataAtual.AddMonths(3);

            var cliente = _context.PessoasFisicas.Find(orcamentoDto.ClienteId);
            var fornecedor = _context.PessoasJuridicas.Find(orcamentoDto.FornecedorId);
            Console.WriteLine($"========================{cliente}============================================");
            Console.WriteLine($"========================{fornecedor}============================================");
            orcamento.PessoaFisica = cliente;
            orcamento.PessoaJuridica = fornecedor;
            _context.Orcamentos.Add(orcamento);
            await _context.SaveChangesAsync();

            return StatusCode(204, orcamento);
        }

        // DELETE: api/Orcamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrcamento(int id)
        {
            if (_context.Orcamentos == null)
            {
                return NotFound();
            }
            var orcamento = await _context.Orcamentos.FindAsync(id);
            if (orcamento == null)
            {
                return NotFound();
            }

            _context.Orcamentos.Remove(orcamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrcamentoExists(int id)
        {
            return (_context.Orcamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
