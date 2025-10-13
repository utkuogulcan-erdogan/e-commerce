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
        IBasketLineDal _basketLine;
        IProductDal _productDal;
        IBasketDal _basketDal;
        public BasketLineManager(IBasketLineDal basketLine, IProductDal productDal, IBasketDal basketDal)
        {
            _basketLine = basketLine;
            _productDal = productDal;
            _basketDal = basketDal;
        }
        public async Task<IResult> AddProductToBasketAsync(Guid userId, Guid productId, int quantity)
        {
            var basket = await _basketDal.GetDetailedBasketByUserIdAsync(userId);
            var product = await _productDal.FindByIdAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            if (basket == null)
            {
                await _basketDal.AddAsync(new Basket { UserId = userId });
                basket = await _basketDal.GetDetailedBasketByUserIdAsync(userId);
            }
            var basketLine = (await _basketLine.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId)).FirstOrDefault();
            if (basketLine != null)
            {
                basketLine.Quantity += quantity;
                await _basketLine.UpdateAsync(basketLine);
            }
            else
            {
                basketLine = new BasketLine
                {
                    Id = Guid.NewGuid(),
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                await _basketLine.AddAsync(basketLine);
            }
            return new SuccessResult("Product added to basket.");
        }

        public async Task<IResult> RemoveProductFromBasketAsync(Guid userId, Guid productId)
        {
            var basket = _basketDal.GetDetailedBasketByUserIdAsync(userId).Result;
            var product = _productDal.FindByIdAsync(productId).Result;
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            if (basket == null)
            {
                throw new Exception("Basket not found");
            }
            var basketLine = (_basketLine.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId).Result).FirstOrDefault();
            if (basketLine != null)
            {
                await _basketLine.DeleteAsync(basketLine.Id);
                return new SuccessResult("Product removed from basket.");
            }
            else
            {
                throw new Exception("Product not found in basket");
            }
        }
        public Task<IResult> UpdateProductQuantityAsync(Guid userId, Guid productId, int quantity)
        {
            var basket = _basketDal.GetDetailedBasketByUserIdAsync(userId).Result;
            var product = _productDal.FindByIdAsync(productId).Result;
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            if (basket == null)
            {
                throw new Exception("Basket not found");
            }
            var basketLine = (_basketLine.GetAllAsync(bl => bl.BasketId == basket.Id && bl.ProductId == productId).Result).FirstOrDefault();
            if (basketLine != null)
            {
                basketLine.Quantity = quantity;
                _basketLine.UpdateAsync(basketLine);
                return Task.FromResult<IResult>(new SuccessResult("Product quantity updated."));
            }
            else
            {
                throw new Exception("Product not found in basket");

            }
        }
    }
}
