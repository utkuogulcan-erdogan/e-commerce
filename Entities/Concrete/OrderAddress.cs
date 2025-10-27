using Core.Entites;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OrderAddress : IEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string AddressType { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public Order Order { get; set; }
        public static OrderAddress Create(Guid orderId, OrderAddressCreateDto orderAdressDto)
        {
            return new OrderAddress
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                AddressType = orderAdressDto.AddressType,
                Street = orderAdressDto.Street,
                City = orderAdressDto.City,
                Country = orderAdressDto.Country,
                PostalCode = orderAdressDto.PostalCode,
            };

        }
    }

}
