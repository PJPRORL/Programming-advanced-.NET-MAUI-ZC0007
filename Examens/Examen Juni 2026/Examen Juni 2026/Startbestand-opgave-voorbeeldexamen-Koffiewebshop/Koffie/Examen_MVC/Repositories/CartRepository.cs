using Examen_MVC.Data;
using Examen_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Examen_MVC.Repositories
{
    public class CartRepository : GenericRepository<Cart>
    {
        public CartRepository(ShopContext context) : base(context)
        {
        }

        public async Task<Cart[]> GetItemsInCartForUser(string userId)
        {
            return await _context.Carts
                            .Include(x => x.Coffee)
                            .Where(x => x.UserId == userId)
                            .ToArrayAsync();
        }

        public async Task ClearCartAsync(string userId)
        {
            var recordsToDelete = _context.Carts.Where(x => x.UserId == userId);

            _context.Carts.RemoveRange(recordsToDelete);
        }
    }
}