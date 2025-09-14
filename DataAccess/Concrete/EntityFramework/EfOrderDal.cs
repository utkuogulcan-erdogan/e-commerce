using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    internal class EfOrderDal : EfEntityRepositoryBase<Order, MyShopContext>, IOrderDal
    {
    }
}
