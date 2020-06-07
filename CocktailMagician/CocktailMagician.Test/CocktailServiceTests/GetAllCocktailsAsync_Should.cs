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
    public class GetAllCocktailsAsync_Should
    {
        [TestMethod]
        public async Task Get_Correct_Models_WhenParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Get_Correct_Models_WhenParamsAreValid));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var cocktail1 = new Cocktail
            {
                Id = 1,
                Name = "TestCocktail1",
            };
            var cocktail2 = new Cocktail
            {
                Id = 2,
                Name = "TestCocktail2",
            };
            var list = new List<CocktailDto>
            {
                new CocktailDto
                {
                    Id = 1,
                    Name = "TestCocktail1",
                },
                new CocktailDto
                {
                    Id = 2,
                    Name = "TestCocktail2",
                }
            };
            mockCocktailDtoMapper.Setup(x => x.MapDto(It.IsAny<ICollection<Cocktail>>())).Returns(list);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail1);
                await arrangeContext.Cocktails.AddAsync(cocktail2);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                var result = await sut.GetAllCocktailsAsync();

                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailDto>));
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual("TestCocktail1", result.First().Name);
                Assert.AreEqual(2, result.Last().Id);
                Assert.AreEqual("TestCocktail2", result.Last().Name);
            }
        }
        [TestMethod]
        public async Task Throw_When_NoCocktailsFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_NoCocktailsFound));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetAllCocktailsAsync());
            }
        }
    }
}
//public async Task<ICollection<CocktailDto>> GetAllCocktailsAsync()
//{
//    var cocktails = await this._context.Cocktails
//        .Where(v => v.IsDeleted == false)
//        .Include(c => c.BarCocktails)
//        .OrderBy(n => n.Name)
//        .ToListAsync();

//    if (!cocktails.Any())
//    {
//        throw new ArgumentNullException("No entities found");
//    }
//    var cocktailDto = this._cocktailDtoMapper.MapDto(cocktails);

//    return cocktailDto;
//}