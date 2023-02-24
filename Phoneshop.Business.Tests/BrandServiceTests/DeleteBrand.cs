using Moq;
using System;
using Xunit;

namespace BrandServiceTests
{
    public class DeleteBrand : Phoneshop.Business.Tests.BrandServiceTests.TestBase
    {
        [Fact]
        public void When_CalledWithValidId_Should_CallRepo()
        {
            _brandService.DeleteBrand(1);

            _mockRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void When_CalledWithBadId_Should_NotCallRepo(int id)
        {
            Action a = () => _brandService.DeleteBrand(id);

            _mockRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Never());
            Assert.Throws<ArgumentOutOfRangeException>(a);
            System.Diagnostics.Debug.WriteLine($"Testing exception in unit test: When_CalledWithBadId_Should_NotCallRepo({id}).");
        }
    }
}
