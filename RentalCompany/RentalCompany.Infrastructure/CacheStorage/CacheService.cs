using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RentalCompany.Core.CacheStorage;
using RentalCompany.Core.Entities;
using System.Runtime.Caching;
using MemoryCache = Microsoft.Extensions.Caching.Memory.MemoryCache;

namespace RentalCompany.Infrastructure.CacheStorage
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private const string _cacheKey = "MotorcycleList";

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public List<Motorcycle> GetMotorcycleList()
        {
            var result = _cache.Get<List<Motorcycle>>(_cacheKey);

            if (result == null) return new List<Motorcycle>();

            return result;
        }

        public void SetCache(string key, List<Motorcycle> data)
        {
            _cache.Set(key, data);
        }
    }
}
