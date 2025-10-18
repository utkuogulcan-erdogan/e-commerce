using Core.Entites;
using Core.Utilities.Results;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid? CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<BasketLine> BasketLines { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }

        public static Product CreateProduct(ProductAddDto dto)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId,
                Slug = GenerateSlug(dto.Name),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };
        }

        public static Product UpdateProduct(Product product, ProductUpdateDto dto)
        {
            ArgumentNullException.ThrowIfNull(product);

            if (product == null)
            {
                return new ErrorResult("Product not found.");
            }

            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                product.Name = dto.Name;
            }

            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                product.Description = dto.Description;
            }

            if (dto.Price.HasValue)
            {
                product.Price = dto.Price.Value;
            }

            if (dto.Stock.HasValue)
            {
                product.Stock = dto.Stock.Value;
            }

            if (dto.CategoryId.HasValue && dto.CategoryId.Value != Guid.Empty)
            {
                product.CategoryId = dto.CategoryId;
            }

            product.UpdatedAt = DateTime.UtcNow;
            return product;
        }

        private static string GenerateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            return name.ToLowerInvariant()
                .Replace(" ", "-")
                .Replace("ı", "i")
                .Replace("ğ", "g")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("ö", "o")
                .Replace("ç", "c")
                .Trim();
        }
    }
}