using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext dbContext;
        public GenericRepository(StoreDbContext dbContext)
        {
            this.dbContext= dbContext;
        }

        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }
        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync(ct);  
        }

        public async Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default)
        {
            return await dbContext.Set<TEntity>().FindAsync(id, ct);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationsAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default)
        {
            var result = SpecificationEvaluator.CreateQuery<TEntity, Tkey>(dbContext.Set<TEntity>(),specifications);
            return await result.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdWithSpecificationsAsync(ISpecifications<TEntity, Tkey> specifications, CancellationToken ct = default)
        {
            var result = SpecificationEvaluator.CreateQuery<TEntity, Tkey>(dbContext.Set<TEntity>(), specifications);
            return await result.FirstOrDefaultAsync(ct);
        }
    }
}
