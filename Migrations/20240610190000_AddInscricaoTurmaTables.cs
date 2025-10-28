using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MvcMovie.Migrations
{
    public partial class AddInscricaoTurmaTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Nascimento = table.Column<DateTime>(nullable: false),
                    Cpf = table.Column<string>(maxLength: 14, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Matricula = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modalidade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modalidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(maxLength: 255, nullable: true),
                    DataInicio = table.Column<DateTime>(nullable: true),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModalidadeTurma",
                columns: table => new
                {
                    ModalidadeId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModalidadeTurma", x => new { x.ModalidadeId, x.TurmaId });
                    table.ForeignKey(
                        name: "FK_ModalidadeTurma_Modalidade_ModalidadeId",
                        column: x => x.ModalidadeId,
                        principalTable: "Modalidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModalidadeTurma_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InscricaoTurma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PessoaId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false),
                    DataInscricao = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
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

            migrationBuilder.CreateIndex(
                name: "IX_ModalidadeTurma_TurmaId",
                table: "ModalidadeTurma",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InscricaoTurma");
            migrationBuilder.DropTable(
                name: "ModalidadeTurma");
            migrationBuilder.DropTable(
                name: "Pessoa");
            migrationBuilder.DropTable(
                name: "Turma");
            migrationBuilder.DropTable(
                name: "Modalidade");
        }
    }
}
