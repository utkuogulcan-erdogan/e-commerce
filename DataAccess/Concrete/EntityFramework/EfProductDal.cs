using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, MyShopContext>, IProductDal
    {
        public EfProductDal(MyShopContext context) : base(context)
        {
        }

        public async Task<List<ProductDisplayDto>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Images)
                .Select(p => new ProductDisplayDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryName = p.CategoryId.HasValue ? "" : "",
                    Images = p.Images.Select(i => new ProductImageDto
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        ImageUrl = i.Url,
                        SortOrder = i.SortOrder,
                        IsPrimary = i.IsPrimary
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Product> FindByIdAsync(Guid id)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
