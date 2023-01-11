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
    [Route("[controller]")]
    [ApiController]
    public class PessoaJuridicaController : ControllerBase
    {
        private readonly DBContext _context;

        public PessoaJuridicaController(DBContext context)
        {
            _context = context;
        }

        // GET: api/PessoaJuridica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaJuridica>>> GetPessoasJuridicas()
        {
          if (_context.PessoasJuridicas == null)
          {
              return NotFound();
          }
            return await _context.PessoasJuridicas.ToListAsync();
        }

        // GET: api/PessoaJuridica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaJuridica>> GetPessoaJuridica(int id)
        {
          if (_context.PessoasJuridicas == null)
          {
              return NotFound();
          }
            var pessoaJuridica = await _context.PessoasJuridicas.FindAsync(id);

            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            return pessoaJuridica;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoaJuridica(int id, PessoaJuridica pessoaJuridica)
        {
            if (id != pessoaJuridica.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoaJuridica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaJuridicaExists(id))
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

        [HttpPost]
        public async Task<ActionResult<PessoaJuridica>> PostPessoaJuridica(PessoaJuridicaDto pessoaJuridicaDto)
        {
            var pessoaJuridica = BuilderService<PessoaJuridica>.Builder(pessoaJuridicaDto);
            pessoaJuridica.DataCriacao = DateTime.Now;
            _context.PessoasJuridicas.Add(pessoaJuridica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPessoaJuridica", new { id = pessoaJuridica.Id }, pessoaJuridica);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoaJuridica(int id)
        {
            if (_context.PessoasJuridicas == null)
            {
                return NotFound();
            }
            var pessoaJuridica = await _context.PessoasJuridicas.FindAsync(id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            _context.PessoasJuridicas.Remove(pessoaJuridica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaJuridicaExists(int id)
        {
            return (_context.PessoasJuridicas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
