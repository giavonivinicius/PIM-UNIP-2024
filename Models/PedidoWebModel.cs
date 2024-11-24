using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimUrbanGreen.Models
{
    [Table("PedidoWeb")]
    public class PedidoWebModel
    {
        public string Produto { get; set; } = string.Empty;

        public int Quantidade { get; set; }

        public string UsuarioPedido { get; set; } = string.Empty;

        public DateTime DataPedido { get; set; } = DateTime.Now;
    }
}
