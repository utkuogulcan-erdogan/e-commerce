using Core.DataAccess.EntityFramework;
using Core.Specifications;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal : EfEntityRepositoryBase<Basket, MyShopContext>, IBasketDal
    {
        public EfBasketDal(MyShopContext context) : base(context)
        {
        }
        public async Task<List<BasketDisplayDto>> GetAllBasketsDetailedAsync()
        {
            return await _context.Baskets
                .AsNoTracking()
                .Select(b => new BasketDisplayDto
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    CreatedAt = b.CreatedAt,
                    ExpiresAt = b.ExpiresAt,
                    BasketLines = b.BasketLines.Select(bl => new BasketLineDisplayDto
                    {
                        Id = bl.Id,
                        ProductId = bl.ProductId,
                        Quantity = bl.Quantity,
                        Product = new ProductDisplayDto
                        {
                            Id = bl.Product.Id,
                            Name = bl.Product.Name,
                            Description = bl.Product.Description,
                            Price = bl.Product.Price,
                            Stock = bl.Product.Stock,
                            Images = bl.Product.Images
                                .Select(pi => new ProductImageDto
                                {
                                    Id = pi.Id,
                                    ImageUrl = pi.Url,
                                    SortOrder = pi.SortOrder,
                                    IsPrimary = pi.IsPrimary
                                }).ToList()
                        }
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<BasketDisplayDto?> GetDetailedBasketAsync(ISpecification<Basket> specification)
        {
            return await _context.Baskets
                .AsNoTracking()
                .Where(specification.Criteria)
                .Select(b => new BasketDisplayDto
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    CreatedAt = b.CreatedAt,
                    ExpiresAt = b.ExpiresAt,
                    BasketLines = b.BasketLines.Select(bl => new BasketLineDisplayDto
                    {
                        Id = bl.Id,
                        ProductId = bl.ProductId,
                        Quantity = bl.Quantity,
                        Product = new ProductDisplayDto
                        {
                            Id = bl.Product.Id,
                            Name = bl.Product.Name,
                            Description = bl.Product.Description,
                            Price = bl.Product.Price,
                            Stock = bl.Product.Stock,
                            Images = bl.Product.Images
                                .Select(pi => new ProductImageDto
                                {
                                    Id = pi.Id,
                                    ImageUrl = pi.Url,
                                    SortOrder = pi.SortOrder,
                                    IsPrimary = pi.IsPrimary
                                }).ToList()
                        }
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

    }
}
