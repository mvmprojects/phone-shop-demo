using Microsoft.Extensions.Caching.Memory;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Phoneshop.Business
{
    [ExcludeFromCodeCoverage]
    public class Caching : ICaching
    {
        private MemoryCache _cache = new(new MemoryCacheOptions());
        private ConcurrentDictionary<object, SemaphoreSlim> _locks = new();

        public int SlidingExpSeconds { get; set; } = 30;
        public int AbsoluteExpSeconds { get; set; } = 60;

        public async Task<TItem> GetOrCreate<TItem>(
            string key,
            Func<Task<TItem>> createItem)
        {
            TItem item;

            if (!_cache.TryGetValue(key, out item))
            {
                SemaphoreSlim myLock = _locks.GetOrAdd(key, new SemaphoreSlim(1));

                await myLock.WaitAsync();

                try
                {
                    if (!_cache.TryGetValue(key, out item))
                    {
                        item = await createItem();

                        var policies = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(SlidingExpSeconds))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(AbsoluteExpSeconds));

                        _cache.Set(key, item, policies);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    myLock.Release();
                }
            }

            return item;
        }

        public void Delete(string key)
        {
            if (_cache.TryGetValue(key, out var value)) _cache.Remove(key);
        }

    }
}
