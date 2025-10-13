using Core.Entites;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class OrderDisplayDto : IDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderLineDisplayDto> OrderLines { get; set; }
        public OrderAddressDisplayDto OrderShippingAddress { get; set; }
        public OrderAddressDisplayDto OrderBillingAddress { get; set; }
        public OrderPaymentDisplayDto OrderPayment{ get; set; }
    }
}
