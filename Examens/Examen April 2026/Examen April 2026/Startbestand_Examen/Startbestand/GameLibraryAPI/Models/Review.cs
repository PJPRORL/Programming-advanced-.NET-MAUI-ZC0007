using System.ComponentModel.DataAnnotations;

namespace GameLibraryAPI.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string? GebruikerId { get; set; }

        public int GamePlatformId { get; set; }

        [Required(ErrorMessage = "Rating is verplicht.")]
        [MaxLength(50, ErrorMessage = "Rating moet tussen de 0 en de 5 liggen.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Comment is verplicht.")]
        [StringLength(2, ErrorMessage = "Comment moet minimaal 10 en maximaal 600 karakters bevatten.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "CreatedAt is verplicht.")]
        public DateTime CreatedAt { get; set; }

        // Relaties

    }
}
