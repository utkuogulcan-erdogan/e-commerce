using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<IResult> Add(ProductAddDto productDto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
                Slug = GenerateSlug(productDto.Name),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            await _productDal.AddAsync(product);
            return new SuccessResult("Product added successfully.");
        }

        public async Task<IResult> Delete(Guid id)
        {
            await _productDal.DeleteAsync(id);
            return new SuccessResult("Product deleted successfully.");
        }

        public async Task<IDataResult<List<Product>>> GetAll()
        {
            var products = await _productDal.GetAllAsync();
            return new SuccessDataResult<List<Product>>(products, "Products listed successfully.");
        }

        public async Task<IDataResult<List<ProductDisplayDto>>> GetAllProductsAsync()
        {
            var products = await _productDal.GetAllProductsAsync();
            return new SuccessDataResult<List<ProductDisplayDto>>(products, "Products listed successfully.");
        }

        public async Task<IDataResult<Product>> GetById(Guid productId)
        {
            var product = await _productDal.FindByIdAsync(productId);
            return new SuccessDataResult<Product>(product, "Product retrieved successfully.");
        }

        public async Task<IResult> Update(Guid id, ProductUpdateDto productUpdateDto)
        {
            var existingProduct = await _productDal.GetAsync(p => p.Id == id);
            if (existingProduct == null)
            {
                return new ErrorResult("Product not found.");
            }

            if (!string.IsNullOrWhiteSpace(productUpdateDto.Name))
            {
                existingProduct.Name = productUpdateDto.Name;
            }

            if (!string.IsNullOrWhiteSpace(productUpdateDto.Description))
            {
                existingProduct.Description = productUpdateDto.Description;
            }

            if (productUpdateDto.Price.HasValue)
            {
                existingProduct.Price = productUpdateDto.Price.Value;
            }

            if (productUpdateDto.Stock.HasValue)
            {
                existingProduct.Stock = productUpdateDto.Stock.Value;
            }

            if (productUpdateDto.CategoryId.HasValue && productUpdateDto.CategoryId.Value != Guid.Empty)
            {
                existingProduct.CategoryId = productUpdateDto.CategoryId;
            }

            existingProduct.UpdatedAt = DateTime.UtcNow;

            await _productDal.UpdateAsync(existingProduct);
            return new SuccessResult("Product updated successfully.");
        }
        private string GenerateSlug(string name)
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
