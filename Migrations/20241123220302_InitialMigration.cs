using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PimUrbanGreen.Migrations
{
    public partial class InitialMigration : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoWeb");

            migrationBuilder.DropTable(
                name: "ProdutoAcabado");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
