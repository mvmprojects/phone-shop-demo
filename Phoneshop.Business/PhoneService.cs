using Microsoft.EntityFrameworkCore;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Phoneshop.Business
{
    public class PhoneService : IPhoneService
    {
        private readonly IRepository<Phone> _repo;
        private readonly IBrandService _brandService;
        private readonly IBasicLogger _logger;

        #region Constructors
        public PhoneService(
            IRepository<Phone> repository,
            IBrandService brandService,
            IBasicLogger logger = null)
        {
            _repo = repository;
            _brandService = brandService;
            _logger = logger ?? new EmptyLogger();
        }
        #endregion

        public Phone CreatePhone(Phone phone)
        {
            bool hasIssues = false;
            hasIssues = ValidatePhone(phone, hasIssues);
            if (hasIssues) return phone;

            Brand retrievedBrand = new() { Id = 0 };

            int brandId = _brandService.GetBrandId(phone.Brand.Name);

            // get object if name exists and let EF track the right entity
            if (brandId > 0)
            {
                retrievedBrand = _brandService.GetBrand(brandId);
            }

            if (retrievedBrand.Id > 0)
            {
                _logger.LogInfo($"Creating {phone.Brand.Name} - {phone.Type}");

                phone.BrandId = retrievedBrand.Id;
                phone.Brand = retrievedBrand;
                phone = _repo.Create(phone);
                _repo.SaveChanges();
            }
            // let EF insert the Brand instead
            else
            {
                _logger.LogInfo($"Letting EF create the new brand {phone.Brand.Name}");
                phone = _repo.Create(phone);
                _repo.SaveChanges();
            }

            return phone;
        }

        private bool ValidatePhone(Phone phone, bool hasIssues)
        {
            bool noType = string.IsNullOrEmpty(phone.Type);
            bool noBrand = string.IsNullOrEmpty(phone.Brand?.Name);

            if (noType || noBrand)
            {
                Console.WriteLine("Phone missing data: " +
                $"{(noType ? " No type found." : "")}" +
                $"{(noBrand ? " No brand found." : "")}");
                hasIssues = true;
            }

            // whether or not a phone object with a stock of 0 should be refused
            // is debatable. the caller might want to import a phone that is
            // physically delayed but definitely on its way to stores.
            bool badPrice = (phone.Price <= 0);
            bool badStock = (phone.Stock < 0);

            if (badPrice || badStock)
            {
                Console.WriteLine("Phone has invalid data: " +
                $"{(badPrice ? " Price cannot be 0 or negative." : "")}" +
                $"{(badStock ? " Stock cannot be negative." : "")}");
                hasIssues = true;
            }

            bool hasExistingCombo = GetPhones().Any(x =>
            x.Brand.Name == phone.Brand.Name &&
            x.Type == phone.Type);

            if (hasExistingCombo)
            {
                Console.WriteLine($"Validation failed: " +
                    $"combination of {phone.Brand.Name} and {phone.Type} already exists.");
                hasIssues = true;
            }

            return hasIssues;
        }

        public Phone GetPhone(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Exception due to invalid phone id ({id}).");
                throw new ArgumentOutOfRangeException(
                    id.ToString(), "Input cannot be negative or zero.");
            }

            return _repo.GetAll().Include(p => p.Brand).Where(x => x.Id == id).SingleOrDefault();
        }

        public List<Phone> GetPhones()
        {
            // NOTE this is inefficient and should be replaced by aggregate root design pattern etc.
            // wish list: move OrderBy input to a(n optional) resource param provided by caller
            return _repo.GetAll().Include(p => p.Brand).OrderBy(phone => phone.Brand.Name).ToList();
        }

        public List<Phone> Search(string query)
        {
            _logger.LogInfo($"Using search with query: {query}");
            var lowerQuery = (query == null) ? string.Empty : query.ToLower();

            return _repo.GetAll().Include(p => p.Brand).Where(
                x => x.Type.ToLower().Contains(lowerQuery)
                     || (x.Brand != null && x.Brand.Name.ToLower().Contains(lowerQuery))
                     || x.Description.ToLower().Contains(lowerQuery)).ToList();
        }

        public void DeletePhone(int id)
        {
            if (id > 0)
            {
                _logger.LogInfo($"Deleting phone (id {id})");
                _repo.Delete(id);
            }
            else Debug.WriteLine($"Argument out of range for DeletePhone (id {id})");
        }

        public async Task<Phone> CreatePhoneAsync(Phone phone)
        {
            bool hasIssues = false;
            hasIssues = ValidatePhone(phone, hasIssues);
            if (hasIssues) return phone;

            Brand retrievedBrand = new() { Id = 0 };

            int brandId = _brandService.GetBrandId(phone.Brand.Name);

            // get object if name exists and let EF track the right entity
            if (brandId > 0)
            {
                retrievedBrand = _brandService.GetBrand(brandId);
            }

            if (retrievedBrand.Id > 0)
            {
                _logger.LogInfo($"Creating {phone.Brand.Name} - {phone.Type}");

                phone.BrandId = retrievedBrand.Id;
                phone.Brand = retrievedBrand;

                phone = await _repo.CreateAsync(phone);
                await _repo.SaveChangesAsync();
            }
            // let EF insert the Brand instead
            else
            {
                _logger.LogInfo($"Letting EF create the new brand {phone.Brand.Name}");

                phone = await _repo.CreateAsync(phone);
                await _repo.SaveChangesAsync();
            }

            return phone;
        }

        public Task<List<Phone>> SearchAsync(string query)
        {
            var lowerQuery = (query == null) ? string.Empty : query.ToLower();

            return _repo.GetAll().Include(p => p.Brand).Where(
                x => x.Type.ToLower().Contains(lowerQuery)
                     || (x.Brand != null && x.Brand.Name.ToLower().Contains(lowerQuery))
                     || x.Description.ToLower().Contains(lowerQuery)).ToListAsync();
        }

        public Task<Phone> GetPhoneAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Exception due to invalid phone id ({id}).");
                throw new ArgumentOutOfRangeException(
                    id.ToString(), "Input cannot be negative or zero.");
            }

            return _repo.GetAll().Include(p => p.Brand).Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

        // method with many parameters supplied by the api's consumer
        // that can also return paging metadata
        public async Task<(List<Phone>, PaginationMetaData)> SearchOrFilterAsync(
            string filter,
            string searchQuery,
            int pageNumber,
            int pageSize)
        {
            // get queryable first
            var collection = _repo.GetAll();

            // attempt to add filter (just on Brand name for now)
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.Trim();
                collection = collection
                    .Where(p => p.Brand.Name == filter);
            }

            // attempt to add search term
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim().ToLower();
                collection = collection
                    .Where(
                        x => x.Type.ToLower().Contains(searchQuery)
                     || (x.Brand != null && x.Brand.Name.ToLower().Contains(searchQuery))
                     || x.Description.ToLower().Contains(searchQuery))
                    .OrderBy(p => p.Brand.Name);
            }

            var totalItemCount = await collection.CountAsync();

            var pMetaData = new PaginationMetaData(
                totalItemCount, pageSize, pageNumber);

            var returnCollection = await collection
                .Skip(pageSize * (pageNumber - 1)) // skip previous page(s)
                .Take(pageSize) // take size allowed per page
                .Include(p => p.Brand).ToListAsync();

            return (returnCollection, pMetaData);
        }
    }
}
