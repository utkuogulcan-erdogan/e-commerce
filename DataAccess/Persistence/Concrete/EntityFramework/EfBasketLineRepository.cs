using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Infrastructure.Persistence.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Concrete.EntityFramework
{
    public class EfBasketLineRepository : EfEntityRepositoryBase<BasketLine, MyShopContext>, IBasketLineRepository
    {
        public EfBasketLineRepository(MyShopContext context) : base(context)
        {
        }

        public async Task ClearBasketLinesAsync(Guid basketId)
        {
            await _context.BasketLines
         .Where(bl => bl.BasketId == basketId)
         .ExecuteDeleteAsync();
        }
    }
}
