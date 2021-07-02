using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecDesp.Migrations
{
    public partial class Cobranca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cobrancas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    valor = table.Column<double>(type: "double", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fromAreaId = table.Column<long>(type: "bigint", nullable: true),
                    toAreaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobrancas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cobrancas_Areas_fromAreaId",
                        column: x => x.fromAreaId,
                        principalTable: "Areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cobrancas_Areas_toAreaId",
                        column: x => x.toAreaId,
                        principalTable: "Areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Cobrancas_fromAreaId",
                table: "Cobrancas",
                column: "fromAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cobrancas_toAreaId",
                table: "Cobrancas",
                column: "toAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cobrancas");
        }
    }
}
