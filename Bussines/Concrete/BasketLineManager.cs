using Bussiness.Abstract;
using Core.Exceptions;
using Core.Utilities.Results;
using Entities.Concrete;
using Infrastructure.Persistence.Abstract;

namespace Bussiness.Concrete
{
    public class BasketLineManager : IBasketLineService
    {
        IBasketLineRepository _basketLineRepository;
        IProductRepository _productRepository;
        IBasketRepository _basketRepository;
        public BasketLineManager(IBasketLineRepository basketLineRepository, IProductRepository productRepository, IBasketRepository basketRepository)
        {
            _basketLineRepository = basketLineRepository;
            _productRepository = productRepository;
            _basketRepository = basketRepository;
        }
        public async Task<IResult> AddProductToBasketAsync(Guid userId, Guid productId, int quantity, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetAsync(basket => basket.UserId == userId, cancellationToken);
            var product = await _productRepository.GetAsync(product => product.Id == productId, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            if (basket == null)
            {
                var newBasket = Basket.Create(userId);
                await _basketRepository.AddAsync(newBasket, cancellationToken);
                basket = await _basketRepository.GetAsync(basket => basket.UserId == userId, cancellationToken);
            }
            var basketLine = (await _basketLineRepository.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId, cancellationToken)).FirstOrDefault();
            if (basketLine != null)
            {
                basketLine = BasketLine.UpdateQuantity(basketLine, basketLine.Quantity++);
                await _basketLineRepository.UpdateAsync(basketLine, cancellationToken);
            }
            else
            {
                var newBasketLine = BasketLine.Create(basket.Id, productId, quantity);
                await _basketLineRepository.AddAsync(newBasketLine, cancellationToken);
            }
            return new SuccessResult("Product added to basket.");
        }

        public async Task<IResult> RemoveProductFromBasketAsync(Guid userId, Guid productId, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetAsync(basketDal => basketDal.UserId == userId, cancellationToken);
            var product = await _productRepository.GetAsync(product => product.Id == productId, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            if (basket == null)
            {
                throw new NotFoundException("Basket not found");
            }
            var basketLine = (await _basketLineRepository.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId, cancellationToken)).FirstOrDefault();
            if (basketLine == null)
            {
                throw new BadRequestException("Product not found in basket");
            }
            await _basketLineRepository.DeleteAsync(basketLine.Id, cancellationToken);
            return new SuccessResult("Product removed from basket.");

        }
        public async Task<IResult> UpdateProductQuantityAsync(Guid userId, Guid productId, int quantity, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetAsync(basketDal => basketDal.UserId == userId, cancellationToken);
            var product = await _productRepository.GetAsync(product => product.Id == productId, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            if (basket == null)
            {
                throw new NotFoundException("Basket not found");
            }
            var basketLine = (await _basketLineRepository.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId, cancellationToken)).FirstOrDefault();
            if (basketLine == null)
            {
                throw new NotFoundException("Product not found in basket");
            }
            basketLine = BasketLine.UpdateQuantity(basketLine, quantity);
            await _basketLineRepository.UpdateAsync(basketLine, cancellationToken);
            return new SuccessResult ("Product quantity updated in basket.");
        }
    }
}
