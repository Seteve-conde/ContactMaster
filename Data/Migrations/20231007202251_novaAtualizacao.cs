using Microsoft.EntityFrameworkCore.Migrations;

namespace Dados.Migrations
{
    public partial class novaAtualizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonusModels_Usuarios_UsuarioId",
                table: "BonusModels");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "BonusModels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BonusModels_Usuarios_UsuarioId",
                table: "BonusModels",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonusModels_Usuarios_UsuarioId",
                table: "BonusModels");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "BonusModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BonusModels_Usuarios_UsuarioId",
                table: "BonusModels",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
