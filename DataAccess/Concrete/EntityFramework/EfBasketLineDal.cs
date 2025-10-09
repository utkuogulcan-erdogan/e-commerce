using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketLineDal : EfEntityRepositoryBase<BasketLine, MyShopContext>, IBasketLineDal
    {
        public EfBasketLineDal(MyShopContext context) : base(context)
        {
        }
    }
}
