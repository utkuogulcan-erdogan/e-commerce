using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
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
    public class OrderAddressManager : IOrderAddressService
    {
        IOrderAddressDal _orderAddressDal;
        public OrderAddressManager(IOrderAddressDal orderAddressDal)
        {
            _orderAddressDal = orderAddressDal;
        }
        public async Task<DataResult<List<OrderAddressDisplayDto>>> GetOrderAddressesByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var specification = new OrderAddressSpecification(userId: userId);
            return new SuccessDataResult<List<OrderAddressDisplayDto>>(await _orderAddressDal.GetOrderAddressesAsync(specification, cancellationToken), "Order addresses retrieved successfully.");
        }

    }
}
