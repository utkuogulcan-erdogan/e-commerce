using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    internal class EfBasketLineDal : EfEntityRepositoryBase<BasketLine, MyShopContext>, IBasketLineDal
    {
    }
}
