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
    public class GetThreeCocktailsAsync_Should
    {
        [TestMethod]
        public async Task Return_Correct_Collectio_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_Correct_Collectio_When_ParamsAreValid));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var newCocktail1 = new Cocktail
            {
                Id = 1,
                Name = "TestCocktail1",

            };
            var newCocktail2 = new Cocktail
            {
                Id = 2,
                Name = "TestCocktail2",

            };

            var list = new List<CocktailDto>()
            {
                new CocktailDto{ Id=1, Name="TestCocktail1",AverageRating=3}, new CocktailDto{Id=2, Name="TestCocktail2",AverageRating=5}
            };

            mockCocktailDtoMapper.Setup(x => x.MapDto(It.IsAny<ICollection<Cocktail>>())).Returns(list);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(newCocktail1);
                await arrangeContext.Cocktails.AddAsync(newCocktail2);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                var result = await sut.GetThreeCocktailsAsync(2);

                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailDto>));
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(3, result.First().AverageRating);
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual(5, result.Last().AverageRating);
                Assert.AreEqual(2, result.Last().Id);

            }
        }
        [TestMethod]
        public async Task ReturnNull_When_NoCocktailsFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_NoCocktailsFound));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                var result = await sut.GetThreeCocktailsAsync(3);
                Assert.IsNull(result);
            }
        }
    }
}
//public async Task<ICollection<CocktailDto>> GetThreeCocktailsAsync(int num)
//{
//    var cocktails = await this._context.Cocktails
//        .Include(r => r.CocktailRatings)
//        .Where(r => r.IsDeleted == false)
//        .OrderByDescending(r => r.CocktailRatings.Count())
//        .Take(num)
//        .ToListAsync();

//    if (cocktails == null)
//    {
//        return null;
//    }

//    var threeCocktailDto = this._cocktailDtoMapper.MapDto(cocktails);

//    return threeCocktailDto;
//}