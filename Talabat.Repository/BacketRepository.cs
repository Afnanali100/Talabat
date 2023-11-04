﻿using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Repository
{
    public class BacketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BacketRepository(IConnectionMultiplexer redis) { 
            _database=redis.GetDatabase();
        
        }


        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);

            return basket.IsNull? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBacketAsync(CustomerBasket customerBasket)
        {
            var UpdatedOrCreatedBasket = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(1));
            if (!UpdatedOrCreatedBasket) return null;


            return await GetBasketAsync(customerBasket.Id);
        }
    }
}
