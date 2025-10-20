using Core.Entites;
using Entities.Concrete;
using System;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BasketSpecification : BaseSpecification<Basket>
    {
        public BasketSpecification(Guid? userId = null, Guid? basketId = null)
        {
            AddCriteria(BuildCriteria(userId, basketId));
        }

        private static Expression<Func<Basket, bool>> BuildCriteria(Guid? userId, Guid? basketId)
        {
            if (userId.HasValue && basketId.HasValue)
                return b => b.UserId == userId.Value && b.Id == basketId.Value;

            if (userId.HasValue)
                return b => b.UserId == userId.Value;

            if (basketId.HasValue)
                return b => b.Id == basketId.Value;

            return b => true; // no filter
        }
    }
}