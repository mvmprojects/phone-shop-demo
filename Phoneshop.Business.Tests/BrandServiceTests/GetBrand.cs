using Moq;
using Xunit;

namespace BrandServiceTests
{
    public class GetBrand : Phoneshop.Business.Tests.BrandServiceTests.TestBase
    {
        [Fact]
        public void Should_CallRepoWithParams_When_Called()
        {
            // Arrange, Act
            var result = _brandService.GetBrand(1);

            // Assert that repo is called
            _mockRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
        }
    }
}
