using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Examen_MVC.Models
{
    public class CustomUser : IdentityUser
    {
        [PersonalData]
        [Required]
        [MaxLength(50)]
        public string Voornaam { get; set; }

        [PersonalData]
        [Required]
        [MaxLength(50)]
        public string Achternaam { get; set; }

        [PersonalData]
        [MaxLength(50)]
        public string? Straat { get; set; }

        [PersonalData]
        [Range(1, int.MaxValue)]
        public int? Huisnummer { get; set; }

        [PersonalData]
        public string? Postcode { get; set; }

        [PersonalData]
        [MaxLength(50)]
        public string? Gemeente { get; set; }

        [PersonalData]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }

        public List<Cart> ItemsInCart { get; set; } = new List<Cart>();
    }
}