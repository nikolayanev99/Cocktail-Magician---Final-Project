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

namespace CocktailMagician.Test.CocktailRatingServiceTests
{
    [TestClass]
    public class CocktailRatingConstructor_Should
    {
        [TestMethod]
        public void ConsctructorCocktailRating_CreateInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ConsctructorCocktailRating_CreateInstance));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);
                Assert.IsNotNull(sut);
            }

        }
        [TestMethod]
        public void Constructor_Throw_WhenContextIsNullInCocktailRating()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_Throw_WhenContextIsNullInCocktailRating));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailRatingService(null,  mockDateTimeProvider.Object, mapper.Object));
            }

        }
        [TestMethod]
        public void Constructor_Throw_WhenProviderIsNullInCocktailRating()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_Throw_WhenProviderIsNullInCocktailRating));
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailRatingService(assertContext, null, mapper.Object));
            }
        }
        [TestMethod]
        public void Constructor_Throw_WhenDtoMappertIsNullInCocktailRating()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_Throw_WhenDtoMappertIsNullInCocktailRating));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailRatingService(assertContext, mockDateTimeProvider.Object, null));
            }

        }

    }
}
