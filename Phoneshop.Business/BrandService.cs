using Microsoft.EntityFrameworkCore;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoneshop.Business
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _repo;
        private readonly ICaching _caching;

        #region Constructors
        public BrandService(
            IRepository<Brand> repository,
            ICaching cache)
        {
            _repo = repository;
            _caching = cache;
        }
        #endregion

        public Brand CreateBrand(string name)
        {
            var defaultBrand = new Brand() { Name = "default" };

            bool alreadyExists = (GetBrandId(name) > 0);

            if (alreadyExists) return defaultBrand;

            var result = _repo
                .Create(new Brand() { Name = name });
            return result ?? defaultBrand;
        }

        public async Task<Brand> CreateBrandAsync(string name)
        {
            int id = GetBrandId(name);

            if (id > 0) return await GetBrandAsync(id);

            var result = await _repo.CreateAsync(new Brand() { Name = name });
            await _repo.SaveChangesAsync();

            return result;
        }

        public int GetBrandId(string name)
        {
            var result = _repo.GetAll().Where(b => b.Name == name).SingleOrDefault();

            return result != null ? result.Id : 0;
        }

        public Brand GetBrand(int id)
        {
            Brand result = _repo.GetById(id);

            return result ?? new Brand() { Name = "unknown" };
        }

        public Task<Brand> GetBrandAsync(int id)
        {
            //var result = await _repo.GetAll().Where(x => x.Id == id)
            //    .SingleOrDefaultAsync();
            //return result;

            return _caching.GetOrCreate(
                "brand" + id.ToString(),
                () => _repo.GetAll().Where(x => x.Id == id).SingleOrDefaultAsync());
        }

        public Task<List<Brand>> GetAllAsync()
        {
            return _caching.GetOrCreate(
                "brands",
                () => _repo.GetAll().ToListAsync());
        }

        public void DeleteBrand(int id)
        {
            if (id <= 0)
            {
                ThrowArgumentOutOfRangeEx(id);
            }
            _repo.Delete(id);
        }

        public Task DeleteBrandAsync(int id)
        {
            if (id <= 0)
            {
                ThrowArgumentOutOfRangeEx(id);
            }
            return _repo.DeleteAsync(id);
        }

        private static void ThrowArgumentOutOfRangeEx(int id)
        {
            throw new ArgumentOutOfRangeException(id.ToString(), "Input cannot be negative or zero.");
        }
    }
}
