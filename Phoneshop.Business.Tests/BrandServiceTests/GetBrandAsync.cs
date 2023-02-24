using Moq;
using Phoneshop.Domain.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BrandServiceTests
{
    public class GetBrandAsync : Phoneshop.Business.Tests.BrandServiceTests.TestBase
    {
        [Fact]
        public void Should_UseCache_When_Called()
        {
            // Arrange, Act
            var result = _brandService.GetBrandAsync(1).GetAwaiter();

            // Assert
            _mockCache.Verify(x => x.GetOrCreate(
                It.IsAny<string>(),
                (Func<Task<Brand>>)It.IsAny<object>()), Times.Once());
        }

        //[Fact]
        //public void Should_CallRepo_When_CalledAsync()
        //{
        //    // Arrange, Act
        //    var result = _brandService.GetBrandAsync(1).GetAwaiter();

        //    // Assert
        //    // System.NotSupportedException : Unsupported expression
        //    //_mockCache.Verify(x => x.GetOrCreate(
        //    //    It.IsAny<string>(),
        //    //    () => _mockRepo.Object.GetAll()
        //    //                          .Where(x => x.Id == id)
        //    //                          .SingleOrDefaultAsync(default), default, default), Times.Once());
        //    _mockRepo.Verify(r => r.GetAll());
        //}
    }
}
