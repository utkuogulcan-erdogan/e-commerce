using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<TEntity> where TEntity : class, IEntity, new()
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
    }
}
