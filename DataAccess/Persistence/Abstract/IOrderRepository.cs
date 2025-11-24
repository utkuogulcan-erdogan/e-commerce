using Core.DataAccess;
using Core.Specifications;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Abstract
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
        public Task<List<OrderDisplayDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
        public Task<OrderDisplayDto> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<List<OrderDisplayDto>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
