using Moq;
using Xunit;

namespace PhoneServiceTests
{
    public class Search : TestBase
    {
        public Search()
        {
            // note: this is too much effort and out of scope.
            // you're testing the repository now rather than the service.

            //_mockRepo.Setup(i => i.SearchPhones(It.IsAny<string>())).
            //    Returns((string q) => 
            //    {
            //        var lowerQuery = q.ToLower();
            //        return _dummyData.FindAll(
            //            x => x.Type.ToLower().Contains(lowerQuery)
            //                 || x.Brand.ToLower().Contains(lowerQuery)
            //                 || x.Description.ToLower().Contains(lowerQuery));
            //    });

            _mockPhoneRepo.Setup(x => x.GetAll()).
                Returns(GetQueryable());
        }

        // from now on, only test service logic itself

        [Fact]
        public void Should_CallRepo_When_Called()
        {
            string query = "";
            var phones = _phoneService.Search(query);

            // Assert
            _mockPhoneRepo.Verify(x => x.GetAll(), Times.Once());
        }

        // the tests below are useful, but out of scope

        //[Fact]
        //public void Should_ReturnResult_When_InputIsEmptyString()
        //{
        //    Arrange, Act
        //   var phones = _phoneService.Search("");
        //    Assert
        //    Assert.Equal(6, phones.Count);
        //}

        //[Fact]
        //public void Should_ReturnResult_When_InputIsNull()
        //{
        //    Arrange, Act
        //   var phones = _phoneService.Search(null);
        //    Assert
        //    Assert.Equal(6, phones.Count);
        //}

        //[Fact]
        //public void Should_ReturnSpecificResult_When_QueryHasMatch()
        //{
        //    Arrange, Act
        //   var phones = _phoneService.Search("google");
        //    Assert
        //    Assert.Equal(2, phones.Count);
        //}

        //[Fact]
        //public void Should_ReturnEmptyResult_When_QueryHasNoMatch()
        //{
        //    Arrange, Act
        //   var phones = _phoneService.Search("nonexisting-phone");
        //    Assert
        //    Assert.Empty(phones);
        //}
    }
}
