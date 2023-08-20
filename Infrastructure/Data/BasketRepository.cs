using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var FoundBasket = await _database.StringGetAsync(id);
            var jsonbasket = JsonSerializer.Deserialize<CustomerBasket>(FoundBasket!);
        

            
            return FoundBasket.IsNullOrEmpty ? null : jsonbasket;
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket newbasket)
        {
            var created = await _database.StringSetAsync(newbasket.Id, JsonSerializer.Serialize(newbasket), TimeSpan.FromDays(1));
            if (!created) { return null; }
            return newbasket;
        }
    }
}
