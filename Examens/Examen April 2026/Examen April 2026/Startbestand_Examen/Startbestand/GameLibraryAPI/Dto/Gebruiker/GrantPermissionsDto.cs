using System.ComponentModel.DataAnnotations;

namespace GameLibraryAPI.Dto
{
    public class GrantPermissionDto
    {
        public string Email { get; set; }
        public string RolNaam { get; set; }
    }
}
