using System;
using System.Threading.Tasks;

namespace Phoneshop.Domain.Interfaces
{
    public interface ICaching
    {
        public int SlidingExpSeconds { get; set; }
        public int AbsoluteExpSeconds { get; set; }

        Task<TItem> GetOrCreate<TItem>(
            string key,
            Func<Task<TItem>> createItem);

        void Delete(string key);
    }
}
