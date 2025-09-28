using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTO_s;
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

        public async Task<IDataResult<List<BasketDto>>> GetAllBasketsDetailedAsync()
        {
            return new SuccessDataResult<List<BasketDto>>(await _basketDal.GetAllBasketsDetailedAsync(), "Detailed baskets retrieved successfully.");
        }

        public async Task<IDataResult<BasketDto>> GetDetailedBasketByIdAsync(Guid id)
        {
            return new SuccessDataResult<BasketDto>(await _basketDal.GetDetailedBasketByIdAsync(id), "Basket retrieved successfully.");
        }

        public async Task<IDataResult<BasketDto>> GetDetailedBasketByUserIdAsync(Guid id)
        {
            return new SuccessDataResult<BasketDto>(await _basketDal.GetDetailedBasketByUserIdAsync(id), "Detailed basket retrieved successfully.");
        }

    }
}
