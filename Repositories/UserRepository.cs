using PimUrbanGreen.Data;
using PimUrbanGreen.Models;
using System.Linq;

namespace PimUrbanGreen.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public UserModel? GetUserByCredentials(string usuario, string senha)
        {
            return _context.Users.FirstOrDefault(u => u.Usuario == usuario && u.Senha == senha);
        }
    }
}
