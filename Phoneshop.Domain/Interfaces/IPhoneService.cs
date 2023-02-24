using Phoneshop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoneshop.Domain.Interfaces
{
    /// <summary>
    /// Retrieves phone objects.
    /// </summary>
    public interface IPhoneService
    {
        /// <summary>
        /// Passes a phone through validation before calling repository.
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Phone CreatePhone(Phone phone);

        public void DeletePhone(int id);

        /// <summary>
        /// Retrieves a single Phone object by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Phone GetPhone(int id);

        /// <summary>
        /// Retrieves a list of Phone objects.
        /// </summary>
        /// <returns></returns>
        public List<Phone> GetPhones();

        /// <summary>
        /// Searches in Description, Brand and Type based on the query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>A list of Phone objects.</returns>
        public List<Phone> Search(string query);

        public Task<List<Phone>> SearchAsync(string query);

        public Task<Phone> CreatePhoneAsync(Phone phone);

        public Task<Phone> GetPhoneAsync(int id);

        Task<(List<Phone>, PaginationMetaData)> SearchOrFilterAsync(
            string filter,
            string searchQuery,
            int pageNumber = 1,
            int pageSize = 10);
    }
}
