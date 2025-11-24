using Core.DataAccess.EntityFramework;
using Core.Specifications;
using Entities.Concrete;
using Entities.DTO_s;
using Infrastructure.Persistence.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Persistence.Concrete.EntityFramework
{
    public class EfOrderAddressRepository : EfEntityRepositoryBase<OrderAddress, MyShopContext>, IOrderAddressRepository
    {
        public EfOrderAddressRepository(MyShopContext context) : base(context)
        {
        }

        public async Task<List<OrderAddressDisplayDto>> GetOrderAddressesAsync(ISpecification<OrderAddress> specification, CancellationToken cancellationToken = default)
        {
            return await _context.Set<OrderAddress>()
                .AsNoTracking()
                .Where(specification.Criteria)
                .Select(orderAddress => new OrderAddressDisplayDto
                {
                    Id = orderAddress.Id,
                    OrderId = orderAddress.OrderId,
                    AddressType = orderAddress.AddressType,
                    Street = orderAddress.Street,
                    City = orderAddress.City,
                    PostalCode = orderAddress.PostalCode,
                    Country = orderAddress.Country
                })
                .ToListAsync(cancellationToken);
        }
    }
}
