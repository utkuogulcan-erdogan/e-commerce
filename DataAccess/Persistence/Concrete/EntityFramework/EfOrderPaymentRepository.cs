using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Infrastructure.Persistence.Abstract;

namespace Infrastructure.Persistence.Concrete.EntityFramework
{
    public class EfOrderPaymentRepository : EfEntityRepositoryBase<OrderPayment, MyShopContext>, IOrderPaymentRepository
    {
        public EfOrderPaymentRepository(MyShopContext context) : base(context)
        {
        }
    }
}
