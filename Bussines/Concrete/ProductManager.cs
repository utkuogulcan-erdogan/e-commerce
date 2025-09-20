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

        public async Task<IResult> Add(Product product)
        {
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

        public async Task<IDataResult<List<Product>>> GetAllProductsAsync()
        {
            var products = await _productDal.GetAllProductsAsync();
            return new SuccessDataResult<List<Product>>(products, "Products listed successfully.");
        }

        public async Task<IDataResult<Product>> GetById(Guid productId)
        {
            var product = await _productDal.FindByIdAsync(productId);
            return new SuccessDataResult<Product>(product, "Product retrieved successfully.");
        }

        public async Task<IResult> Update(Product product)
        {
            await _productDal.UpdateAsync(product);
            return new SuccessResult("Product updated successfully.");
        }
    }
}
