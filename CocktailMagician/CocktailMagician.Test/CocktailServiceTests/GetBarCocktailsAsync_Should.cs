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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.CocktailServiceTests
{
    [TestClass]
    public class GetBarCocktailsAsync_Should
    {
        [TestMethod]
        public async Task Return_Correct_Cocktail_WhenParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_Correct_Cocktail_WhenParamsAreValid));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var bar = new Bar { Id = 1 };
            var cocktail = new Cocktail { Id = 1, Name = "TestCocktail" };
            var barCocktail = new BarCocktail { BarId = 1, CocktailId = 1 };
            var cocktailDto = new CocktailDto
            {
                Id = 1,
                Name = "TestCocktail",
            };

            mockCocktailDtoMapper.Setup(x => x.MapDto(It.IsAny<Cocktail>())).Returns(cocktailDto);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.BarCocktails.AddAsync(barCocktail);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                var result = await sut.GetBarCocktailsAsync(1);

                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailDto>));
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual("TestCocktail", result.First().Name);
            }
        }
    }
}
//public async Task<ICollection<CocktailDto>> GetBarCocktailsAsync(int barId)
//{
//    var cocktailsFromBar = new List<CocktailDto>();

//    var barCocktails = await this._context.BarCocktails
//        .Where(bc => bc.BarId == barId)
//        .Select(bc => bc.CocktailId)
//        .ToListAsync();

//    foreach (var item in barCocktails)
//    {
//        cocktailsFromBar.Add(await this.GetCokctailAsync(item));
//    }


//    return cocktailsFromBar;
//}