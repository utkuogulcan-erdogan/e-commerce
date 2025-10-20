using Core.DataAccess;
using Core.Specifications;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOrderAddressDal : IEntityRepository<OrderAddress>
    {
        public Task<List<OrderAddressDisplayDto>> GetOrderAddressesAsync(ISpecification<OrderAddress> specification);
    }
}
