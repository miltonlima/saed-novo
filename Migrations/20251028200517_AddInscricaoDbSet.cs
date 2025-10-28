using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    public partial class AddInscricaoDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // No-op: evitando recriação de tabelas existentes (Modalidade, Pessoa, Turma, etc.)
            // A tabela 'inscricao' já é criada por 20251019100000_CreateInscricaoTable.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No-op
        }
    }
}
