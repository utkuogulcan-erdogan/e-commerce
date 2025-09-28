using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal : EfEntityRepositoryBase<Basket, MyShopContext>, IBasketDal
    {
        public async Task<List<BasketDto>> GetAllBasketsDetailedAsync()
        {
            using (var context = new MyShopContext())
            {
                var result = await (
                    from basket in context.Baskets
                    join basketLine in context.BasketLines
                        on basket.Id equals basketLine.BasketId into basketLinesGroup

                    select new BasketDto
                    {
                        Id = basket.Id,
                        UserId = basket.UserId,
                        CreatedAt = basket.CreatedAt,
                        ExpiresAt = basket.ExpiresAt,

                        BasketLines = basketLinesGroup.Select(bl => new BasketLineDto
                        {
                            Id = bl.Id,
                            BasketId = bl.BasketId,
                            ProductId = bl.ProductId,
                            Quantity = bl.Quantity,

                            Product = new ProductDto
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
                                        ProductId = pi.ProductId,
                                        ImageUrl = pi.Url,
                                        SortOrder = pi.SortOrder,
                                        IsPrimary = pi.IsPrimary
                                    }).ToList()
                            }
                        }).ToList()
                    }
                ).AsNoTracking().ToListAsync();

                return result;
            }
        }

        public async Task<BasketDto> GetDetailedBasketByIdAsync(Guid id)
        {
            using (var context = new MyShopContext())
            {
                var result = await (
                    from basket in context.Baskets
                    where basket.Id == id
                    join basketLine in context.BasketLines
                        on basket.Id equals basketLine.BasketId into basketLinesGroup
                    select new BasketDto
                    {
                        Id = basket.Id,
                        UserId = basket.UserId,
                        CreatedAt = basket.CreatedAt,
                        ExpiresAt = basket.ExpiresAt,

                        BasketLines = basketLinesGroup.Select(bl => new BasketLineDto
                        {
                            Id = bl.Id,
                            BasketId = bl.BasketId,
                            ProductId = bl.ProductId,
                            Quantity = bl.Quantity,

                            Product = new ProductDto
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
                                        ProductId = pi.ProductId,
                                        ImageUrl = pi.Url,
                                        SortOrder = pi.SortOrder,
                                        IsPrimary = pi.IsPrimary
                                    }).ToList()
                            }
                        }).ToList()
                    }
                ).AsNoTracking().FirstOrDefaultAsync();

                return result;
            }
        }

        public async Task<BasketDto> GetDetailedBasketByUserIdAsync(Guid userId)
        {
            using (var context = new MyShopContext())
            {
                var result = await (
                    from basket in context.Baskets
                    where basket.UserId == userId
                    join basketLine in context.BasketLines
                         on basket.Id equals basketLine.BasketId into basketLinesGroup
                    select new BasketDto
                    {
                        Id = basket.Id,
                        UserId = basket.UserId,
                        CreatedAt = basket.CreatedAt,
                        ExpiresAt = basket.ExpiresAt,

                        BasketLines = basketLinesGroup.Select(bl => new BasketLineDto
                        {
                            Id = bl.Id,
                            BasketId = bl.BasketId,
                            ProductId = bl.ProductId,
                            Quantity = bl.Quantity,

                            Product = new ProductDto
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
                                        ProductId = pi.ProductId,
                                        ImageUrl = pi.Url,
                                        SortOrder = pi.SortOrder,
                                        IsPrimary = pi.IsPrimary
                                    }).ToList()
                            }
                        }).ToList()
                    }
                ).AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
        }

    }
}
