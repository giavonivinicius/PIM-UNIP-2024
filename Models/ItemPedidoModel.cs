using System;

namespace PimUrbanGreen.Models
{
    public class ItemPedidoModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty; 
        public string Produto { get; set; } = string.Empty; 
        public int Quantidade { get; set; } 
        public DateTime DataPedido { get; set; } = DateTime.Now; 
    }
}

