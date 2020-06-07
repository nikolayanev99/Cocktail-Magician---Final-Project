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
    public class CreateCocktailAsync_Should
    {
        [TestMethod]
        public async Task Create_Cocktail_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Create_Cocktail_Correctly));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var ingredient = new Ingredient
            {
                Id = 1,
                Name = "TestIngredient"
            };

            var ingredientDto = new IngredientDto
            {
                Id = 1,
                Name = "TestIngredient"
            };

            string[] ingredients = new string[] { "TestIngredient" };

            var cocktailDto = new CocktailDto
            {
                Id = 1,
                Name = "TestCocktail",
                Ingredients = ingredients,
            };

            var cocktailIngredient = new CocktailIngredient
            {
                CocktailId = cocktailDto.Id,
                IngredientId = ingredient.Id
            };

            mockCocktailDtoMapper.Setup(x => x.MapDto(It.IsAny<Cocktail>())).Returns(cocktailDto);
            mockIngredientsService.Setup(x => x.GetIngredientByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(ingredient));
            mockCocktailIngretientService.Setup(x => x.CreateCocktailIngredientAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(cocktailIngredient));
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                var result = await sut.CreateCocktailAsync(cocktailDto);

                Assert.IsInstanceOfType(result, typeof(CocktailDto));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("TestCocktail", result.Name);
                Assert.AreEqual("TestIngredient", result.Ingredients.First());
            }
        }
        [TestMethod]
        public async Task Throw_When_DtoIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_DtoIsNull));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();


            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateCocktailAsync(null));
            }
        }
        [TestMethod]
        public async Task Throw_When_DtoIngredientsIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_DtoIngredientsIsNull));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            string[] ingredients = new string[] { };
            var cocktailDto = new CocktailDto
            {
                Id = 1,
                Name = "TestCocktail",
                Ingredients = ingredients,
            };
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateCocktailAsync(cocktailDto));
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_NoIngredientsFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_NoIngredientsFound));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var ingredient = new Ingredient
            {
                Id = 1,
                Name = "TestIngredient"
            };

            var ingredientDto = new IngredientDto
            {
                Id = 1,
                Name = "TestIngredient"
            };

            string[] ingredients = new string[] { "TestIngredient" };

            var cocktailDto = new CocktailDto
            {
                Id = 1,
                Name = "TestCocktail",
                Ingredients = ingredients,
            };

            var cocktailIngredient = new CocktailIngredient
            {
                CocktailId = cocktailDto.Id,
                IngredientId = ingredient.Id
            };

            mockCocktailDtoMapper.Setup(x => x.MapDto(It.IsAny<Cocktail>())).Returns(cocktailDto);
            mockCocktailIngretientService.Setup(x => x.CreateCocktailIngredientAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(cocktailIngredient));
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                var result = await sut.CreateCocktailAsync(cocktailDto);

                Assert.IsNull(result);
            }
        }
    }
}
//public async Task<CocktailDto> CreateCocktailAsync(CocktailDto tempCocktailDto)
//{
//    if (tempCocktailDto == null)
//    {
//        throw new ArgumentNullException("No entity found");
//    }

//    if (tempCocktailDto.Ingredients.Count == 0)
//    {
//        throw new ArgumentNullException("The ingredients are missing");
//    }

//    var cocktail = new Cocktail
//    {
//        Name = tempCocktailDto.Name,
//        ShortDescription = tempCocktailDto.ShortDescription,
//        LongDescription = tempCocktailDto.LongDescription,
//        ImageUrl = tempCocktailDto.ImageUrl,
//        ImageThumbnailUrl = tempCocktailDto.ImageThumbnailUrl,
//    };

//    await _context.Cocktails.AddAsync(cocktail);
//    await _context.SaveChangesAsync();

//    foreach (var item in tempCocktailDto.Ingredients)
//    {
//        var ingredient = await this._ingredientService.GetIngredientByNameAsync(item);

//        if (ingredient == null)
//        {
//            return null;
//        }

//        var cocktailIngredient = await this._cocktailIngredientService.CreateCocktailIngredientAsync(cocktail.Id, ingredient.Id);
//        cocktail.CocktailIngredients.Add(cocktailIngredient);
//        await this._context.SaveChangesAsync();
//    }

//    var cocktailDto = this._cocktailDtoMapper.MapDto(cocktail);

//    return cocktailDto;
//}