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
    public class GetBarAsync_Should
    {
        [TestMethod]
        public async Task Return_CorrectModel_WhenParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_CorrectModel_WhenParamsAreValid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();
            var newBar = new Bar
            {
                Id = 1,
                Name = "TestBar",
            };
            var barDto = new BarDTO
            {
                Id = 1,
                Name = "TestBar",
            };

            mockBarDtoMapper.Setup(x => x.MapDto(It.IsAny<Bar>())).Returns(barDto);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(newBar);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetBarAsync(1);

                Assert.IsInstanceOfType(result, typeof(BarDTO));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("TestBar", result.Name);
            }
        }

        [TestMethod]
        public async Task ReturnNull_When_BarNotFound()
        {   

            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_BarNotFound));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();


            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetBarAsync(1);

                Assert.IsNull(result);
            }
        }
    }
}
