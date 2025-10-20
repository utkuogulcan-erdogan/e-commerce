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
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string? email) { 
            AddCriteria(BuildCriteria(email));
        }

        public static Expression<Func<User, bool>> BuildCriteria(string? email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return u => u.Email == email;
            }
            return u => true;
        }
    }
}
