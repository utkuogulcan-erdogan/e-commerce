using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketLineDal : EfEntityRepositoryBase<BasketLine, MyShopContext>, IBasketLineDal
    {
        public EfBasketLineDal(MyShopContext context) : base(context)
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
