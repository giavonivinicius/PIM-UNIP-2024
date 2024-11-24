using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimUrbanGreen.Models
{
    public class ProdutoModel
    {
        [Key] 
        public string NomeProdutoAcabado { get; set; } = string.Empty;
        public decimal PrecoUnitario { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
