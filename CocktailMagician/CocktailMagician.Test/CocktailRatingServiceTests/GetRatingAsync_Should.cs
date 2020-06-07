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

namespace CocktailMagician.Test.CocktailRatingServiceTests
{
    [TestClass]
    public class GetRatingAsync_Should
    {
        [TestMethod]
        public async Task Return_Correct_Model_When_ParamsAreValidCocktailRating()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_Correct_Model_When_ParamsAreValidCocktailRating));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            var user = new User { Id = 1 };
            var cocktail = new Cocktail { Id = 1 };

            var rating = new CocktailRating
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                Value = 5,
            };
            var ratingDto = new CocktailRatingDto
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                Value = 5,
            };
            mapper.Setup(x => x.MapDto(It.IsAny<CocktailRating>())).Returns(ratingDto);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.CocktailRatings.AddAsync(rating);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);
                var result = await sut.GetRatingAsync(1, 1);

                Assert.IsInstanceOfType(result, typeof(CocktailRatingDto));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(1, result.UserId);
                Assert.AreEqual(1, result.CocktailId);
                Assert.AreEqual(5, result.Value);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_NoRatingsFound()
        {

            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_Correct_Model_When_ParamsAreValidCocktailRating));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);
                var result = await sut.GetRatingAsync(1, 1);

                Assert.IsNull(result);
            }
        }
    }
}
