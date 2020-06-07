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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Test.IngredientsServiceTests
{
    [TestClass]
    public class GetAllIngredientsAsync_Should
    {
        [TestMethod]
        public async Task GetCorrectIngredients_When_ParamsAreValid ()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(GetCorrectIngredients_When_ParamsAreValid));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var ingredient = new Ingredient
            {
                Id = 1,
                Name = "Cola"
            };

            var ingredient1 = new Ingredient
            {
                Id = 2,
                Name = "Coca-cola"
            };

            var ingredientsDto = new List<IngredientDto>
            {
                new IngredientDto{ Id=1, Name="Cola"}, new IngredientDto{Id=2, Name="Coca-cola"}
            };

            mapper.Setup(x => x.MapDto(It.IsAny<ICollection<Ingredient>>())).Returns(ingredientsDto);

            using (var arrangeContext = new CocktailMagicianContext(options)) 
            {
                //Act
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.Ingredients.AddAsync(ingredient1);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Assert
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetAllIngredientsAsync();

                Assert.IsInstanceOfType(result, typeof(ICollection<IngredientDto>));
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(ingredient.Id, result.First().Id);
                Assert.AreEqual(ingredient.Name, result.First().Name);
                Assert.AreEqual(ingredient1.Id, result.Last().Id);
                Assert.AreEqual(ingredient1.Name, result.Last().Name);
            }
        }
        [TestMethod]
        public async Task ThrowsWhen_NoIngredientsAreFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowsWhen_NoIngredientsAreFound));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetAllIngredientsAsync());
            }
        }
    }
}