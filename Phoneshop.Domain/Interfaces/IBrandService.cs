using Phoneshop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoneshop.Domain.Interfaces
{
    public interface IBrandService
    {
        /// <summary>
        /// Create a Brand with the name specified.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A Brand object, with the name "default" and no id
        /// if the repository returned null.</returns>
        public Brand CreateBrand(string name);

        /// <summary>
        /// Create a Brand with the name specified.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A Brand object, with the name "default" and no id
        /// if the repository returned null.</returns>
        public Task<Brand> CreateBrandAsync(string name);

        /// <summary>
        /// Attempt to find a Brand by integer id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Brand object, with the name "unknown" and no id
        /// if no matching Brand was found.</returns>
        public Brand GetBrand(int id);

        /// <summary>
        /// Attempt to find a Brand by integer id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Brand object, with the name "unknown" and no id
        /// if no matching Brand was found.</returns>
        public Task<Brand> GetBrandAsync(int id);

        /// <summary>
        /// Attempts to find a Brand by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>An integer id, or 0 if no Brand was found.</returns>
        public int GetBrandId(string name);

        public void DeleteBrand(int id);

        public Task DeleteBrandAsync(int id);

        public Task<List<Brand>> GetAllAsync();
    }
}
