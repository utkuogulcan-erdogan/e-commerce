using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, MyShopContext>, IOrderDal
    {
        public EfOrderDal(MyShopContext context) : base(context)
        {
        }
    }
}
