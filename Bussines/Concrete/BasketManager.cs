using Bussiness.Abstract;
using Core.Specifications;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTO_s;
using Entities.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public async Task<IResult> AddAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var newBasket = Basket.Create(userId);
            await _basketDal.AddAsync(newBasket, cancellationToken);
            return new SuccessResult("Basket created successfully.");
        }

        public async Task<IDataResult<List<BasketDisplayDto>>> GetAllBasketsDetailedAsync(CancellationToken cancellationToken = default)
        {
            return new SuccessDataResult<List<BasketDisplayDto>>(await _basketDal.GetAllBasketsDetailedAsync(cancellationToken), "Detailed baskets retrieved successfully.");
        }

        public async Task<IDataResult<BasketDisplayDto>> GetDetailedBasketByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var specification = new BasketSpecification(basketId: id);
            return new SuccessDataResult<BasketDisplayDto>(await _basketDal.GetDetailedBasketAsync(specification, cancellationToken), "Basket retrieved successfully.");
        }

        public async Task<IDataResult<BasketDisplayDto>> GetDetailedBasketByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var specification = new BasketSpecification(userId: id);
            return new SuccessDataResult<BasketDisplayDto>(await _basketDal.GetDetailedBasketAsync(specification, cancellationToken), "Detailed basket retrieved successfully.");
        }

    }
}
