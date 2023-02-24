using Moq;
using Xunit;

namespace PhoneServiceTests
{
    public class DeletePhone : TestBase
    {
        public DeletePhone()
        {
        }

        [Fact]
        public void When_CalledWithValidId_Should_CallRepo()
        {
            _phoneService.DeletePhone(1);

            _mockPhoneRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void When_CalledWithBadId_Should_NotCallRepo(int id)
        {
            _phoneService.DeletePhone(id);

            _mockPhoneRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Never());
        }
    }
}
