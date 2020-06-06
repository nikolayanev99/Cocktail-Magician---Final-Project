using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
    public class GetRatingAsync_Should
    {
        [TestMethod]
        public async Task Return_Correct_Model_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_Correct_Model_When_ParamsAreValid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            var user = new User { Id = 1 };
            var bar = new Bar { Id = 1 };

            var rating = new BarRating
            {
                Id = 1,
                UserId = 1,
                BarId = 1,
                Value = 5,
            };
            var ratingDto = new BarRatingDto
            {
                Id = 1,
                UserId = 1,
                BarId = 1,
                Value = 5,
            };
            mockBarRatingDtoMapper.Setup(x => x.MapDto(It.IsAny<BarRating>())).Returns(ratingDto);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.BarRatings.AddAsync(rating);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetRatingAsync(1, 1);

                Assert.IsInstanceOfType(result, typeof(BarRatingDto));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(1, result.UserId);
                Assert.AreEqual(1, result.BarId);
                Assert.AreEqual(5, result.Value);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_NoRatingsFound()
        {

            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_NoRatingsFound));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetRatingAsync(1, 1);

                Assert.IsNull(result);
            }
        }
    }
}
