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
    public class OrderAddressSpecification : BaseSpecification<OrderAddress>
    {
        public OrderAddressSpecification(Guid? orderId = null,Guid? userId = null)
        {
            AddCriteria(BuildCriteria(orderId,userId));
        }
        private static Expression<Func<OrderAddress, bool>> BuildCriteria(Guid? orderId = null, Guid? userId = null)
        {
            if (orderId.HasValue)
                return oa => oa.OrderId == orderId.Value;
            if (userId.HasValue)
                return oa => oa.Order.UserId == userId.Value;
            return oa => true; // no filter
        }
    }
}
