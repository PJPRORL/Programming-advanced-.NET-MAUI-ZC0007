
namespace DeMol.Models
{
    public class Deelnemer
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam {  get; set; }

        public int Score { get; set; }
        public int PasVragen { get; set; }
        public bool NietIngevuld {  get; set; }

        public Rol Rol { get; set; }
        public string VolledigeNaam => Voornaam + " " + Achternaam;
    }
}
