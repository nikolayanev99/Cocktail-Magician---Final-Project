using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Test.IngredientsServiceTests
{
    [TestClass]
    public class EditIngredientAsync_Should
    {
        [TestMethod]
        public async Task ThrowWhen_NewNameIsEmpty()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhen_NewNameIsEmpty));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.EditIngredientAsync(0, String.Empty));
            }
        }
        [TestMethod]
        public async Task SetCorrectParam_WhenValueIsValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(SetCorrectParam_WhenValueIsValid));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
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
                Name = "Coca-cola",
            };

            mapperMock.Setup(x => x.MapDto(It.IsAny<Ingredient>())).Returns(ingredientDto);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapperMock.Object, mockDateTimeProvider.Object);
                var result = await sut.EditIngredientAsync(1, "Coca-cola");
                var selectIngredient = await assertContext.Ingredients.FirstAsync();

                Assert.AreEqual("Coca-cola", selectIngredient.Name);
            }
        }

        [TestMethod]
        public async Task SetCorrectParam_ToCorrectEntity()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(SetCorrectParam_ToCorrectEntity));
            var mapperMock = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
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
                Name = "Coca-cola",
            };

            mapperMock.Setup(x => x.MapDto(It.IsAny<Ingredient>())).Returns(ingredientDto);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapperMock.Object, mockDateTimeProvider.Object);
                var result = await sut.EditIngredientAsync(1, "Coca-cola");

                Assert.AreEqual(ingredient.Id, result.Id);
            }
        }
    }
}
