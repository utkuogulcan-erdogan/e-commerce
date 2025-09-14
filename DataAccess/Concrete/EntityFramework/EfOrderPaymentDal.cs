using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    internal class EfOrderPaymentDal : EfEntityRepositoryBase<OrderPayment, MyShopContext>, IOrderPaymentDal
    {
    }
}
