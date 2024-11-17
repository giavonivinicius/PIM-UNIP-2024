using System;

namespace PimUrbanGreen.Models
{
    public class ItemPedidoModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty; // Relacionado ao usuário que fez o pedido
        public string Produto { get; set; } = string.Empty; // Nome do produto escolhido
        public int Quantidade { get; set; } // Quantidade do produto pedido
        public DateTime DataPedido { get; set; } = DateTime.Now; // Data do pedido
    }
}

