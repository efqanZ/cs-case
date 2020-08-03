using System;
using CiSeCase.Core.Interfaces.Manager;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CiSeCase.Infrastructure.Managers.Cache
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly RedisServer _redisServer;
        public RedisCacheManager(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }
        public void Add(string key, object value)
        {
            string jsonData = JsonConvert.SerializeObject(value);
            _redisServer.Database.StringSet(key, jsonData);
        }

        public void Add(string key, object value, DateTime expired)
        {
            string jsonData = JsonConvert.SerializeObject(value);
            var expiry = expired.TimeOfDay - DateTime.Now.TimeOfDay;
            _redisServer.Database.StringSet(key, jsonData, expiry);
        }

        public bool Exists(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            if (Exists(key))
            {
                string jsonData = _redisServer.Database.StringGet(key);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            return default;
        }

        public void Remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }
    }
}