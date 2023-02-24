using Moq;
using Phoneshop.Domain.Models;
using System;
using System.Xml;
using Xunit;

namespace XmlToPhoneServiceTests
{
    public class MapImportedPhones : TestBase
    {
        [Fact]
        public void Should_CallPhoneService_When_GivenValidXml()
        {
            // Arrange, Act
            string xml = "<?xml version=\"1.0\" encoding=\"utf - 8\"?>" +
                "<Phones><Phone><Brand>a</Brand><Type>a</Type></Phone></Phones>";

            _xmlToPhoneService.MapImportedPhones(xml);

            // Assert
            _mockService.Verify(x => x.CreatePhone(It.IsAny<Phone>()), Times.Once());
        }

        [Fact]
        public void Should_ThrowXmlExc_When_GivenBadXml()
        {
            // Arrange, Act
            string xml = "<?xml version=\"1.0\" encoding=\"utf - 8\"?>" +
                "</nothing>";

            Action a = () => _xmlToPhoneService.MapImportedPhones(xml);

            // Assert
            Assert.Throws<XmlException>(a);
            System.Diagnostics.Debug.WriteLine($"Testing exception in unit test: Should_ThrowXmlExc_When_GivenBadXml().");
        }
    }
}
