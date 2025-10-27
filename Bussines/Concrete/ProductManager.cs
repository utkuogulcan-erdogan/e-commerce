using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Entities.Specifications;
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

        public async Task<IResult> AddAsync(ProductAddDto productDto)
        {
            var product = Product.Create(
                productDto
            );

            await _productDal.AddAsync(product);
            return new SuccessResult("Product added successfully.");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            await _productDal.DeleteAsync(id);
            return new SuccessResult("Product deleted successfully.");
        }

        public async Task<IDataResult<List<Product>>> GetAllAsync()
        {
            var products = await _productDal.GetAllAsync();
            return new SuccessDataResult<List<Product>>(products, "Products listed successfully.");
        }

        public async Task<IDataResult<List<ProductDisplayDto>>> GetAllProductsAsync()
        {
            var products = await _productDal.GetAllProductsAsync();
            return new SuccessDataResult<List<ProductDisplayDto>>(products, "Products listed successfully.");
        }

        public async Task<IDataResult<Product>> GetProductByIdAsync(Guid productId)
        {
            var specification = new ProductSpecification(productId: productId);
            var product = await _productDal.GetProductAsync(specification);
            return new SuccessDataResult<Product>(product, "Product retrieved successfully.");
        }

        public async Task<IResult> UpdateAsync(Guid id, ProductUpdateDto productUpdateDto)
        {
            var existingProduct = await _productDal.GetAsync(p => p.Id == id);
            if (existingProduct == null)
            {
                return new ErrorResult("Product not found.");
            }

            var updatedProduct = Product.Update(existingProduct, productUpdateDto);
            await _productDal.UpdateAsync(existingProduct);
            return new SuccessResult("Product updated successfully.");
        }
    }
}
