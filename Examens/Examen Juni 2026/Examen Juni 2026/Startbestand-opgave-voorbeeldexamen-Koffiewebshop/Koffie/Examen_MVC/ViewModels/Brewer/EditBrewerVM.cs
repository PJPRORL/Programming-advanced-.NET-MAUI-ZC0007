using System.ComponentModel.DataAnnotations;

namespace Examen_MVC.ViewModels.Brewer
{
    public class EditBrewerDTO
    {
        [Required(ErrorMessage = "ID is verplicht")]
        public int Id { get; set; }

        [Display(Name = "Naam")]
        [Required(ErrorMessage = "Naam is verplicht")]
        [MinLength(3, ErrorMessage = "Brouwer naam moet minstens 3 karakters lang zijn")]
        [MaxLength(50, ErrorMessage = "Brouwer naam mag niet langer zijn dan 50 karakters")]
        public string Name { get; set; }
    }
}