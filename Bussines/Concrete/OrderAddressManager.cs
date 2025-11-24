using Bussiness.Abstract;
using Core.Utilities.Results;
using Entities.DTO_s;
using Entities.Specifications;
using Infrastructure.Persistence.Abstract;

namespace Bussiness.Concrete
{
    public class OrderAddressManager : IOrderAddressService
    {
        IOrderAddressRepository _orderAddressRepository;
        public OrderAddressManager(IOrderAddressRepository orderAddressRepository)
        {
            _orderAddressRepository = orderAddressRepository;
        }
        public async Task<DataResult<List<OrderAddressDisplayDto>>> GetOrderAddressesByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var specification = new OrderAddressSpecification(userId: userId);
            return new SuccessDataResult<List<OrderAddressDisplayDto>>(await _orderAddressRepository.GetOrderAddressesAsync(specification, cancellationToken), "Order addresses retrieved successfully.");
        }

    }
}
