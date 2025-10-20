using System;
using System.Linq.Expressions;
using Core.Entites;
using Core.Extensions;

namespace Core.Specifications
{
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : class, IEntity, new()
    {
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        protected BaseSpecification(Expression<Func<TEntity, bool>> initialCriteria = null)
        {
            Criteria = initialCriteria ?? (x => true); 
        }

        protected void AddCriteria(Expression<Func<TEntity, bool>> newCriteria)
        {
            if (newCriteria == null) return;
            Criteria = Criteria.AndAlso(newCriteria);  
        }
    }
}