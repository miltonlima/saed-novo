using System.ComponentModel.DataAnnotations;
using MvcMovie.Attributes;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
    }

    public class Pessoa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres")]
        [CpfValidation]
        [Display(Name = "CPF")]
        public string Cpf { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [StringLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A matrícula é obrigatória")]
    [StringLength(15, ErrorMessage = "A matrícula deve ter no máximo 15 números")]
    [Display(Name = "Matrícula")]
    public string Matricula { get; set; } = string.Empty;

    public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
    }
    public enum StatusTurma
    {
        Ativa = 1,
        Inativa = 0
    }

    // Removida a definição duplicada da classe Modalidade deste arquivo.
    // Mantenha UMA definição de Modalidade em outro arquivo (por exemplo Models\Modalidade.cs).
}