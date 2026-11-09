using System.ComponentModel.DataAnnotations;

namespace Examen_MVC.Models
{
    public class Cart : IModel
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public CustomUser User { get; set; }

        [Required]
        public int CoffeeId { get; set; }

        public Coffee Coffee { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}