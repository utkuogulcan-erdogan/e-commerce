using Core.Utilities.Results;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IOrderAddressService
    {
        Task<DataResult<List<OrderAddressDisplayDto>>> GetOrderAddressesByUserIdAsync(Guid orderId);
    }
}
