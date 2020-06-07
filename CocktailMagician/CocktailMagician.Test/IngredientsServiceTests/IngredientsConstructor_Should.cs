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

namespace CocktailMagician.Test.IngredientsServiceTests
{
    [TestClass]
    public class IngredientsConstructor_Should
    {
        [TestMethod]
        public void Constructor_CreateCorrectInstance() 
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_CreateCorrectInstance));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            using (var assertContext = new CocktailMagicianContext(options)) 
            {
                //Act
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);

                //Assert
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void ThrowWhenContextIsNull() 
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowWhenContextIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            using (var assertContext = new CocktailMagicianContext(options)) 
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new IngredientService(null, mapper.Object, mockDateTimeProvider.Object));
            }
        }

        [TestMethod]
        public void ThrowExWhen_MapperIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowExWhen_MapperIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new IngredientService(assertContext, null, mockDateTimeProvider.Object));
            }
        }

        [TestMethod]
        public void ThrowExWhen_ProviderIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowExWhen_ProviderIsNull));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                Assert.ThrowsException<ArgumentNullException>(() => new IngredientService(assertContext, mapper.Object, null));
            }
        }
    }
}
