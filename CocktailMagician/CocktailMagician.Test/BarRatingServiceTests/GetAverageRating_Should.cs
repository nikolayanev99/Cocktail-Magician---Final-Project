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

    public class GetAverageRating_Should
    {
        [TestMethod]
        public void ReturnCorrectValue_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectValue_When_ParamsAreValid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            var user1 = new User { Id = 1 };
            var user2 = new User { Id = 2 };
            var bar = new Bar { Id = 1 };

            var rating1 = new BarRating
            {
                Id = 1,
                UserId = 1,
                BarId = 1,
                Value = 5,
            };
            var rating2 = new BarRating
            {
                Id = 2,
                UserId = 2,
                BarId = 1,
                Value = 3,
            };

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                arrangeContext.Bars.AddAsync(bar);
                arrangeContext.Users.AddAsync(user1);
                arrangeContext.Users.AddAsync(user2);
                arrangeContext.BarRatings.AddAsync(rating1);
                arrangeContext.BarRatings.AddAsync(rating2);
                arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                var result = sut.GetAverageBarRating(1);

                Assert.AreEqual(4, result);
            }
        }
        [TestMethod]
        public void Return_0_When_NoRatingsFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_0_When_NoRatingsFound));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                var result = sut.GetAverageBarRating(1);

                Assert.AreEqual(0, result);
            }
        }
    }
}
