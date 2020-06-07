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
    public class CreateIngredientAsync_Should
    {
        [TestMethod]
        public async Task CreateCorrect_Ingredient()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CreateCorrect_Ingredient));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var ingredientDto = new IngredientDto
            {
                Name = "Cola"
            };


            mapper.Setup(x => x.MapDto(It.IsAny<Ingredient>())).Returns(ingredientDto);

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateIngredientAsync(ingredientDto);


                Assert.IsInstanceOfType(result, typeof(IngredientDto));
                Assert.AreEqual(0, result.Id);
                Assert.AreEqual("Cola", result.Name);
            }
        }
    }
}
