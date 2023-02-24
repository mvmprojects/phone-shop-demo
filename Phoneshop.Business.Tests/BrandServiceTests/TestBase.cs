using Moq;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoneshop.Business.Tests.BrandServiceTests
{
    public class TestBase
    {
        public BrandService _brandService;
        public Mock<IRepository<Brand>> _mockRepo;
        public Mock<ICaching> _mockCache;

        public List<Brand> _dummyList;

        public TestBase()
        {
            _mockRepo = new Mock<IRepository<Brand>>();
            _mockCache = new Mock<ICaching>();

            Task<Brand> mockReturn = new(() => new Brand() { Id = 1 });

            _mockCache.Setup(i => i.GetOrCreate(
                It.IsAny<string>(),
                //(Func<Task<It.IsAnyType>>)It.IsAny<object>(), 30, 60))
                (Func<Task<Brand>>)It.IsAny<object>()))
                .Returns(mockReturn);

            _brandService = new BrandService(
                _mockRepo.Object,
                _mockCache.Object);
        }
    }
}
