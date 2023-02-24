using System;
using Xunit;

namespace Phoneshop.Business.Tests.VodafoneScraperTests
{
    public class ExecuteFile : TestBase
    {
        [Fact]
        public void Should_RunFile_When_Called()
        {
            // get html file in solution folder
            string currentDir = Environment.CurrentDirectory;
            string vodaPath = "../../../../../vodafone.html";

            var list = _scraper.ExecuteFile(currentDir + vodaPath, 0);

            Assert.NotNull(list);
            Assert.NotEmpty(list);
            Assert.Equal("Apple", list[0].Brand.Name);
            Assert.True(list[0].Price > 0);
        }

        [Fact]
        public void Should_ThrowArgumentException_When_InvalidPath()
        {
            // Arrange, Act
            string falsePath = null;

            Action a = () => _scraper.ExecuteFile(falsePath, 0);

            // Assert
            Assert.Throws<ArgumentException>(a);
            System.Diagnostics.Debug.WriteLine($"Testing exception in unit test: Should_ThrowArgumentException_When_InvalidPath.");
        }
    }
}
