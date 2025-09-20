using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, MyShopContext>, IProductDal
    {
        
        public async Task<List<Product>> GetAllProductsAsync()
        {
            using (var context = new MyShopContext())
            {
                var query = context.Products.AsNoTracking().Include(p => p.Images);

                return await query.ToListAsync();
            }
        }

        public async Task<Product> FindByIdAsync(Guid id)
        {
            using (var context = new MyShopContext())
            {
                return await context.Products.
                     AsNoTracking()
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
        }
    }
}
