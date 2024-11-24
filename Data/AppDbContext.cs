using Microsoft.EntityFrameworkCore;
using PimUrbanGreen.Models;

namespace PimUrbanGreen.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<PedidoWebModel> PedidoWeb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserModel
            modelBuilder.Entity<UserModel>()
                .ToTable("Usuarios")
                .HasKey(u => u.NomeUsuario); 

            // ProdutoModel
            modelBuilder.Entity<ProdutoModel>()
                .ToTable("ProdutoAcabado")
                .HasKey(p => p.NomeProdutoAcabado);

            // PedidoWebModel
            modelBuilder.Entity<PedidoWebModel>()
                .ToTable("PedidoWeb")
                .HasKey(p => new { p.Produto, p.UsuarioPedido });

            modelBuilder.Entity<PedidoWebModel>()
                .Property(p => p.UsuarioPedido)
                .HasColumnName("UsuarioPedido");
        }
    }
}
