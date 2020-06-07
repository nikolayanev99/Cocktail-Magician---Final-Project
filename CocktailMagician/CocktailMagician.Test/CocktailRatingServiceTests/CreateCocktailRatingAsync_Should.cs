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
    public class CreateCocktailRatingAsync_Should
    {
        [TestMethod]
        public async Task Create_CocktailRating_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Create_CocktailRating_Correctly));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            var user = new User { Id = 1 };
            var cocktail = new Cocktail { Id = 1 };

            var ratingDto = new CocktailRatingDto
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                Value = 4,
            };

            mapper.Setup(x => x.MapDto(It.IsAny<CocktailRating>())).Returns(ratingDto);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);
                var result = await sut.CreateRatingAsync(ratingDto);

                Assert.IsInstanceOfType(result, typeof(CocktailRatingDto));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(1, result.CocktailId);
                Assert.AreEqual(1, result.UserId);
                Assert.AreEqual(4, result.Value);
            }
        }
        [TestMethod]
        public async Task EditCocktailRating_When_It_Exists()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(EditCocktailRating_When_It_Exists));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            var user = new User { Id = 1 };
            var cocktail = new Cocktail { Id = 1 };

            var rating = new CocktailRating
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                Value = 4,
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
                await arrangeContext.CocktailRatings.AddAsync(rating);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);
                var result = await sut.CreateRatingAsync(ratingDto);
                Assert.IsInstanceOfType(result, typeof(CocktailRatingDto));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(1, result.CocktailId);
                Assert.AreEqual(1, result.UserId);
                Assert.AreEqual(5, result.Value);

            }
        }
        [TestMethod]
        public async Task ReturnNull_When_CocktailDtoIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_CocktailDtoIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();



            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateRatingAsync(null));
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_CocktailRatingParamsAreInvalid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_CocktailRatingParamsAreInvalid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            var ratingDto = new CocktailRatingDto
            {
                Id = 1,
                UserId = 0,
                CocktailId = 0,
                Value = 0,
            };

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);
                var result = await sut.CreateRatingAsync(ratingDto);

                Assert.IsNull(result);
            }
        }
    }
}
