using PimUrbanGreen.Data;
using PimUrbanGreen.Models;
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

        public void AddPedidoWeb(PedidoWebModel item)
        {
            _context.PedidoWeb.Add(item);
            _context.SaveChanges();
        }

        public List<PedidoWebModel> GetAllPedidoWeb()
        {
            return _context.PedidoWeb.ToList();
        }
    }
}
