using WebApi.Models;

namespace WebApi.Repositories;

public class InMemoryBestellingRepository : IBestellingRepository
{
    private List<Bestelling> bestellingen = new()
    {
        new Bestelling { Id = 1, Naam = "Anna Jansen", Tafelnummer = 4, Gerechten = "Lasagne-Salade", Status = "Gereed" },
        new Bestelling { Id = 2, Naam = "Youssef Benali", Tafelnummer = 7, Gerechten = "Risotto-Gegrilde Groenten", Status = "Bereiden" },
        new Bestelling { Id = 3, Naam = "Maria De Smet", Tafelnummer = 2, Gerechten = "Pasta Carbonara", Status = "Gereed" }
    };

    public List<Bestelling> GetAll()
    {
        return bestellingen;
    }

    public Bestelling? GetById(int id)
    {
        return bestellingen.FirstOrDefault(b => b.Id == id);
    }

    public Bestelling Create(Bestelling bestelling)
    {
        int nieuweId = bestellingen.Max(b => b.Id) + 1;
        bestelling.Id = nieuweId;
        bestellingen.Add(bestelling);
        return bestelling;
    }

    public void Update(int id, Bestelling bestelling)
    {
        var bestaandeBestelling = bestellingen.FirstOrDefault(b => b.Id == id);

        if (bestaandeBestelling != null)
        {
            bestaandeBestelling.Id = bestelling.Id;
            bestaandeBestelling.Naam = bestelling.Naam;
            bestaandeBestelling.Tafelnummer = bestelling.Tafelnummer;
            bestaandeBestelling.Gerechten = bestelling.Gerechten;
            bestaandeBestelling.Status = bestelling.Status;
        }
    }

    public void Delete(int id)
    {
        var bestelling = bestellingen.FirstOrDefault(b => b.Id == id);

        if (bestelling != null)
        {
            bestellingen.Remove(bestelling);
        }
    }
}
