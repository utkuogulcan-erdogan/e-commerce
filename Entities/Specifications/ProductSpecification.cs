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
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(Guid? productId) { 
            if (productId.HasValue)
            {
                AddCriteria(p => p.Id == productId.Value);
            }
        }

        public static Expression<Func<Product, bool>> BuildCriteria(Guid? productId)
        {
            if (productId.HasValue)
                return p => p.Id == productId.Value;
            return p => true;
        }
    }
}
