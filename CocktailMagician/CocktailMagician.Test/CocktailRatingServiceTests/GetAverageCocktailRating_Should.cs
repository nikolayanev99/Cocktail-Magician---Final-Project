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
    public class GetAverageCocktailRating_Should
    {
        [TestMethod]
        public void ReturnCorrectValue_When_ParamsAreValidInMethod()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectValue_When_ParamsAreValidInMethod));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            var user1 = new User { Id = 1 };
            var user2 = new User { Id = 2 };
            var cocktail = new Cocktail { Id = 1 };

            var rating1 = new CocktailRating
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                Value = 5,
            };
            var rating2 = new CocktailRating
            {
                Id = 2,
                UserId = 2,
                CocktailId = 1,
                Value = 3,
            };

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                arrangeContext.Cocktails.AddAsync(cocktail);
                arrangeContext.Users.AddAsync(user1);
                arrangeContext.Users.AddAsync(user2);
                arrangeContext.CocktailRatings.AddAsync(rating1);
                arrangeContext.CocktailRatings.AddAsync(rating2);
                arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);
                var result = sut.GetAverageCocktailRating(1);

                Assert.AreEqual(4, result);
            }
        }
        [TestMethod]
        public void Return_0_When_NoRatingsFoundCocktailRating()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_0_When_NoRatingsFoundCocktailRating));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);
                var result = sut.GetAverageCocktailRating(1);

                Assert.AreEqual(0, result);
            }
        }
    }
}
