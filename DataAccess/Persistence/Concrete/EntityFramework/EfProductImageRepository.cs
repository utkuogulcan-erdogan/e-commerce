using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Infrastructure.Persistence.Abstract;

namespace Infrastructure.Persistence.Concrete.EntityFramework
{
    public class EfProductImageRepository : EfEntityRepositoryBase<ProductImage, MyShopContext>, IProductImageRepository
    {
        public EfProductImageRepository(MyShopContext context) : base(context)
        {
        }
    }
}
