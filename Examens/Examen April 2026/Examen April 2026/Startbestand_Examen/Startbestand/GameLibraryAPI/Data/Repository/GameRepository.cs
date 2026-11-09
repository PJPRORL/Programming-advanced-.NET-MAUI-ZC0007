namespace GameLibraryAPI.Data.Repository
{
    public class GameRepository: GenericRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context)
        {

        }

        public GetAllGamesAsync()
        {

        }

        public ZoekOpBasisDatum(DateTime startDatum, DateTime endDate)
        {

        }

        GetGameByIdReviewsVanGebruikersAsync(int id)
        {

        }
    }
}
