namespace Examen_MVC.Models
{
    public class Country
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<Brewer> Brewers { get; set; }
    }
}