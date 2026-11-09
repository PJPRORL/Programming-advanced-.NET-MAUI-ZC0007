using GameLibraryAPI.Data.Repository;
using GameLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryAPI.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APIDbContext _context;

        public UnitOfWork()
        {

        }

        public GameRepository
    }
}
