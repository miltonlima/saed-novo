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
    public enum StatusTurma
    {
        Ativa = 1,
        Inativa = 0
    }

    // Removida a definição duplicada da classe Modalidade deste arquivo.
    // Mantenha UMA definição de Modalidade em outro arquivo (por exemplo Models\Modalidade.cs).
}