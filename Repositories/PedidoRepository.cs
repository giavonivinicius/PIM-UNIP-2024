using PimUrbanGreen.Data;
using PimUrbanGreen.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PimUrbanGreen.Repositories
{
    public class PedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddItemPedido(ItemPedidoModel item)
        {
            _context.ItensPedido.Add(item); 
            _context.SaveChanges();
        }

        public List<ItemPedidoModel> GetAllItensPedidos()
        {
            return _context.ItensPedido.ToList();
        }
    }
}
