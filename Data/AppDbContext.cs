using Microsoft.EntityFrameworkCore;
using PimUrbanGreen.Models;

namespace PimUrbanGreen.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<ItemPedidoModel> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da tabela Usuarios
            modelBuilder.Entity<UserModel>()
                .ToTable("Usuarios");

            // Configuração da tabela ProdutoAcabado
            modelBuilder.Entity<ProdutoModel>()
                .ToTable("ProdutoAcabado")
                .Property(p => p.PrecoUnitario)
                .HasColumnType("decimal(18,2)"); // precisão e escala para a coluna Preco

            // Configuração da tabela ItensPedido
            modelBuilder.Entity<ItemPedidoModel>()
                .ToTable("ItensPedido");
        }
    }
}
