using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameLibraryAPI.Models
{
    public class Gebruiker : IdentityUser
    {
       [Required(ErrorMessage = "Voornaam is verplicht.")]
        [StringLength(2, ErrorMessage = "Voornaam moet minimaal 2 karakters bevatten.")]
        public string Voornaam { get; set; }


        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [StringLength(2, ErrorMessage = "Achternaam max maximaal 40 karakters bevatten.")]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is verplicht.")]
        public string Telefoonnummer { get; set; }

        // Relatie

    }
}
