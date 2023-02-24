using System.Linq;
using System.Threading.Tasks;

namespace Phoneshop.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get all items from the datasource.
        /// </summary>
        /// <returns>An IQueryable of the type used in the repository.</returns>
        public IQueryable<T> GetAll();

        public T GetById(int id);

        /// <summary>
        /// Creates an entity of the given type.
        /// Does not save changes on its own.
        /// </summary>
        /// <param name="entity"></param>
        public T Create(T entity);

        public Task<T> CreateAsync(T entity);

        public void Delete(int id);

        public Task DeleteAsync(int id);

        public void SaveChanges();

        public Task SaveChangesAsync();

    }
}
