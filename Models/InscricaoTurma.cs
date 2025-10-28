using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    [Table("inscricao")]
    public class Inscricao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O aluno é obrigatório")]
        [Display(Name = "Aluno")]
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; } = null!;

        [Required(ErrorMessage = "A turma é obrigatória")]
        [Display(Name = "Turma")]
        public int TurmaId { get; set; }
        public Turma Turma { get; set; } = null!;

        [Display(Name = "Data de Inscrição")]
        public DateTime DataInscricao { get; set; } = DateTime.Now;

        [Display(Name = "Status")]
        public string Status { get; set; } = "Ativa";
    }
}
