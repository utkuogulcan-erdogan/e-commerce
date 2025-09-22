using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class BasketManager : IBasketService
    {
        IBasketDal _basketDal;
        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }
        public async Task<IResult> Add(Guid userId)
        {
            await _basketDal.AddAsync(new Basket { UserId = userId });
            return new SuccessResult("Basket created successfully.");
        }

        public async Task<IResult> Delete(Guid id)
        {
            await _basketDal.DeleteAsync(id);
            return new SuccessResult("Basket deleted successfully.");
        }

        public async Task<IDataResult<List<Basket>>> GetAll()
        {
            var results = await _basketDal.GetAllAsync();
            return new SuccessDataResult<List<Basket>>(results, "Baskets listed successfully.");
        }

        public async Task<IDataResult<List<Basket>>> GetAllBasketsDetailedAsync()
        {
            return new SuccessDataResult<List<Basket>>(await _basketDal.GetAllBasketsDetailedAsync(), "Detailed baskets retrieved successfully.");
        }

        public async Task<IDataResult<Basket>> GetById(Guid id)
        {
            return new SuccessDataResult<Basket>(await _basketDal.GetAsync(b => b.Id == id), "Basket retrieved successfully.");
        }

        public async Task<IDataResult<Basket>> GetByUserId(Guid userId)
        {
            return new SuccessDataResult<Basket>(await _basketDal.GetAsync(b =>b.User.Id == userId), "Basket retrieved successfully.");
        }

        public async Task<IDataResult<Basket>> GetDetailedBasketByUserIdAsync(Guid id)
        {
            return new SuccessDataResult<Basket>(await _basketDal.GetDetailedBasketByUserIdAsync(id), "Detailed basket retrieved successfully.");
        }

        public async Task<IResult> Update(Basket basket)
        {
            await _basketDal.UpdateAsync(basket);
            return new SuccessResult("Basket updated successfully.");
        }
    }
}
