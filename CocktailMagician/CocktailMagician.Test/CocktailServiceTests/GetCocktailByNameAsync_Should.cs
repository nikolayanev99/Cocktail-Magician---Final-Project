using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.CocktailServiceTests
{
    [TestClass]
    public class GetCocktailByNameAsync_Should
    {
        [TestMethod]
        public async Task Get_CorrectModel_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Get_CorrectModel_When_ParamsAreValid));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var cocktail = new Cocktail
            {
                Id = 1,
                Name = "TestCocktail",
            };
            var cocktailDto = new CocktailDto
            {
                Id = 1,
                Name = "TestCocktail",
            };

            mockCocktailDtoMapper.Setup(x => x.MapDto(It.IsAny<Cocktail>())).Returns(cocktailDto);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                var result = await sut.GetCocktailByNameAsync("TestCocktail");

                Assert.IsInstanceOfType(result, typeof(CocktailDto));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("TestCocktail", result.Name);
            }
        }
        [TestMethod]
        public async Task Throw_When_NoCocktailFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_NoCocktailFound));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetCocktailByNameAsync("TestCocktail"));
            }

        }
    }
}
