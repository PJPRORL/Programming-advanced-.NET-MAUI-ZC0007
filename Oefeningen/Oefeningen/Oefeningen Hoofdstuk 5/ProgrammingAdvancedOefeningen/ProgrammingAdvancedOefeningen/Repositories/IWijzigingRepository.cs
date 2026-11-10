namespace ProgrammingAdvancedOefeningen.Repositories
{
    public interface IWijzigingRepository
    {
        void GeslaagdeWijziging(string actie, int ventilatorId);
        List<Wijziging> GetAllWijzigingen();
    }
}
