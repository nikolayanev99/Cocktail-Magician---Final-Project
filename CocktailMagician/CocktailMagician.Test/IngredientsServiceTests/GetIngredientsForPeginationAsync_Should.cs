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
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Test.IngredientsServiceTests
{
    [TestClass]
    public class GetIngredientsForPeginationAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectCollectionOfIngredients_WhenParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCollectionOfIngredients_WhenParamsAreValid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            var ingredient1 = new Ingredient
            {
                Id = 1,
                Name = "Cola",
                IsDeleted = false
            };
            var ingredient2 = new Ingredient
            {
                Id = 2,
                Name = "Water",
                IsDeleted = false
            };

            var list = new List<IngredientDto>
            {
                new IngredientDto{ Id=1, Name="Cola"}, new IngredientDto{Id=2, Name="Water"}
            };

            mapper.Setup(x => x.MapDto(It.IsAny<ICollection<Ingredient>>())).Returns(list);
            
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient1);
                await arrangeContext.Ingredients.AddAsync(ingredient2);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetIngredientsForPeginationAsync(2, 1);

                Assert.IsInstanceOfType(result, typeof(ICollection<IngredientDto>));
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual("Cola", result.First().Name);
                Assert.AreEqual(2, result.Last().Id);
                Assert.AreEqual("Water", result.Last().Name);
            }
        }
    }
}
