using System;

namespace VidyaVahini.Infrastructure.Contracts
{
    public interface ICache
    {
        public T Get<T>(string key);

        public void Set(string key, object value, TimeSpan duration);

        public void Remove(string key);
    }
}
