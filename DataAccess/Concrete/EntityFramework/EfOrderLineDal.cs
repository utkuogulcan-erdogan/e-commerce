using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    internal class EfOrderLineDal : EfEntityRepositoryBase<OrderLine, MyShopContext>, IOrderLineDal
    {
    }
}
