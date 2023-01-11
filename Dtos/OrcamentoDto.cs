using System.Text.Json.Serialization;
using CodigoDoFuturoApi.Models;

namespace CodigoDoFuturoApi.Dtos
{
    public class OrcamentoDto
    { 
        public int ClienteId { get; set; }

        public int FornecedorId{ get; set; }

        public string DescricaoDoServico { get; set; }

        public double ValorTotal { get; set; }

    }
}
