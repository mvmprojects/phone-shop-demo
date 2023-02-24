using Xunit;

namespace PhoneServiceTests
{
    public class GetPhones : TestBase
    {
        public GetPhones()
        {
            //_mockRepo.Setup(i => i.GetPhones()).
            //    Returns(_dummyData);
            _mockPhoneRepo.Setup(x => x.GetAll()).
                Returns(GetQueryable());
        }

        [Fact]
        public void Should_ReturnAllResults_WhenCalled()
        {
            // Arrange, Act
            var phones = _phoneService.GetPhones();
            // Assert
            // using InRange so a longer list of dummy data still works
            Assert.InRange<int>(phones.Count, 6, 100);
        }

        [Fact]
        public void Should_ContainOrderedResults_WhenCalled()
        {
            // Arrange, Act
            var phones = _phoneService.GetPhones();
            var first = phones[0];
            var last = phones[5];
            // Assert
            Assert.Equal("A", first.Brand.Name);
            Assert.Equal("Z", last.Brand.Name);
        }
    }
}
