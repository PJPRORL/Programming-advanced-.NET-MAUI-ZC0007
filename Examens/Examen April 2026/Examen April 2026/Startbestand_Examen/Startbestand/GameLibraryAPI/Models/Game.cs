
using System.ComponentModel.DataAnnotations;

namespace GameLibraryAPI.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titel is verplicht.")]
        [StringLength(50, ErrorMessage = "Titel moet minimaal 4 tekens bevatten en niet meer dan 50 tekens bevatten.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ReleaseDate is verplicht.")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Prijs is verplicht.")]
        [MaxLength(50, ErrorMessage = "Prijs moet tussen de 0 en 99999 liggen.")]
        public decimal Price { get; set; }

        // Relaties
        
    }
}
