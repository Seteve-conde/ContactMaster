using Microsoft.EntityFrameworkCore.Migrations;

namespace Dados.Migrations
{
    public partial class bonusModelAtualização : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "BonusModels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BonusModels_UsuarioId",
                table: "BonusModels",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_BonusModels_Usuarios_UsuarioId",
                table: "BonusModels",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonusModels_Usuarios_UsuarioId",
                table: "BonusModels");

            migrationBuilder.DropIndex(
                name: "IX_BonusModels_UsuarioId",
                table: "BonusModels");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "BonusModels");
        }
    }
}
