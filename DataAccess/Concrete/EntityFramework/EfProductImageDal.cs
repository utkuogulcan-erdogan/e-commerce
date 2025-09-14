using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    internal class EfProductImageDal : EfEntityRepositoryBase<ProductImage, MyShopContext>, IProductImageDal
    {
    }
}
