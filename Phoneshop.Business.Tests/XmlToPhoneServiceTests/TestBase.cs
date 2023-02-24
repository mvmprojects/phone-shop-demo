using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;

namespace XmlToPhoneServiceTests
{
    public class TestBase
    {
        public XmlToPhoneService _xmlToPhoneService;
        public Mock<IPhoneService> _mockService;

        public TestBase()
        {
            _mockService = new Mock<IPhoneService>();
            _xmlToPhoneService = new XmlToPhoneService(_mockService.Object);

            Phone result = new() { Id = 1 };

            _mockService.Setup(i => i.CreatePhone(It.IsAny<Phone>()))
                .Returns(result);
        }
    }
}
