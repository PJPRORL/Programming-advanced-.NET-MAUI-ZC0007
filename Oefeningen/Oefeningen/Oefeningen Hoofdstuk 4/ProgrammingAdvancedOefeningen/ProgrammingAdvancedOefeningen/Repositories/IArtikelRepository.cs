namespace ProgrammingAdvancedOefeningen.Repositories
{
    public interface IArtikelRepository
    {
        List<Artikel> GetAll();
        Artikel? GetArtikelById(int id);
        List<Artikel> GetArtikelBySoort(string soort);
        List<Artikel> GetVoorraadArtikel();
        List<Artikel> GetArtikelMerk(string merk);
        Artikel Create(Artikel artikel);
        void Update(int id, Artikel artikel);
        void Delete(int id);
    }
}
