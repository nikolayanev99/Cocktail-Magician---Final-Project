using System;
using System.Collections.Generic;
using System.Text;
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
    public class CocktailServiceConstructor_Should
    {
        [TestMethod]
        public void Constructor_Create_Instance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_Create_Instance));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                Assert.IsNotNull(sut);
            }
        }
        [TestMethod]
        public void Throw_When_Context_IsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_Context_IsNull));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(()=> new CocktailService(null, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object));               
            }
        }

        [TestMethod]
        public void Throw_When_DtoMapper_IsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_DtoMapper_IsNull));
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailService(assertContext, null, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object));
            }
        }
        [TestMethod]
        public void Throw_When_CocktailIngredientsService_IsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_CocktailIngredientsService_IsNull));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object,null, mockIngredientsService.Object));
            }
        }
        [TestMethod]
        public void Throw_When_IngredientsService_IsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_IngredientsService_IsNull));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object,null));
            }
        }
        [TestMethod]
        public void Throw_When_Provider_IsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_Provider_IsNull));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailService(assertContext, mockCocktailDtoMapper.Object,null, mockCocktailIngretientService.Object, mockIngredientsService.Object));
            }
        }
    }
}

