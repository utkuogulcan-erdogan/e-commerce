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

        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new SuccessResult("Product added successfully.");
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult("Product deleted successfully.");
        }

        public IDataResult<List<Product>> GetAll()
        {
            var products = _productDal.GetAll();
            return new SuccessDataResult<List<Product>>(products, "Products listed successfully.");
        }

        public async Task<IDataResult<List<Product>>> GetAllProductsAsync()
        {
            var products = await _productDal.GetAllProductsAsync();
            return new SuccessDataResult<List<Product>>(products, "Products listed successfully.");
        }

        public IDataResult<Product> GetById(Guid productId)
        {
            var product = _productDal.Get(p => p.Id == productId);
            return new SuccessDataResult<Product>(product, "Product retrieved successfully.");
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult("Product updated successfully.");
        }
    }
}
