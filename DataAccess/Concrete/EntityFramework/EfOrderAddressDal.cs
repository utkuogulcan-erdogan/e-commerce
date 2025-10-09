using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderAddressDal : EfEntityRepositoryBase<OrderAddress, MyShopContext>, IOrderAddressDal
    {
        public EfOrderAddressDal(MyShopContext context) : base(context)
        {
        }
    }
}
