using Moq;
using Phoneshop.Domain.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PhoneServiceTests
{
    public class CreatePhoneAsync : TestBase
    {
        public CreatePhoneAsync()
        {
            _mockPhoneRepo.Setup(i => i.CreateAsync(It.IsAny<Phone>())).
                Returns(Task.FromResult(new Phone() { Id = 999 }));
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
        public async Task Should_CallCreateAsync_When_ValidAndNewInput()
        {
            Phone phone = new()
            {
                Brand = new Brand() { Name = "newBrand" },
                Type = "newType",
                Price = 1,
                Stock = 1
            };

            Phone result = await _phoneService.CreatePhoneAsync(phone);

            Assert.NotNull(result);
            _mockPhoneRepo.Verify(x => x.CreateAsync(It.IsAny<Phone>()), Times.Once());
        }

        [Fact]
        public async Task Should_PassValidation_When_UniqueBrandAndTypeAsync()
        {
            Phone phone = new()
            {
                Brand = new Brand() { Name = "A" },
                Type = "newType",
                Price = 1,
                Stock = 1
            };

            Phone result = await _phoneService.CreatePhoneAsync(phone);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Should_FailValidation_When_BadPriceAsync(int badPrice)
        {
            Phone phone = new()
            {
                Brand = new Brand() { Name = "A" },
                Type = "newType",
                Price = badPrice,
                Stock = 1
            };

            Phone result = await _phoneService.CreatePhoneAsync(phone);

            Assert.Equal(0, result.Id);
        }

        [Fact]
        public async Task Should_FailValidation_When_ExistingComboBrandAndTypeAsync()
        {
            Phone phone = new()
            {
                Brand = new Brand() { Name = "A" },
                Type = "existingType",
                Price = 1,
                Stock = 1
            };

            Phone result = await _phoneService.CreatePhoneAsync(phone);

            Assert.Equal(0, result.Id);
        }

        [Fact]
        public async Task Should_CallLogger_When_CreatingPhoneAsync()
        {
            Phone phone = new()
            {
                Brand = new Brand() { Name = "A" },
                Type = "newType",
                Price = 1,
                Stock = 1
            };

            Phone result = await _phoneService.CreatePhoneAsync(phone);

            _mockBasicLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Once);
        }
    }
}
