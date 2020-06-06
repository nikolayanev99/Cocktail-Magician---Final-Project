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
    public class GetAllBarsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectModel_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectModel_When_ParamsAreValid));
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
                new BarDTO{ Id=1, Name="TestBar1"}, new BarDTO{Id=2, Name="TestBar2"}
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
                var result = await sut.GetAllBarsAsync();

                Assert.IsInstanceOfType(result, typeof(ICollection<BarDTO>));
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual("TestBar1", result.First().Name);
                Assert.AreEqual(2, result.Last().Id);
                Assert.AreEqual("TestBar2", result.Last().Name);
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
                var result = await sut.GetAllBarsAsync();

                Assert.IsNull(result);
            }
        }

    }
}