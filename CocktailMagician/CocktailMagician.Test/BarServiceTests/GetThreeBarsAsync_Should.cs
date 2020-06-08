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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.BarServiceTests
{
    [TestClass]
    public class GetThreeBarsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectCollection_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCollection_When_ParamsAreValid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();
            var newBar1 = new Bar
            {
                Id = 1,
                Name = "TestBar1",
            };
            var newBar2 = new Bar
            {
                Id = 2,
                Name = "TestBar2",
            };
            var list = new List<BarDTO>()
            {
                new BarDTO{ Id=1, Name="TestBar1",AverageRating=5}, new BarDTO{Id=2, Name="TestBar2",AverageRating=3}
            };
            mockBarDtoMapper.Setup(x => x.MapDto(It.IsAny<ICollection<Bar>>())).Returns(list);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(newBar1);
                await arrangeContext.Bars.AddAsync(newBar2);
                await arrangeContext.SaveChangesAsync();
            };

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetThreeBarsAsync(2);

                Assert.IsInstanceOfType(result, typeof(ICollection<BarDTO>));
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual("TestBar1", result.First().Name);
                Assert.AreEqual(5, result.First().AverageRating);
                Assert.AreEqual(2, result.Last().Id);
                Assert.AreEqual("TestBar2", result.Last().Name);
                Assert.AreEqual(3, result.Last().AverageRating);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_NoBarsFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_NoBarsFound));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetThreeBarsAsync(2);

                Assert.IsNull(result);
            }
        }
    }
}
//public async Task<ICollection<BarDTO>> GetThreeBarsAsync(int num)
//{
//    var bars = await this.context.Bars
//        .Include(r => r.Ratings)
//        .Where(r => r.IsDeleted == false)
//        .OrderByDescending(r => r.Ratings.Count())
//        .Take(num)
//        .ToListAsync();

//    if (bars == null)
//    {
//        return null;
//    }

//    var threeBarsDto = this.barDTOMapper.MapDto(bars);

//    return threeBarsDto;
//}