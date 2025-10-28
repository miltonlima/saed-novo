using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class Turma
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [Display(Name = "Data de Início")]
        [DataType(DataType.Date)]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data de Fim")]
        [DataType(DataType.Date)]
        public DateTime? DataFim { get; set; }

        [Display(Name = "Status")]
        public string? Status { get; set; }

        [Display(Name = "Data de Criação")]
        [DataType(DataType.DateTime)]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public ICollection<ModalidadeTurma> ModalidadesTurmas { get; set; } = new List<ModalidadeTurma>();

        public ICollection<InscricaoTurma> Inscricoes { get; set; } = new List<InscricaoTurma>();
    }
}

// Certifique-se de que só existe UMA definição da classe Turma neste namespace.
// Remova ou renomeie a duplicata em outro arquivo do projeto.