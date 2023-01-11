using System.ComponentModel.DataAnnotations.Schema;

namespace CodigoDoFuturoApi.Models
{
    [Table("tb_pessoa_juridica")]
    public class PessoaJuridica 
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Cnpj { get; set; }

        public DateTime? DataCriacao { get; set; }
    }
}
