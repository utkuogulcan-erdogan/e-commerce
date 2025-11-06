using Core.Utilities.Results;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IOrderService
    {
        Task<IDataResult<List<OrderDisplayDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IDataResult<OrderDisplayDto>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IDataResult<List<OrderDisplayDto>>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IResult> CreateOrderAsync(Guid userId, OrderCreateDto dto, CancellationToken cancellationToken = default);
        Task<IResult> UpdateOrderStatusAsync(Guid userId, OrderUpdateStatusDto dto, CancellationToken cancellationToken = default);
        Task<IResult> CreatePaymentAsync(Guid userId, OrderPaymentDto dto, CancellationToken cancellationToken = default);

    }
}
