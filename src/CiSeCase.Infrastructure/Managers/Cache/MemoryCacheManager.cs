using System;
using CiSeCase.Core.Interfaces.Manager;
using Microsoft.Extensions.Caching.Memory;

namespace CiSeCase.Infrastructure.Managers.Cache
{
    public class MemoryCacheManager : ICacheManager
    {
        readonly IMemoryCache _cache;
        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void Add(string key, object value, DateTime expired)
        {
            _cache.Set(key, value, expired);
        }

        public void Add(string key, object value)
        {
            _cache.Set(key, value);
        }

        public bool Exists(string key)
        {
            if (_cache.TryGetValue(key, out object value))
                return true;

            return false;
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}