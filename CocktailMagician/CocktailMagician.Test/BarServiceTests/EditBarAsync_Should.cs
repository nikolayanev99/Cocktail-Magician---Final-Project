using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.BarServiceTests
{
    [TestClass]
    public class EditBarAsync_Should
    {
        [TestMethod]
        public async Task Update_Bar_Correctly()
        {

            //Arrange
            var options = TestUtilities.GetOptions(nameof(Update_Bar_Correctly));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();

            var newBar = new Bar
            {
                Id = 1,
                Name = "TestBar",
                Info = "TestInfo",
                Address = "TestAddress",
            };
            var newBarDto = new BarDTO
            {
                Id = 1,
                Name = "NewTestBar",
                Info = "NewTestInfo",
                Address = "NewTestAddress",
            };
            mockBarDtoMapper.Setup(x => x.MapDto(It.IsAny<Bar>())).Returns(newBarDto);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(newBar);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.EditBarAsync(newBarDto);
                var editedBar = await assertContext.Bars.FirstAsync();

                Assert.IsInstanceOfType(result, typeof(BarDTO));
                Assert.AreEqual("NewTestBar", editedBar.Name);
                Assert.AreEqual("NewTestInfo", editedBar.Info);
                Assert.AreEqual("NewTestAddress", editedBar.Address);
            }
        }

        [TestMethod]
        public async Task ReturnNull_When_BarNotFound()
        {

            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_BarNotFound));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();

            var newBarDto = new BarDTO
            {
                Id = 1,
                Name = "NewTestBar",
                Info = "NewTestInfo",
                Address = "NewTestAddress",
            };
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.EditBarAsync(newBarDto);

                Assert.IsNull(result);
            }
        }
    }
}
