using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    public partial class AddModalidadeTurmaRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modalidade_Turma",
                table: "modalidade");

            migrationBuilder.DropIndex(
                name: "IX_Modalidade_TurmaId",
                table: "modalidade");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "modalidade");

            migrationBuilder.CreateTable(
                name: "modalidade_turma",
                columns: table => new
                {
                    ModalidadeId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modalidade_turma", x => new { x.ModalidadeId, x.TurmaId });
                    table.ForeignKey(
                        name: "FK_modalidade_turma_modalidade_ModalidadeId",
                        column: x => x.ModalidadeId,
                        principalTable: "modalidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_modalidade_turma_turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_modalidade_turma_TurmaId",
                table: "modalidade_turma",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "modalidade_turma");

            migrationBuilder.AddColumn<int>(
                name: "TurmaId",
                table: "modalidade",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modalidade_TurmaId",
                table: "modalidade",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modalidade_Turma",
                table: "modalidade",
                column: "TurmaId",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}