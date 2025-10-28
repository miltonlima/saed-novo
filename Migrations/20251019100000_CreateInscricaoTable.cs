using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    public partial class CreateInscricaoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inscricao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    DataInscricao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false, defaultValue: "Ativa")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inscricao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inscricao_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inscricao_turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_inscricao_PessoaId_TurmaId",
                table: "inscricao",
                columns: new[] { "PessoaId", "TurmaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inscricao_TurmaId",
                table: "inscricao",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inscricao");
        }
    }
}
