using Core.Utilities.Results;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IOrderService
    {
        Task<IDataResult<List<OrderDisplayDto>>> GetAllAsync();
        Task<IDataResult<OrderDisplayDto>> GetOrderByIdAsync(Guid id);
        Task<IDataResult<List<OrderDisplayDto>>> GetOrdersByUserIdAsync(Guid userId);
        Task<IResult> CreateOrderAsync(Guid userId, OrderCreateDto dto);
        Task<IResult> UpdateOrderStatusAsync(Guid userId,OrderUpdateStatusDto dto);
        Task<IResult> CreatePaymentAsync(Guid userId, OrderPaymentDto dto);

    }
}
