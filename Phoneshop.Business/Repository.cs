using Phoneshop.Domain.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Phoneshop.Business
{
    [ExcludeFromCodeCoverage]
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            //_context.SaveChanges(); // do this in service
            return entity;
        }

        // AddAsync's method summary seems to advise against
        // using AddAsync unless absolutely necessary.
        // But no other warnings are given at the time
        // of writing (2022-10-14).
        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            //await _context.SaveChangesAsync(); // do this in service
            return entity;
        }

        public void Delete(int id)
        {
            _context.Set<T>().Remove(GetById(id));
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Remove(GetById(id));
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        //public async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
