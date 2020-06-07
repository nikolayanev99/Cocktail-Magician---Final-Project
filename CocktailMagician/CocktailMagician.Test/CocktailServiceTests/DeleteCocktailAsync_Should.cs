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
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.CocktailServiceTests
{
    [TestClass]
    public class DeleteCocktailAsync_Should
    {
        [TestMethod]
        public async Task Delete_Cocktail_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Delete_Cocktail_Correctly));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var cocktail = new Cocktail
            {
                Id = 1,
                Name = "TestCocktail",
            };
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
                var cocktailService = new CocktailService(arrangeContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                var result = await cocktailService.DeleteCocktailAsync(1);
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var deletedCocktail = await assertContext.Cocktails.FirstAsync();
                Assert.IsTrue(deletedCocktail.IsDeleted);
            }
        }

        [TestMethod]
        public async Task Throw_When_NoCocktailFoun()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_NoCocktailFoun));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(()=> sut.DeleteCocktailAsync(1));
              
            }
        }
    }
}