using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public User User { get; set; } = new User();
        public ICollection<OrderLine> OrderLines { get; set; }
        public OrderAddress OrderAddress { get; set; }
        public OrderPayment OrderPayment { get; set; }
    }
}
