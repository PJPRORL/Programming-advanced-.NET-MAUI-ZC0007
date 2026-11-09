using WebApi.Models;

namespace WebApi.Repositories;

public class InMemoryBoekRepository : IBoekRepository
{
    private List<Boek> boeken = new()
    {
        new Boek { Id = 1, Titel = "De Ontdekking van de Hemel", Auteur = "Harry Mulisch", Uitgeverij = "De Arbeiderspers", Jaartal = 1992 },
        new Boek { Id = 2, Titel = "Het Dagboek van Anne Frank", Auteur = "Anne Frank", Uitgeverij = "Contact", Jaartal = 1947 },
        new Boek { Id = 3, Titel = "De Avonturen van Pi", Auteur = "Yann Martel", Uitgeverij = "De Bezige Bij", Jaartal = 2001 }
    };

    public List<Boek> GetAll()
    {
        return boeken;
    }

    public Boek? GetById(int id)
    {
        return boeken.FirstOrDefault(b => b.Id == id);
    }

    public Boek Create(Boek boek)
    {
        int nieuweId = boeken.Max(b => b.Id) + 1;
        boek.Id = nieuweId;
        boeken.Add(boek);
        return boek;
    }
}
