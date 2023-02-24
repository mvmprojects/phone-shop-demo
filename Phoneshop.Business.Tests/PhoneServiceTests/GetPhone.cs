using System;
using Xunit;

namespace PhoneServiceTests
{
    public class GetPhone : TestBase
    {
        public GetPhone()
        {
            //_mockRepo.Setup(i => i.GetPhone(It.IsAny<int>())).
            //    Returns((int id) => { return _dummyData.FirstOrDefault(x => x.Id == id); });
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_ThrowException_When_ArgumentOutRange(int id)
        {
            // Arrange, Act
            Action a = () => _phoneService.GetPhone(id);
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(a);
            System.Diagnostics.Debug.WriteLine($"Testing exception in unit test: Should_ThrowException_When_ArgumentOutRange({id}).");
        }

        [Fact]
        public void Should_HaveSpecificErrorMsg_When_ArgumentOutRange()
        {
            // Arrange, Act
            ArgumentOutOfRangeException ex = Assert
                .Throws<ArgumentOutOfRangeException>(
                () => _phoneService.GetPhone(0));
            // Assert
            Assert.Equal(
                "Input cannot be negative or zero. (Parameter '0')", ex.Message);
            System.Diagnostics.Debug.WriteLine("Testing exception in unit test: Should_HaveSpecificErrorMsg_When_ArgumentOutRange()");
        }

        // out of scope

        //[Fact]
        //public void Should_ReturnResult_When_ValidValueIsUsed()
        //{
        //    // Arrange, Act
        //    var phone = _phoneService.GetPhone(1);
        //    // Assert
        //    Assert.NotNull(phone);
        //    Assert.IsType<Phone>(phone);
        //}

        //[Fact]
        //public void Should_ReturnNull_When_NoMatch()
        //{
        //    // Arrange, Act
        //    var phone = _phoneService.GetPhone(999999);
        //    // Assert
        //    Assert.Null(phone);
        //}
    }
}
