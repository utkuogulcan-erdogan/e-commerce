using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class OrderCreateDto : IDto
    {
        public Guid? ShippingAddressId { get; set; }
        public Guid? BillingAddressId { get; set; }
        public OrderAddressCreateDto? ShippingAddress { get; set; }
        public OrderAddressCreateDto? BillingAddress { get; set; }
    }
}
