using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace PhoneServiceTests
{
    public class TestBase
    {
        public PhoneService _phoneService;

        public Mock<IRepository<Phone>> _mockPhoneRepo;
        public Mock<IBrandService> _mockBrandService;
        public Mock<IBasicLogger> _mockBasicLogger;

        public List<Phone> _dummyData;

        public TestBase()
        {
            _dummyData = new List<Phone>()
            {
                new ()
                {
                    Id = 1,
                    Brand = new Brand() { Name = "A" },
                    Type = "existingType",
                    Description = "abc",
                    Price = 1,
                    Stock = 1
                },
                new ()
                {
                    Id = 2,
                    Brand = new Brand() { Name = "B" },
                    Type = "abc",
                    Description = "abc",
                    Price = 1,
                    Stock = 1
                },
                new ()
                {
                    Id = 3,
                    Brand = new Brand() { Name = "C" },
                    Type = "abc",
                    Description = "abc",
                    Price = 1,
                    Stock = 1
                },
                new ()
                {
                    Id = 4,
                    Brand = new Brand() { Name = "Google" },
                    Type = "abc",
                    Description = "abc",
                    Price = 1,
                    Stock = 1
                },
                new ()
                {
                    Id = 5,
                    Brand = new Brand() { Name = "Google" },
                    Type = "abcd",
                    Description = "abc",
                    Price = 1,
                    Stock = 1
                },
                new ()
                {
                    Id = 6,
                    Brand = new Brand() { Name = "Z" },
                    Type = "abc",
                    Description = "abc",
                    Price = 1,
                    Stock = 1
                }
            };

            _mockPhoneRepo = new Mock<IRepository<Phone>>();
            _mockBrandService = new Mock<IBrandService>();
            _mockBasicLogger = new Mock<IBasicLogger>();

            _phoneService = new PhoneService(
                _mockPhoneRepo.Object,
                _mockBrandService.Object,
                _mockBasicLogger.Object);
        }

        public IQueryable<Phone> GetQueryable()
        {
            return _dummyData.AsQueryable();
        }
    }
}
