using Moq;
using Phoneshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BrandServiceTests
{
    public class GetAllAsync : Phoneshop.Business.Tests.BrandServiceTests.TestBase
    {
        [Fact]
        public void Should_UseCacheForList_When_Called()
        {
            // Arrange, Act
            var result = _brandService.GetAllAsync().GetAwaiter();

            // Assert
            _mockCache.Verify(x => x.GetOrCreate(
                It.IsAny<string>(),
                (Func<Task<List<Brand>>>)It.IsAny<object>()), Times.Once());
        }
    }
}
