using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Infrastructure.Persistence.Abstract;

namespace Infrastructure.Persistence.Concrete.EntityFramework
{
    public class EfOrderLineRepository : EfEntityRepositoryBase<OrderLine, MyShopContext>, IOrderLineRepository
    {
        public EfOrderLineRepository(MyShopContext context) : base(context)
        {
        }
    }
}
