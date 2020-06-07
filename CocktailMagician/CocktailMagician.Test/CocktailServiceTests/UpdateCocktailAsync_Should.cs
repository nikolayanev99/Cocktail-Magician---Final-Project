using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UpdateCocktailAsync_Should
    {
        [TestMethod]
        public async Task Update_Cocktail_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Update_Cocktail_Correctly));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();


            var cocktail = new Cocktail
            {
                Id = 1,
                Name = "TestCocktail",
                ShortDescription = "TestShortDescription",
                LongDescription = "TestLongDescription",
            };
            var cocktailDto = new CocktailDto
            {
                Id = 1,
                Name = "NewTestCocktail",
                ShortDescription = "NewTestShortDescription",
                LongDescription = "NewTestLongDescription",
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
                var result = await sut.UpdateCocktailAsync(cocktailDto);

                var editedCocktail = await assertContext.Cocktails.FirstAsync();

                Assert.IsInstanceOfType(result, typeof(CocktailDto));
                Assert.AreEqual("NewTestCocktail", editedCocktail.Name);
                Assert.AreEqual("NewTestShortDescription", editedCocktail.ShortDescription);
                Assert.AreEqual("NewTestLongDescription", editedCocktail.LongDescription);
            }
        }
        [TestMethod]
        public async Task Throw_When_NoCocktailsFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Update_Cocktail_Correctly));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var cocktailDto = new CocktailDto
            {
                Id = 1,
                Name = "NewTestCocktail",
                ShortDescription = "NewTestShortDescription",
                LongDescription = "NewTestLongDescription",
            };
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.UpdateCocktailAsync(cocktailDto));

            }
        }
    }
}
