

namespace Orders.Data.Repository;

public class WerknemerRepository : BaseRepository, IWerknemerRepository
{
    public List<Werknemer> OphalenWerknemers()
    {
        string sql = @"SELECT * FROM werknemers ORDER BY Achternaam, Voornaam";

        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            return db.Query<Werknemer>(sql).ToList();
        }
    }

    public IEnumerable<Werknemer> OphalenWerknemersViaAchternaamEnVoornaam(string achternaam, string voornaam)
    {

        string sql = @"SELECT *";
        sql += " FROM werknemers";
        sql += " WHERE achternaam like '%'+ @achternaam +'%'";
        sql += " AND voornaam like '%'+ @voornaam + '%'";
        sql += " ORDER BY Achternaam, Voornaam";

        var parameters = new { @voornaam = voornaam, @achternaam = achternaam };

        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            return db.Query<Werknemer>(sql, parameters);
        }
    }

    public ICollection<Werknemer> OphalenWerknemersViaFunctie(string functie)
    {
        string sql = @"SELECT * FROM werknemers WHERE Functie like '%' + @functie + '%' ORDER BY Achternaam, Voornaam";

        var parameters = new { @functie = functie };

        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            return db.Query<Werknemer>(sql, parameters).ToList();
        }
    }

    public Werknemer OphalenWerknemerViaPK(int werknemerID)
    {
        string sql = @"SELECT *";
        sql += " FROM werknemers";
        sql += " WHERE Id = @id";
        sql += " ORDER BY Achternaam, Voornaam";

        var parameters = new { @id = werknemerID };
        using (IDbConnection db = new SqlConnection(ConnectionString))
        {
            return db.QuerySingleOrDefault<Werknemer>(sql, parameters);
        }
    }
}