using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Modalidade
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da modalidade é obrigatório")]
        [StringLength(100)]
        [Display(Name = "Nome da Modalidade")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(255)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        public ICollection<ModalidadeTurma> ModalidadesTurmas { get; set; } = new List<ModalidadeTurma>();
    }
}
