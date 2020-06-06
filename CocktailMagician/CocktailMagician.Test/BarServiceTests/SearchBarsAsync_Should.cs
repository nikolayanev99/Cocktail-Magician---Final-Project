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
    public class SearchBarsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectBars_When_Searching_By_Rating()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectBars_When_Searching_By_Rating));
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
                new BarDTO{ Id=1, Name="TestBar1",AverageRating=5}, new BarDTO{Id=2, Name="TestBar2",AverageRating=5}
            };

            mockBarDtoMapper.Setup(x => x.MapDto(It.IsAny<ICollection<Bar>>())).Returns(list);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(newBar1);
                await arrangeContext.Bars.AddAsync(newBar2);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.SearchBarsAsync("5");

                Assert.IsInstanceOfType(result, typeof(ICollection<BarDTO>));
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(5, result.First().AverageRating);
                Assert.AreEqual(5, result.Last().AverageRating);

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
                var result = await sut.SearchBarsAsync("5");

                Assert.IsNull(result);

            }
        }
        [TestMethod]
        public async Task ReturnCorrectBars_When_Searching_By_NameOrAddress()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectBars_When_Searching_By_NameOrAddress));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarDtoMapper = new Mock<IDtoMapper<Bar, BarDTO>>();
            var newBar1 = new Bar
            {
                Id = 1,
                Name = "TestBar1",
                Address = "TestAddress1",

            };
            var newBar2 = new Bar
            {
                Id = 2,
                Name = "TestBar2",
                Address = "TestAddress2",

            };

            var list = new List<BarDTO>()
            {
                new BarDTO{ Id=1, Name="TestBar1",Address="TestAddress1",}, new BarDTO{Id=2, Name="TestBar2",Address="TestAddress2",}
            };

            mockBarDtoMapper.Setup(x => x.MapDto(It.IsAny<ICollection<Bar>>())).Returns(list);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(newBar1);
                await arrangeContext.Bars.AddAsync(newBar2);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarService(assertContext, mockBarDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.SearchBarsAsync("bar");

                Assert.IsInstanceOfType(result, typeof(ICollection<BarDTO>));
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual("TestBar1", result.First().Name);
                Assert.AreEqual(2, result.Last().Id);
                Assert.AreEqual("TestBar2", result.Last().Name);
            }
        }
    }
}
