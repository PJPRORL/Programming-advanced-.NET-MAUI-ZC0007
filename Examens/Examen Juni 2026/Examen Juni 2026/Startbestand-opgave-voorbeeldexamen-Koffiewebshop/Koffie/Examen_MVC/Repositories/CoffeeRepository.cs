using Examen_MVC.Data;
using Examen_MVC.Models;

namespace Examen_MVC.Repositories
{
    public class CoffeeRepository : GenericRepository<Coffee>
    {
        public CoffeeRepository(ShopContext context) : base(context)
        {
        }

        // TODO: Vul Repository aan met vereiste methoden

    }
}