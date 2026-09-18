using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase database;
        public BasketRepository(IConnectionMultiplexer connection) 
        {
            database = connection.GetDatabase();
        }


        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(basket);
            var success = await database.StringSetAsync(basket.Id, json, timeToLive?? TimeSpan.FromDays(30));
            return success ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
           return await database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
           var basket = await database.StringGetAsync(basketId);
            if (basket.IsNullOrEmpty)  return null;
            
            return JsonSerializer.Deserialize<CustomerBasket>(basket.ToString());
        }
    }
}
