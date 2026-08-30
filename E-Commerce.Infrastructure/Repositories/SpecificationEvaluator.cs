using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications) where TEntity:BaseEntity<TKey>
        {
            var query = inputQuery;

            if (specifications.IncludeExpressions.Count>0)
            {
                query = specifications.IncludeExpressions.Aggregate(query,(current, expression)  => current.Include(expression) );
            }

            if (specifications.Criteria is not null)
              query = query.Where(specifications.Criteria);

            if (specifications.OrderBy is not null )
            {
                query = query.OrderBy(specifications.OrderBy);
            }

            if (specifications.OrderByDesc is not null)
            {
                query = query.OrderBy(specifications.OrderByDesc);
            }


            return query;
        } 
    }
}
