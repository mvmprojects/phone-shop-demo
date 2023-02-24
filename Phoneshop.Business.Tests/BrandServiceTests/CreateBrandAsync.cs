using Moq;
using Phoneshop.Domain.Models;
using System.Threading.Tasks;
using Xunit;

namespace BrandServiceTests
{
    public class CreateBrandAsync : Phoneshop.Business.Tests.BrandServiceTests.TestBase
    {
        [Fact]
        public async Task Should_CallCreateBrandWithParams_When_CalledAsync()
        {
            // Arrange, Act
            string name = "test";
            var phones = await _brandService.CreateBrandAsync(name);

            // Assert that repo is called
            _mockRepo.Verify(x => x.CreateAsync(It.IsAny<Brand>()), Times.Once());
        }
    }
}
