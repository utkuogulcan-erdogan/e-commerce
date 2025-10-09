using Core.DataAccess.EntityFramework;
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
                var result = await (
                    from basket in _context.Baskets
                    join basketLine in _context.BasketLines
                        on basket.Id equals basketLine.BasketId into basketLinesGroup

                    select new BasketDisplayDto
                    {
                        Id = basket.Id,
                        UserId = basket.UserId,
                        CreatedAt = basket.CreatedAt,
                        ExpiresAt = basket.ExpiresAt,

                        BasketLines = basketLinesGroup.Select(bl => new BasketLineDisplayDto
                        {
                            Id = bl.Id,
                            BasketId = bl.BasketId,
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

        public async Task<BasketDisplayDto> GetDetailedBasketByIdAsync(Guid id)
        {
                var result = await (
                    from basket in _context.Baskets
                    where basket.Id == id
                    join basketLine in _context.BasketLines
                        on basket.Id equals basketLine.BasketId into basketLinesGroup
                    select new BasketDisplayDto
                    {
                        Id = basket.Id,
                        UserId = basket.UserId,
                        CreatedAt = basket.CreatedAt,
                        ExpiresAt = basket.ExpiresAt,

                        BasketLines = basketLinesGroup.Select(bl => new BasketLineDisplayDto
                        {
                            Id = bl.Id,
                            BasketId = bl.BasketId,
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

        public async Task<BasketDisplayDto> GetDetailedBasketByUserIdAsync(Guid userId)
        {
                var result = await (
                    from basket in _context.Baskets
                    where basket.UserId == userId
                    join basketLine in _context.BasketLines
                         on basket.Id equals basketLine.BasketId into basketLinesGroup
                    select new BasketDisplayDto
                    {
                        Id = basket.Id,
                        UserId = basket.UserId,
                        CreatedAt = basket.CreatedAt,
                        ExpiresAt = basket.ExpiresAt,

                        BasketLines = basketLinesGroup.Select(bl => new BasketLineDisplayDto
                        {
                            Id = bl.Id,
                            BasketId = bl.BasketId,
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
