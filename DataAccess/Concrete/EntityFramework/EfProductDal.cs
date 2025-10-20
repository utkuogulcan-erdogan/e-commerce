using Core.DataAccess.EntityFramework;
using Core.Specifications;
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

        public async Task<Product> GetProductAsync(ISpecification<Product> specification)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(specification.Criteria)
                .Include(p => p.Images)
                .FirstOrDefaultAsync();
        }
    }
}
