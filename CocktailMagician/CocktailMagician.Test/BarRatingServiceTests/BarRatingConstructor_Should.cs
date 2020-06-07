using System;
using System.Collections.Generic;
using System.Text;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.BarRatingTests
{
    [TestClass]
    public class BarRatingConstructor_Should
    {
        [TestMethod]
        public void Consctructor_CreateInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Consctructor_CreateInstance));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                Assert.IsNotNull(sut);
            }

        }
        [TestMethod]
        public void Constructor_Throw_WhenContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_Throw_WhenContextIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new BarRatingService(null, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object));
            }

        }
        [TestMethod]
        public void Constructor_Throw_WhenProviderIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_Throw_WhenProviderIsNull));
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, null));
            }
        }
        [TestMethod]
        public void Constructor_Throw_WhenDtoMappertIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_Throw_WhenDtoMappertIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new BarRatingService(assertContext, null, mockDateTimeProvider.Object));
            }

        }
    }
}
