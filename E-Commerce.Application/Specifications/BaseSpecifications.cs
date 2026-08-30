using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; private set; } = [];

        public Expression<Func<TEntity, bool>> Criteria {  get; private set; }

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }

        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public void AddInclude (Expression<Func<TEntity,object>> expression)
        {
            IncludeExpressions.Add(expression);

        }

        public void AddOrderBy (Expression<Func<TEntity, object>>? orderby)
        {
            orderby = OrderBy;
        }

        public void AddOrderByDesc(Expression<Func<TEntity, object>>? orderbydesc)
        {
            orderbydesc = OrderByDesc;
        }
    }
}
