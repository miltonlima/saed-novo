using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class InscricaoTurma
    {
        public int Id { get; set; }

        [Required]
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; } = null!;

        [Required]
        public int TurmaId { get; set; }
        public Turma Turma { get; set; } = null!;
    }
}
