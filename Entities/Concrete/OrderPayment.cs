using Core.Entites;
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
    }
}
