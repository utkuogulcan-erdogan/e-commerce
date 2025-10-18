using Core.Entites;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OrderPayment : IEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Provider { get; set; }
        public string TransactionId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Order Order { get; set; }
        public static OrderPayment CreateOrderPayment(Guid orderId, OrderPaymentDto dto)
        {
            return new OrderPayment
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                Amount = dto.Amount,
                Provider = dto.Provider,
                TransactionId = dto.TransactionId,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

        }
    }
}
