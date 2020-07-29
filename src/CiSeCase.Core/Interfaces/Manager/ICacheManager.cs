using System;

namespace CiSeCase.Core.Interfaces.Manager
{
    public interface ICacheManager
    {
        void Add(string key, object value);

        void Add(string key, object value, DateTime expired);

        T Get<T>(string key);

        bool Exists(string key);

        void Remove(string key);
    }
}