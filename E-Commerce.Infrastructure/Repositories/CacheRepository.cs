using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase datadase;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            datadase=connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string cacheKey, CancellationToken ct = default)
        {
            var value = await datadase.StringGetAsync(cacheKey);
            return value.IsNullOrEmpty ?  null: value.ToString();
        }

        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan timeToLive, CancellationToken ct = default)
        {
            await datadase.StringSetAsync(cacheKey, cacheValue, timeToLive);
        }
    }
}
