using Moq;
using Phoneshop.Domain.Models;
using System.Linq;
using Xunit;

namespace PhoneServiceTests
{
    public class CreatePhone : TestBase
    {
        public CreatePhone()
        {
            _mockPhoneRepo.Setup(i => i.Create(It.IsAny<Phone>())).
                Returns(new Phone() { Id = 999 });
            _mockBrandService.Setup(x => x.CreateBrand(It.IsAny<string>())).
                Returns(new Brand() { Id = 999 });
            _mockBrandService.Setup(x => x.GetBrand(It.IsAny<int>())).
                Returns(new Brand() { Id = 999 });
            _mockBrandService.Setup(x => x.GetBrandId(It.IsAny<string>())).
                Returns((string name) =>
                {
                    var phones = _dummyData;
                    bool hasBrand = phones.Any(p => p.Brand.Name == name);
                    return hasBrand ? 1 : 0;
                });
            _mockPhoneRepo.Setup(x => x.GetAll()).
                Returns(GetQueryable());
        }

        [Fact]
        public void Should_CallCreate_When_ValidAndNewInput()
        {
            Phone phone = new()
            {
                Brand = new Brand() { Name = "newType" },
                Type = "newType",
                Price = 1,
                Stock = 1
            };

            var result = _phoneService.CreatePhone(phone);

            Assert.NotNull(result);
            _mockPhoneRepo.Verify(x => x.Create(It.IsAny<Phone>()), Times.Once());
        }

        [Fact]
        public void Should_PassValidation_When_UniqueBrandAndType()
        {
            Phone phone = new()
            {
                Brand = new Brand() { Name = "A" },
                Type = "newType",
                Price = 1,
                Stock = 1
            };

            var result = _phoneService.CreatePhone(phone);

            Assert.NotEqual(0, result.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_FailValidation_When_BadPrice(int badPrice)
        {
            Phone phone = new()
            {
                Brand = new Brand() { Name = "A" },
                Type = "newType",
                Price = badPrice,
                Stock = 1
            };

            var result = _phoneService.CreatePhone(phone);

            Assert.Equal(0, result.Id);
        }

        [Fact]
        public void Should_FailValidation_When_ExistingComboBrandAndType()
        {
            Phone phone = new()
            {
                Brand = new Brand() { Name = "A" },
                Type = "existingType",
                Price = 1,
                Stock = 1
            };

            var result = _phoneService.CreatePhone(phone);

            Assert.Equal(0, result.Id);
        }

        [Fact]
        public void Should_CallLogger_When_CreatingPhone()
        {
            Phone phone = new()
            {
                Brand = new Brand() { Name = "A" },
                Type = "newType",
                Price = 1,
                Stock = 1
            };

            var result = _phoneService.CreatePhone(phone);

            _mockBasicLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Once);
        }
    }
}
