using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.BarServiceTests
{
    [TestClass]
    public class CreateBarAsync_Should
    {
        [TestMethod]
        public async Task CreateCorrectInstance_Of_Type_BarDto()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CreateCorrectInstance_Of_Type_BarDto));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();

            var newBarDto = new BarDTO
            {
                Id = 1,
                Name = "TestBar",
                Info = "TestInfo",
                Address = "TestAddress",
                PhotoPath = "TestPath",
            };
            mockBarDtoMapper.Setup(x => x.MapDto(It.IsAny<Bar>())).Returns(newBarDto);

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateBarAsync(newBarDto);

                Assert.IsInstanceOfType(result, typeof(BarDTO));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("TestBar", result.Name);
                Assert.AreEqual("TestInfo", result.Info);
                Assert.AreEqual("TestAddress", result.Address);
                Assert.AreEqual("TestPath", result.PhotoPath);

            }
        }
        [TestMethod]
        public async Task ReturnNull_When_DtoIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_DtoIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateBarAsync(null);

                Assert.IsNull(result);
            }
        }
    }
}
