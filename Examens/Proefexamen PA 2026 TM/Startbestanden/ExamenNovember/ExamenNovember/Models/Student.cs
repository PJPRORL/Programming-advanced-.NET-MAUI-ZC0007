namespace ExamenNovember.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Naam { get; set; } = "";
        public string Voornaam { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime Geboortedatum { get; set; }
    }
}
