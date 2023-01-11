using CodigoDoFuturoApi.Context;
using CodigoDoFuturoApi.Dtos;
using CodigoDoFuturoApi.Models;
using CodigoDoFuturoApi.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("")]
        public IActionResult GetAll()
        {
            var pesoasFisicas = _context.PessoasFisicas.ToList();
            return StatusCode(200, pesoasFisicas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var pessoaFisica = _context.PessoasFisicas.AsQueryable().Where(p => p.Id == id);
            return StatusCode(200, pessoaFisica);
        }


        [HttpPost("")]
        public IActionResult Create([FromBody] PessoaFIsicaDto pessoaFisicaDto)
        {
            var pessoaFisica = BuilderService<PessoaFisica>.Builder(pessoaFisicaDto);
            pessoaFisica.DataCriacao = DateTime.Now;
            _context.Add(pessoaFisica);
            _context.SaveChanges();
            return StatusCode(201, pessoaFisica);
        }

        [HttpPut]
        public IActionResult Update(int id ,[FromBody] PessoaFisica pessoaFisica)
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
            _context.SaveChanges();
            return StatusCode(204);
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            var pessoaFisica = _context.PessoasFisicas.Find(id);
            if (pessoaFisica is null)
            {
                return StatusCode(404);
            }
            _context.PessoasFisicas.Remove(pessoaFisica);
            _context.SaveChanges();
            return StatusCode(204);
        }

    }


}
