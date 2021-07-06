using Microsoft.EntityFrameworkCore.Migrations;

namespace RecDesp.Migrations
{
    public partial class Ajustes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "Transacoes",
                newName: "Descricao");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_AreaId",
                table: "Transacoes",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Areas_AreaId",
                table: "Transacoes",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Areas_AreaId",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_AreaId",
                table: "Transacoes");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Transacoes",
                newName: "descricao");
        }
    }
}
