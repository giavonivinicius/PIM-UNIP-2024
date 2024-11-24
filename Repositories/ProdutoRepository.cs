using PimUrbanGreen.Data;
using PimUrbanGreen.Models;
using System.Collections.Generic;
using System.Linq;

namespace PimUrbanGreen.Repositories
{
    public class ProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<ProdutoModel> GetAllProdutos()
        {
            return _context.Produtos.ToList();
        }
    }
}
