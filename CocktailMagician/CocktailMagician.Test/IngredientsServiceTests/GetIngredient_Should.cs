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
    public class GetIngredient_Should
    {
        [TestMethod]

        public async Task ReturnCorrectInstance_IngredientDto() 
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectInstance_IngredientDto));
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
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);
                var result =  await sut.GetIngredientAsyng(1);

                Assert.AreEqual(ingredient.Id, result.Id);
                Assert.AreEqual(ingredient.Name, result.Name);
                Assert.IsInstanceOfType(result, typeof(IngredientDto));
            }
        }

        [TestMethod]
        public async Task ReturnNull_When_IngredientNotFound()
        {

            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_IngredientNotFound));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();


            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetIngredientAsyng(1);

                Assert.IsNull(result);
            }
        }
    }
}
