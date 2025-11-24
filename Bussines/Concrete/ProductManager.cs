using Bussiness.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO_s;
using Entities.Specifications;
using Infrastructure.Persistence.Abstract;

namespace Bussiness.Concrete
{
    public class ProductManager : IProductService
    {
        IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IResult> AddAsync(ProductAddDto productDto, CancellationToken cancellationToken = default)
        {
            var product = Product.Create(
                productDto
            );

            await _productRepository.AddAsync(product, cancellationToken);
            return new SuccessResult("Product added successfully.");
        }

        public async Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _productRepository.DeleteAsync(id, cancellationToken);
            return new SuccessResult("Product deleted successfully.");
        }

        public async Task<IDataResult<List<Product>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAllAsync(null, cancellationToken);
            return new SuccessDataResult<List<Product>>(products, "Products listed successfully.");
        }

        public async Task<IDataResult<List<ProductDisplayDto>>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAllProductsAsync(cancellationToken);
            return new SuccessDataResult<List<ProductDisplayDto>>(products, "Products listed successfully.");
        }

        public async Task<IDataResult<Product>> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var specification = new ProductSpecification(productId: productId);
            var product = await _productRepository.GetProductAsync(specification, cancellationToken);
            return new SuccessDataResult<Product>(product, "Product retrieved successfully.");
        }

        public async Task<IResult> UpdateAsync(Guid id, ProductUpdateDto productUpdateDto, CancellationToken cancellationToken = default)
        {
            var existingProduct = await _productRepository.GetAsync(p => p.Id == id, cancellationToken);
            if (existingProduct == null)
            {
                return new ErrorResult("Product not found.");
            }

            var updatedProduct = Product.Update(existingProduct, productUpdateDto);
            await _productRepository.UpdateAsync(existingProduct, cancellationToken);
            return new SuccessResult("Product updated successfully.");
        }
    }
}
