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
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.BarServiceTests
{
    [TestClass]
    public class DeleteBarAsync_Shpuld
    {
        [TestMethod]
        public async Task Delete_Bar_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Delete_Bar_Correctly));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();
            var newBar = new Bar
            {
                Id = 1,
                Name = "TestBar",
            };

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(newBar);
                await arrangeContext.SaveChangesAsync();
                var barService = new BarService(arrangeContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await barService.DeleteBarAsync(1);
            }   
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var deletedBar = await assertContext.Bars.FirstAsync();
                Assert.IsTrue(deletedBar.IsDeleted);

            }
        }
        [TestMethod]
        public async Task ReturnFalse_When_BarNotFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnFalse_When_BarNotFound));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.DeleteBarAsync(1);

                Assert.IsFalse(result);
            }
        }
    }
}
