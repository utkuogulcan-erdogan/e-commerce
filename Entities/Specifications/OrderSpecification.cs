using Core.Specifications;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(Guid? orderId = null, Guid? userId = null) { 
            AddCriteria(BuildCriteria(orderId, userId));
        }

        private static Expression<Func<Order, bool>> BuildCriteria(Guid? orderId = null, Guid? userId = null)
        {
            if (orderId.HasValue && userId.HasValue)
                return o => o.Id == orderId.Value && o.UserId == userId.Value;
            if (orderId.HasValue)
                return o => o.Id == orderId.Value;
            if (userId.HasValue)
                return o => o.UserId == userId.Value;
            return o => true; 
        }
    }
}
