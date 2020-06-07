using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Test.IngredientsServiceTests
{
    [TestClass]
    public class DeleteIngredientAsync_Should
    {
        [TestMethod]

        public async Task DeleteIngredient_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(DeleteIngredient_Correctly));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var ingredient = new Ingredient
            {
                Id = 1,
                Name = "Cola",
                IsDeleted = false
            };
            var ingredientDto = new IngredientDto
            {
                Id = 1,
                Name = "Cola",
            };

            mapper.Setup(x => x.MapDto(It.IsAny<Ingredient>())).Returns(ingredientDto);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                //Act
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.SaveChangesAsync();
                var barService = new IngredientService(arrangeContext, mapper.Object, mockDateTimeProvider.Object);
                var result = await barService.DeleteIngredientAsync(1);
            }

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Assert

                Assert.IsTrue(ingredient.IsDeleted);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectType_IngredientDto()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectType_IngredientDto));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var ingredient = new Ingredient
            {
                Id = 1,
                Name = "Cola",
                IsDeleted = false
            };
            var ingredientDto = new IngredientDto
            {
                Id = 1,
                Name = "Cola",
            };

            mapper.Setup(x => x.MapDto(It.IsAny<Ingredient>())).Returns(ingredientDto);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                //Act
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.SaveChangesAsync();

            }

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Assert
                var barService = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);
                var result = await barService.DeleteIngredientAsync(1);
                Assert.IsInstanceOfType(result, typeof(IngredientDto));
            }
        }
        [TestMethod]
        public async Task ThrowWhen_IngredientIsContainsInCocktail()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_IngredientIsContainsInCocktail));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();


            var ingredient = new Ingredient
            {
                Id = 1,
                Name = "Cola",
                IsDeleted = false
            };

            var cocktail = new Cocktail
            {
                Id = 1,
                Name = "ColaCokctail"
            };

            var cocktailIngr = new CocktailIngredient
            {
                CocktailId = cocktail.Id,
                IngredientId = ingredient.Id,
            };

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.CocktailIngredients.AddAsync(cocktailIngr);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.DeleteIngredientAsync(ingredient.Id));
            }
        }
    }
}
