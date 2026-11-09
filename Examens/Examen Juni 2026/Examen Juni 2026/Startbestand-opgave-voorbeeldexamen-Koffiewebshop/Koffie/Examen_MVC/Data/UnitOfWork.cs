using Examen_MVC.Models;
using Examen_MVC.Repositories;

namespace Examen_MVC.Data
{
    public class UnitOfWork
    {
        private readonly ShopContext ctx;

        private GenericRepository<Brewer> brewerRepository;
        private CartRepository cartRepository;
        private CoffeeRepository coffeeRepository;

        public UnitOfWork(ShopContext shopContext)
        {
            ctx = shopContext;
        }

        public GenericRepository<Brewer> BrewerRepository
        {
            get
            {
                return brewerRepository ??= new GenericRepository<Brewer>(ctx);
            }
        }

        public CartRepository CartRepository
        {
            get
            {
                return cartRepository ??= new CartRepository(ctx);
            }
        }

        public CoffeeRepository CoffeeRepository
        {
            get
            {
                return coffeeRepository ??= new CoffeeRepository(ctx);
            }
        }

        public async Task SaveChangesAsync()
        {
            await ctx.SaveChangesAsync();
        }
    }
}