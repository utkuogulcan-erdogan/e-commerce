using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderAddressDal : EfEntityRepositoryBase<OrderAddress, MyShopContext>, IOrderAddressDal
    {
        public EfOrderAddressDal(MyShopContext context) : base(context)
        {
        }

        public async Task<List<OrderAddressDisplayDto>> GetOrderAddressesByOrderIdAsync(Guid orderId)
        {
            var result = await (from address in _context.OrderAddresses
                                where address.OrderId == orderId
                                select new OrderAddressDisplayDto
                                {
                                    Id = address.Id,
                                    OrderId = address.OrderId,
                                    AddressType = address.AddressType,
                                    Street = address.Street,
                                    City = address.City,
                                    PostalCode = address.PostalCode,
                                    Country = address.Country
                                }).AsNoTracking().ToListAsync();
            return result;
        }
    }
}
