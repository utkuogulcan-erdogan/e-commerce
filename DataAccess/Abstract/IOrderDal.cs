using Core.DataAccess;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        public Task<List<OrderDisplayDto>> GetAllOrdersAsync();
        public Task<OrderDisplayDto> GetOrderByIdAsync(Guid id);
        public Task<List<OrderDisplayDto>> GetOrdersByUserIdAsync(Guid userId);
    }
}
