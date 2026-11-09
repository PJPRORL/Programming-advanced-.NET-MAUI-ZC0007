using System.ComponentModel.DataAnnotations;

namespace GameLibraryAPI.Dto
{
    public class GebruikerInlogDto
    {
        [Required(ErrorMessage = "UserName is verplicht!")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Password is verplicht!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
}
