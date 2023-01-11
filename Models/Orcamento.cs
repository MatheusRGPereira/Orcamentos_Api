using System.ComponentModel.DataAnnotations.Schema;

namespace CodigoDoFuturoApi.Models
{
    [Table("tb_Orcamentos")]
    public class Orcamento
    {
        public int Id { get; set; }

        [ForeignKey("PessoaFisica")]
        public int ClienteId { get; set; }
        public PessoaFisica PessoaFisica { get; set; }

        [ForeignKey("PessoaJuridica")]
        public int FornecedorId { get; set; }
        public PessoaJuridica PessoaJuridica { get; set; }

        public string DescricaoDoServico { get; set;}

        public double ValorTotal { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataExpiracao { get; set; }
    }
}
