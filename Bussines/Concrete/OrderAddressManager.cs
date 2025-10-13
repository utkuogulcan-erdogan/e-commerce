using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<DataResult<List<OrderAddressDisplayDto>>> GetOrderAddressesByOrderIdAsync(Guid userId)
        {
            return new SuccessDataResult<List<OrderAddressDisplayDto>>(await _orderAddressDal.GetOrderAddressesByOrderIdAsync(userId), "Order addresses retrieved successfully.");
        }

    }
}
