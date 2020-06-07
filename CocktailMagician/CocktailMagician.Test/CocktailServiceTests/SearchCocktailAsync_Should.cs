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
    public class SearchCocktailAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectCocktails_When_Searching_By_Rating()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCocktails_When_Searching_By_Rating));
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
                new CocktailDto{ Id=1, Name="TestCocktail1",AverageRating=5}, new CocktailDto{Id=2, Name="TestCocktail2",AverageRating=5}
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
                var result = await sut.SearchCocktailsAsync("5");

                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailDto>));
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(5, result.First().AverageRating);
                Assert.AreEqual(5, result.Last().AverageRating);

            }
        }

        [TestMethod]
        public async Task ReturnCorrectCocktails_When_Searching_By_Name()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCocktails_When_Searching_By_Name));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            string[] ingredients = new string[] { "TestIngredient" };
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
                new CocktailDto{ Id=1, Name="TestCocktail1",Ingredients=ingredients} , new CocktailDto{Id=2, Name="TestCocktail2",Ingredients=ingredients}
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
                var result = await sut.SearchCocktailsAsync("test");

                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailDto>));
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual("TestCocktail1", result.First().Name);
                Assert.AreEqual("TestCocktail2", result.Last().Name);

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
                var result = await sut.SearchCocktailsAsync("test");

                Assert.IsNull(result);

            }
        }
    }
}
//public async Task<ICollection<CocktailDto>> SearchCocktailsAsync(string searchString)
//{
//    int number;

//    if (int.TryParse(searchString, out number))
//    {
//        var allCocktails = await this._context.Cocktails
//       .Where(i => i.IsDeleted == false)
//       .Include(i => i.CocktailIngredients)
//       .ThenInclude(i => i.Ingredient)
//       .Include(i => i.CocktailRatings)
//       .ToListAsync();

//        if (!allCocktails.Any())
//        {
//            return null;
//        }
//        var mappedCocktails = this._cocktailDtoMapper.MapDto(allCocktails);

//        var cocktailByRating = mappedCocktails.Where(r => Math.Floor(r.AverageRating) == number);

//        return cocktailByRating.ToList();
//    }

//    else
//    {


//        var cocktails = await this._context.Cocktails
//            .Where(i => i.IsDeleted == false)
//            .Include(i => i.CocktailIngredients)
//            .ThenInclude(i => i.Ingredient)
//            .Include(r => r.CocktailRatings)
//            .ToListAsync();
//        if (!cocktails.Any())
//        {
//            return null;
//        }
//        var mappedCocktails = this._cocktailDtoMapper.MapDto(cocktails);
//        var cocktailByIngredients = new List<CocktailDto>();

//        foreach (var item in mappedCocktails)
//        {
//            foreach (var ingredient in item.Ingredients)
//            {
//                if (ingredient.ToLower().Contains(searchString.ToLower()))
//                {
//                    cocktailByIngredients.Add(item);
//                }
//            }
//        }

//        var cocktailByName = mappedCocktails.Where(i => i.Name.ToLower().Contains(searchString.ToLower()));

//        var result = cocktailByIngredients.Union(cocktailByName);


//        return result.ToList();
//    }
//}