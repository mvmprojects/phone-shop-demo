using Moq;
using Phoneshop.Domain.Models;
using Xunit;

namespace BrandServiceTests
{
    public class CreateBrand : Phoneshop.Business.Tests.BrandServiceTests.TestBase
    {
        [Fact]
        public void Should_CallCreateBrandWithParams_When_Called()
        {
            // Arrange, Act
            string name = "test";
            var phones = _brandService.CreateBrand(name);

            // Assert that repo is called
            _mockRepo.Verify(x => x.Create(It.IsAny<Brand>()), Times.Once());
        }
    }
}
