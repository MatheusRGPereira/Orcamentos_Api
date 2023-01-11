using CodigoDoFuturoApi.Context;
using CodigoDoFuturoApi.Dtos;
using CodigoDoFuturoApi.Models;
using CodigoDoFuturoApi.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodigoDoFuturoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PessoaFIsicaController : ControllerBase
    {

        private readonly DBContext _context;

        public PessoaFIsicaController(DBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaFisica>>> GetAll()
        {
            var pesoasFisicas = await _context.PessoasFisicas.ToListAsync();
            return StatusCode(200, pesoasFisicas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaFisica>> GetById([FromRoute] int id)
        {
            var pessoaFisica = await _context.PessoasFisicas.FindAsync(id);
            return StatusCode(200, pessoaFisica);
        }


        [HttpPost("")]
        public async Task<ActionResult<PessoaFisica>> Create([FromBody] PessoaFIsicaDto pessoaFisicaDto)
        {
            var pessoaFisica = BuilderService<PessoaFisica>.Builder(pessoaFisicaDto);
            pessoaFisica.DataCriacao = DateTime.Now;
            _context.Add(pessoaFisica);
            await  _context.SaveChangesAsync();
            return StatusCode(201, pessoaFisica);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id ,[FromBody] PessoaFisica pessoaFisica)
        {
            if(id != pessoaFisica.Id)
            {
                return StatusCode(400, new{
                    Mensagem = "O id do Cliente precisa bater com o id da URL"
                });
            }
            if(pessoaFisica is null)
            {
                return StatusCode(404, new
                {
                    Mensagem = "O cliente informado nao existe"
                });
            }
            _context.PessoasFisicas.Update(pessoaFisica);
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(int id)
        {
            var pessoaFisica = await _context.PessoasFisicas.FindAsync(id);
            if (pessoaFisica is null)
            {
                return StatusCode(404);
            }
            _context.PessoasFisicas.Remove(pessoaFisica);
           await _context.SaveChangesAsync();
            return StatusCode(204);
        }

    }


}
