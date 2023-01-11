using System.ComponentModel.DataAnnotations.Schema;

namespace CodigoDoFuturoApi.Models
{

    [Table("tb_pessoa_fisica")]
    public class PessoaFisica
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Cpf { get; set; }

        public DateTime? DataCriacao { get; set; }

    }
}
