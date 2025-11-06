using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class BasketLineManager : IBasketLineService
    {
        IBasketLineDal _basketLineDal;
        IProductDal _productDal;
        IBasketDal _basketDal;
        public BasketLineManager(IBasketLineDal basketLine, IProductDal productDal, IBasketDal basketDal)
        {
            _basketLineDal = basketLine;
            _productDal = productDal;
            _basketDal = basketDal;
        }
        public async Task<IResult> AddProductToBasketAsync(Guid userId, Guid productId, int quantity, CancellationToken cancellationToken = default)
        {
            var basket = await _basketDal.GetAsync(basket => basket.UserId == userId, cancellationToken);
            var product = await _productDal.GetAsync(product => product.Id == productId, cancellationToken);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            if (basket == null)
            {
                var newBasket = Basket.Create(userId);
                await _basketDal.AddAsync(newBasket, cancellationToken);
                basket = await _basketDal.GetAsync(basket => basket.UserId == userId, cancellationToken);
            }
            var basketLine = (await _basketLineDal.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId, cancellationToken)).FirstOrDefault();
            if (basketLine != null)
            {
                basketLine = BasketLine.UpdateQuantity(basketLine, basketLine.Quantity++);
                await _basketLineDal.UpdateAsync(basketLine, cancellationToken);
            }
            else
            {
                var newBasketLine = BasketLine.Create(basket.Id, productId, quantity);
                await _basketLineDal.AddAsync(newBasketLine, cancellationToken);
            }
            return new SuccessResult("Product added to basket.");
        }

        public async Task<IResult> RemoveProductFromBasketAsync(Guid userId, Guid productId, CancellationToken cancellationToken = default)
        {
            var basket = await _basketDal.GetAsync(basketDal => basketDal.UserId == userId, cancellationToken);
            var product = await _productDal.GetAsync(product => product.Id == productId, cancellationToken);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            if (basket == null)
            {
                throw new Exception("Basket not found");
            }
            var basketLine = (await _basketLineDal.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId, cancellationToken)).FirstOrDefault();
            if (basketLine == null)
            {
                throw new Exception("Product not found in basket");
            }
            await _basketLineDal.DeleteAsync(basketLine.Id, cancellationToken);
            return new SuccessResult("Product removed from basket.");

        }
        public async Task<IResult> UpdateProductQuantityAsync(Guid userId, Guid productId, int quantity, CancellationToken cancellationToken = default)
        {
            var basket = await _basketDal.GetAsync(basketDal => basketDal.UserId == userId, cancellationToken);
            var product = await _productDal.GetAsync(product => product.Id == productId, cancellationToken);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            if (basket == null)
            {
                throw new Exception("Basket not found");
            }
            var basketLine = (await _basketLineDal.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId, cancellationToken)).FirstOrDefault();
            if (basketLine == null)
            {
                throw new Exception("Product not found in basket");
            }
            basketLine = BasketLine.UpdateQuantity(basketLine, quantity);
            await _basketLineDal.UpdateAsync(basketLine, cancellationToken);
            return new SuccessResult ("Product quantity updated in basket.");
        }
    }
}
