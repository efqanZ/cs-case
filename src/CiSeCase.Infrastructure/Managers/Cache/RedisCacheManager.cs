using System;
using CiSeCase.Core.Interfaces.Manager;

namespace CiSeCase.Infrastructure.Managers.Cache
{
    public class RedisCacheManager : ICacheManager
    {
        //ToDo: Add redis client
        public RedisCacheManager()
        {

        }
        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Add(string key, object value, DateTime expired)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }
    }
}