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

        public async Task<List<BasketDisplayDto>> GetAllBasketsDetailedAsync(CancellationToken cancellationToken = default)
        {
            var query = from basket in _context.Baskets
                       join basketLine in _context.BasketLines on basket.Id equals basketLine.BasketId into basketLinesGroup
                       from basketLine in basketLinesGroup.DefaultIfEmpty()
                       join product in _context.Products on basketLine.ProductId equals product.Id into productGroup
                       from product in productGroup.DefaultIfEmpty()
                       select new
                       {
                           BasketId = basket.Id,
                           basket.UserId,
                           basket.CreatedAt,
                           basket.ExpiresAt,
                           BasketLineId = basketLine != null ? basketLine.Id : (Guid?)null,
                           BasketLineBasketId = basketLine != null ? basketLine.BasketId : (Guid?)null,
                           BasketLineProductId = basketLine != null ? basketLine.ProductId : (Guid?)null,
                           BasketLineQuantity = basketLine != null ? basketLine.Quantity : (int?)null,
                           ProductId = product != null ? product.Id : (Guid?)null,
                           ProductName = product != null ? product.Name : null,
                           ProductDescription = product != null ? product.Description : null,
                           ProductPrice = product != null ? product.Price : (decimal?)null,
                           ProductStock = product != null ? product.Stock : (int?)null
                       };

            var results = await query.ToListAsync(cancellationToken);

            var basketDtos = results
                .GroupBy(x => x.BasketId)
                .Select(g => new BasketDisplayDto
                {
                    Id = g.Key,
                    UserId = g.First().UserId,
                    CreatedAt = g.First().CreatedAt,
                    ExpiresAt = g.First().ExpiresAt,
                    BasketLines = g.Where(x => x.BasketLineId.HasValue)
                        .Select(x => new BasketLineDisplayDto
                        {
                            Id = x.BasketLineId.Value,
                            BasketId = x.BasketLineBasketId.Value,
                            ProductId = x.BasketLineProductId.Value,
                            Quantity = x.BasketLineQuantity.Value,
                            Product = x.ProductId.HasValue ? new ProductDisplayDto
                            {
                                Id = x.ProductId.Value,
                                Name = x.ProductName,
                                Description = x.ProductDescription,
                                Price = x.ProductPrice.Value,
                                Stock = x.ProductStock.Value,
                                Images = new List<ProductImageDto>()
                            } : null
                        }).ToList()
                }).ToList();

            // Load product images separately to avoid complex joins
            foreach (var basketDto in basketDtos)
            {
                foreach (var basketLine in basketDto.BasketLines)
                {
                    if (basketLine.Product != null)
                    {
                        var images = await _context.ProductImages
                            .Where(pi => pi.ProductId == basketLine.ProductId)
                            .Select(pi => new ProductImageDto
                            {
                                Id = pi.Id,
                                ProductId = pi.ProductId,
                                ImageUrl = pi.Url,
                                SortOrder = pi.SortOrder,
                                IsPrimary = pi.IsPrimary
                            }).ToListAsync(cancellationToken);
                        
                        basketLine.Product.Images = images;
                    }
                }
            }

            return basketDtos;
        }

        public async Task<BasketDisplayDto> GetDetailedBasketByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = from basket in _context.Baskets
                       where basket.Id == id
                       join basketLine in _context.BasketLines on basket.Id equals basketLine.BasketId into basketLinesGroup
                       from basketLine in basketLinesGroup.DefaultIfEmpty()
                       join product in _context.Products on basketLine.ProductId equals product.Id into productGroup
                       from product in productGroup.DefaultIfEmpty()
                       select new
                       {
                           BasketId = basket.Id,
                           basket.UserId,
                           basket.CreatedAt,
                           basket.ExpiresAt,
                           BasketLineId = basketLine != null ? basketLine.Id : (Guid?)null,
                           BasketLineBasketId = basketLine != null ? basketLine.BasketId : (Guid?)null,
                           BasketLineProductId = basketLine != null ? basketLine.ProductId : (Guid?)null,
                           BasketLineQuantity = basketLine != null ? basketLine.Quantity : (int?)null,
                           ProductId = product != null ? product.Id : (Guid?)null,
                           ProductName = product != null ? product.Name : null,
                           ProductDescription = product != null ? product.Description : null,
                           ProductPrice = product != null ? product.Price : (decimal?)null,
                           ProductStock = product != null ? product.Stock : (int?)null
                       };

            var results = await query.ToListAsync(cancellationToken);

            if (!results.Any()) return null;

            var firstResult = results.First();
            var basketDto = new BasketDisplayDto
            {
                Id = firstResult.BasketId,
                UserId = firstResult.UserId,
                CreatedAt = firstResult.CreatedAt,
                ExpiresAt = firstResult.ExpiresAt,
                BasketLines = results.Where(x => x.BasketLineId.HasValue)
                    .Select(x => new BasketLineDisplayDto
                    {
                        Id = x.BasketLineId.Value,
                        BasketId = x.BasketLineBasketId.Value,
                        ProductId = x.BasketLineProductId.Value,
                        Quantity = x.BasketLineQuantity.Value,
                        Product = x.ProductId.HasValue ? new ProductDisplayDto
                        {
                            Id = x.ProductId.Value,
                            Name = x.ProductName,
                            Description = x.ProductDescription,
                            Price = x.ProductPrice.Value,
                            Stock = x.ProductStock.Value,
                            Images = new List<ProductImageDto>()
                        } : null
                    }).ToList()
            };

            // Load product images
            foreach (var basketLine in basketDto.BasketLines)
            {
                if (basketLine.Product != null)
                {
                    var images = await _context.ProductImages
                        .Where(pi => pi.ProductId == basketLine.ProductId)
                        .Select(pi => new ProductImageDto
                        {
                            Id = pi.Id,
                            ProductId = pi.ProductId,
                            ImageUrl = pi.Url,
                            SortOrder = pi.SortOrder,
                            IsPrimary = pi.IsPrimary
                        }).ToListAsync(cancellationToken);
                    
                    basketLine.Product.Images = images;
                }
            }

            return basketDto;
        }

        public async Task<BasketDisplayDto> GetDetailedBasketByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var query = from basket in _context.Baskets
                       where basket.UserId == userId
                       join basketLine in _context.BasketLines on basket.Id equals basketLine.BasketId into basketLinesGroup
                       from basketLine in basketLinesGroup.DefaultIfEmpty()
                       join product in _context.Products on basketLine.ProductId equals product.Id into productGroup
                       from product in productGroup.DefaultIfEmpty()
                       select new
                       {
                           BasketId = basket.Id,
                           basket.UserId,
                           basket.CreatedAt,
                           basket.ExpiresAt,
                           BasketLineId = basketLine != null ? basketLine.Id : (Guid?)null,
                           BasketLineBasketId = basketLine != null ? basketLine.BasketId : (Guid?)null,
                           BasketLineProductId = basketLine != null ? basketLine.ProductId : (Guid?)null,
                           BasketLineQuantity = basketLine != null ? basketLine.Quantity : (int?)null,
                           ProductId = product != null ? product.Id : (Guid?)null,
                           ProductName = product != null ? product.Name : null,
                           ProductDescription = product != null ? product.Description : null,
                           ProductPrice = product != null ? product.Price : (decimal?)null,
                           ProductStock = product != null ? product.Stock : (int?)null
                       };

            var results = await query.ToListAsync(cancellationToken);

            if (!results.Any()) return null;

            var firstResult = results.First();
            var basketDto = new BasketDisplayDto
            {
                Id = firstResult.BasketId,
                UserId = firstResult.UserId,
                CreatedAt = firstResult.CreatedAt,
                ExpiresAt = firstResult.ExpiresAt,
                BasketLines = results.Where(x => x.BasketLineId.HasValue)
                    .Select(x => new BasketLineDisplayDto
                    {
                        Id = x.BasketLineId.Value,
                        BasketId = x.BasketLineBasketId.Value,
                        ProductId = x.BasketLineProductId.Value,
                        Quantity = x.BasketLineQuantity.Value,
                        Product = x.ProductId.HasValue ? new ProductDisplayDto
                        {
                            Id = x.ProductId.Value,
                            Name = x.ProductName,
                            Description = x.ProductDescription,
                            Price = x.ProductPrice.Value,
                            Stock = x.ProductStock.Value,
                            Images = new List<ProductImageDto>()
                        } : null
                    }).ToList()
            };

            // Load product images
            foreach (var basketLine in basketDto.BasketLines)
            {
                if (basketLine.Product != null)
                {
                    var images = await _context.ProductImages
                        .Where(pi => pi.ProductId == basketLine.ProductId)
                        .Select(pi => new ProductImageDto
                        {
                            Id = pi.Id,
                            ProductId = pi.ProductId,
                            ImageUrl = pi.Url,
                            SortOrder = pi.SortOrder,
                            IsPrimary = pi.IsPrimary
                        }).ToListAsync(cancellationToken);
                    
                    basketLine.Product.Images = images;
                }
            }

            return basketDto;
        }

        public async Task<BasketDisplayDto> GetDetailedBasketAsync(Core.Specifications.ISpecification<Basket> specification, CancellationToken cancellationToken = default)
        {
            var query = from basket in _context.Baskets.Where(specification.Criteria)
                       join basketLine in _context.BasketLines on basket.Id equals basketLine.BasketId into basketLinesGroup
                       from basketLine in basketLinesGroup.DefaultIfEmpty()
                       join product in _context.Products on basketLine.ProductId equals product.Id into productGroup
                       from product in productGroup.DefaultIfEmpty()
                       select new
                       {
                           BasketId = basket.Id,
                           basket.UserId,
                           basket.CreatedAt,
                           basket.ExpiresAt,
                           BasketLineId = basketLine != null ? basketLine.Id : (Guid?)null,
                           BasketLineBasketId = basketLine != null ? basketLine.BasketId : (Guid?)null,
                           BasketLineProductId = basketLine != null ? basketLine.ProductId : (Guid?)null,
                           BasketLineQuantity = basketLine != null ? basketLine.Quantity : (int?)null,
                           ProductId = product != null ? product.Id : (Guid?)null,
                           ProductName = product != null ? product.Name : null,
                           ProductDescription = product != null ? product.Description : null,
                           ProductPrice = product != null ? product.Price : (decimal?)null,
                           ProductStock = product != null ? product.Stock : (int?)null
                       };

            var results = await query.ToListAsync(cancellationToken);

            if (!results.Any()) return null;

            var firstResult = results.First();
            var basketDto = new BasketDisplayDto
            {
                Id = firstResult.BasketId,
                UserId = firstResult.UserId,
                CreatedAt = firstResult.CreatedAt,
                ExpiresAt = firstResult.ExpiresAt,
                BasketLines = results.Where(x => x.BasketLineId.HasValue)
                    .Select(x => new BasketLineDisplayDto
                    {
                        Id = x.BasketLineId.Value,
                        BasketId = x.BasketLineBasketId.Value,
                        ProductId = x.BasketLineProductId.Value,
                        Quantity = x.BasketLineQuantity.Value,
                        Product = x.ProductId.HasValue ? new ProductDisplayDto
                        {
                            Id = x.ProductId.Value,
                            Name = x.ProductName,
                            Description = x.ProductDescription,
                            Price = x.ProductPrice.Value,
                            Stock = x.ProductStock.Value,
                            Images = new List<ProductImageDto>()
                        } : null
                    }).ToList()
            };

            // Load product images
            foreach (var basketLine in basketDto.BasketLines)
            {
                if (basketLine.Product != null)
                {
                    var images = await _context.ProductImages
                        .Where(pi => pi.ProductId == basketLine.ProductId)
                        .Select(pi => new ProductImageDto
                        {
                            Id = pi.Id,
                            ProductId = pi.ProductId,
                            ImageUrl = pi.Url,
                            SortOrder = pi.SortOrder,
                            IsPrimary = pi.IsPrimary
                        }).ToListAsync(cancellationToken);
                    
                    basketLine.Product.Images = images;
                }
            }

            return basketDto;
        }
    }
}
