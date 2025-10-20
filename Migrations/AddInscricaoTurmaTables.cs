using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    public partial class AddInscricaoTurmaTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Tabela Pessoa
            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySql.EntityFrameworkCore.Metadata.MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false)
                    // ...adicione outros campos necessários...
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            // Tabela Turma
            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySql.EntityFrameworkCore.Metadata.MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false)
                    // ...adicione outros campos necessários...
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                });

            // Tabela InscricaoTurma
            migrationBuilder.CreateTable(
                name: "InscricaoTurma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySql.EntityFrameworkCore.Metadata.MySQLValueGenerationStrategy.IdentityColumn),
                    PessoaId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscricaoTurma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InscricaoTurma_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InscricaoTurma_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InscricaoTurma_PessoaId_TurmaId",
                table: "InscricaoTurma",
                columns: new[] { "PessoaId", "TurmaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InscricaoTurma_TurmaId",
                table: "InscricaoTurma",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "InscricaoTurma");
            migrationBuilder.DropTable(name: "Turma");
            migrationBuilder.DropTable(name: "Pessoa");
        }
    }
}
