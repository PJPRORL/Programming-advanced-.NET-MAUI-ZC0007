using System.ComponentModel.DataAnnotations;

namespace GameLibraryAPI.Dto
{
    public class GebruikerMetRollenDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
