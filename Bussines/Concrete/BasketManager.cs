using Bussiness.Abstract;
using Core.Specifications;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO_s;
using Infrastructure.Persistence.Abstract;

namespace Bussiness.Concrete
{
    public class BasketManager : IBasketService
    {
        IBasketRepository _basketRepository;
        public BasketManager(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<IResult> AddAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var newBasket = Basket.Create(userId);
            await _basketRepository.AddAsync(newBasket, cancellationToken);
            return new SuccessResult("Basket created successfully.");
        }

        public async Task<IDataResult<List<BasketDisplayDto>>> GetAllBasketsDetailedAsync(CancellationToken cancellationToken = default)
        {
            return new SuccessDataResult<List<BasketDisplayDto>>(await _basketRepository.GetAllBasketsDetailedAsync(cancellationToken), "Detailed baskets retrieved successfully.");
        }

        public async Task<IDataResult<BasketDisplayDto>> GetDetailedBasketByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var specification = new BasketSpecification(basketId: id);
            return new SuccessDataResult<BasketDisplayDto>(await _basketRepository.GetDetailedBasketAsync(specification, cancellationToken), "Basket retrieved successfully.");
        }

        public async Task<IDataResult<BasketDisplayDto>> GetDetailedBasketByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var specification = new BasketSpecification(userId: id);
            return new SuccessDataResult<BasketDisplayDto>(await _basketRepository.GetDetailedBasketAsync(specification, cancellationToken), "Detailed basket retrieved successfully.");
        }

    }
}
