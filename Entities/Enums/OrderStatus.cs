using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public enum OrderStatus
    {
        Pending = 0,
        ReadyForPayment = 1,
        Paid = 2,
        Shipped = 3,
        Completed = 4,
        Cancelled = 5,
        Failed = 6
    }
}
