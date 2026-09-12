using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiOefeningen2.Data.IRepository
{
    public interface IGameRepository
    {
        public List<Game> GetGames();
    }
}