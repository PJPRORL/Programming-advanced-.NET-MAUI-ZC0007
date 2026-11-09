namespace ProgrammingAdvancedOefeningen.Repositories
{
    public interface IVentilatorRepository
    {
        List<Ventilator> GetAll();
        Ventilator? GetVentilatorById(int id);
        Ventilator Create(Ventilator ventilator);
        void Update(int id, Ventilator ventilator);
        void Delete(int id);
        void Replace(int id);
    }
}
