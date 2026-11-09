namespace GameLibraryAPI.Models
{
    public class Settings
    {

        // Afblijven van deze klasse

        public string? Name { get; set; }
        public char[]? Secret { get; set; }
        public string? ValidIssuer { get; set; }
        public string? ValidAudience { get; set; }
    }
}
