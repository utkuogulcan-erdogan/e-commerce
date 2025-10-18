using Core.Entites;
using Entities.DTO_s;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
        public ICollection<OrderAddress> OrderAddresses { get; set; }
        public OrderPayment OrderPayment { get; set; }
        public static Order CreateOrder(Guid userId, Basket basket)
        {
            ArgumentNullException.ThrowIfNull(basket);

            return new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                OrderStatus = (int)Entities.Enums.OrderStatus.ReadyForPayment,
                TotalAmount = basket.BasketLines.Sum(bl => bl.Quantity * bl.Product.Price),
                OrderLines = basket.BasketLines.Select(bl => new OrderLine
                {
                    Id = Guid.NewGuid(),
                    ProductId = bl.ProductId,
                    ProductName = bl.Product.Name,
                    Quantity = bl.Quantity,
                    UnitPrice = bl.Product.Price,
                    LineTotal = bl.Quantity * bl.Product.Price
                }).ToList()
            };
        }

        public void AddOrderAddress(OrderAddress address)
        {
            address.OrderId = Id;
            OrderAddresses.Add(address);
        }

        public static Order UpdateOrderStatus(Order order, OrderUpdateStatusDto dto)
        {
            ArgumentNullException.ThrowIfNull(order);
            ArgumentNullException.ThrowIfNull(dto);
            order.OrderStatus = dto.Status;
            return order;
        }
    }
}
