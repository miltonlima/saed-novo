using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        public string? Matricula { get; set; }

        public DateTime? Nascimento { get; set; }

        public string? Cpf { get; set; }

        public ICollection<InscricaoTurma> Inscricoes { get; set; } = new List<InscricaoTurma>();
    }
}
