using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IResult> AddProductToBasketAsync(Guid userId, Guid productId, int quantity)
        {
            var basket = await _basketDal.GetAsync(basket => basket.UserId == userId);
            var product = await _productDal.GetAsync(product => product.Id == productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            if (basket == null)
            {
                var newBasket = Basket.CreateBasket(userId);
                await _basketDal.AddAsync(newBasket);
                basket = await _basketDal.GetAsync(basket => basket.UserId == userId);
            }
            var basketLine = (await _basketLineDal.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId)).FirstOrDefault();
            if (basketLine != null)
            {
                basketLine = BasketLine.UpdateQuantity(basketLine, basketLine.Quantity++);
                await _basketLineDal.UpdateAsync(basketLine);
            }
            else
            {
                var newBasketLine = BasketLine.CreateBasketLine(basket.Id, productId, quantity);
                await _basketLineDal.AddAsync(newBasketLine);
            }
            return new SuccessResult("Product added to basket.");
        }

        public async Task<IResult> RemoveProductFromBasketAsync(Guid userId, Guid productId)
        {
            var basket = await _basketDal.GetAsync(basketDal => basketDal.UserId == userId);
            var product = await _productDal.GetAsync(product => product.Id == productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            if (basket == null)
            {
                throw new Exception("Basket not found");
            }
            var basketLine = (await _basketLineDal.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId)).FirstOrDefault();
            if (basketLine == null)
            {
                throw new Exception("Product not found in basket");
            }
            await _basketLineDal.DeleteAsync(basketLine.Id);
            return new SuccessResult("Product removed from basket.");

        }
        public async Task<IResult> UpdateProductQuantityAsync(Guid userId, Guid productId, int quantity)
        {
            var basket = await _basketDal.GetAsync(basketDal => basketDal.UserId == userId);
            var product = await _productDal.GetAsync(product => product.Id == productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            if (basket == null)
            {
                throw new Exception("Basket not found");
            }
            var basketLine = (await _basketLineDal.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId)).FirstOrDefault();
            if (basketLine == null)
            {
                throw new Exception("Product not found in basket");
            }
            basketLine = BasketLine.UpdateQuantity(basketLine, quantity);
            await _basketLineDal.UpdateAsync(basketLine);
            return new SuccessResult ("Product quantity updated in basket.");
        }
    }
}
