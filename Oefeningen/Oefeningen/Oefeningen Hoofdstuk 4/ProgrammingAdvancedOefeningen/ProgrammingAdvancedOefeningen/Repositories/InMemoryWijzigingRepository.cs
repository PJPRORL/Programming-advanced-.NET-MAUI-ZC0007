namespace ProgrammingAdvancedOefeningen.Repositories
{
    public class InMemoryWijzigingRepository : IWijzigingRepository
    {
        private static List<Wijziging> wijzigingen = new List<Wijziging>();
        public void GeslaagdeWijziging(string actie, int ventilatorId)
        {
            int wijzigingId = wijzigingen.Max(wijzig => wijzig.Id) + 1;
        }
        public List<Wijziging> GetAllWijzigingen()
        {
            return wijzigingen;
        }
    }
}
