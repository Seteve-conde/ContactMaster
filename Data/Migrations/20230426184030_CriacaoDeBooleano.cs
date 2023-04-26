using Microsoft.EntityFrameworkCore.Migrations;

namespace Dados.Migrations
{
    public partial class CriacaoDeBooleano : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Selecionado",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selecionado",
                table: "Usuarios");
        }
    }
}
