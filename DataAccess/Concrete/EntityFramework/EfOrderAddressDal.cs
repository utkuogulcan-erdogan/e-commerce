using Core.DataAccess.EntityFramework;
using Core.Specifications;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderAddressDal : EfEntityRepositoryBase<OrderAddress, MyShopContext>, IOrderAddressDal
    {
        public EfOrderAddressDal(MyShopContext context) : base(context)
        {
        }

        public async Task<List<OrderAddressDisplayDto>> GetOrderAddressesAsync(ISpecification<OrderAddress> specification)
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
                .ToListAsync();
        }
    }
}
