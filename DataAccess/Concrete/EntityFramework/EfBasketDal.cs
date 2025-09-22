using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal : EfEntityRepositoryBase<Basket, MyShopContext>, IBasketDal
    {
        public async Task<List<Basket>> GetAllBasketsDetailedAsync()
        {
            using (var context = new MyShopContext())
            {
                var result = await context.Baskets.Include(b => b.User)
                                                  .Include(b => b.BasketLines)
                                                  .ThenInclude(bi => bi.Product)
                                                  .ThenInclude(p => p.Images)
                                                  .IgnoreAutoIncludes()
                                                  .AsNoTracking()
                                                  .ToListAsync();
                return result;
            }
        }

        public async Task<Basket> GetDetailedBasketByUserIdAsync(Guid userId)
        {
            using (var context = new MyShopContext())
            {
                var result = await context.Baskets
                                    .Include(b => b.User)
                                    .Include(b => b.BasketLines)
                                    .ThenInclude(bi => bi.Product)
                                    .ThenInclude(p => p.Images)
                                    .IgnoreAutoIncludes()
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(b => b.UserId == userId);
                return result;
            }
        }
    }
}
