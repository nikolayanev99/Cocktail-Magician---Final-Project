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
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Test.CocktailRatingServiceTests
{
    [TestClass]
    public class GetAllRatingsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectEntity_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectEntity_When_ParamsAreValid));
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

            var list = new List<CocktailRatingDto>()
            {
                new CocktailRatingDto
                {
                  Id = 1,
                  UserId = 1,
                  CocktailId = 1,
                  Value = 5,
                },

               new CocktailRatingDto
               {
                     Id = 2,
                     UserId = 2,
                     CocktailId = 1,
                     Value = 3,
               }
            };
            mapper.Setup(x => x.MapDto(It.IsAny<ICollection<CocktailRating>>())).Returns(list);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.Users.AddAsync(user1);
                await arrangeContext.Users.AddAsync(user2);
                await arrangeContext.CocktailRatings.AddAsync(rating1);
                await arrangeContext.CocktailRatings.AddAsync(rating2);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);
                var result = await sut.GetAllRatingsAsync(1);

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual(1, result.First().UserId);
                Assert.AreEqual(1, result.First().CocktailId);
                Assert.AreEqual(5, result.First().Value);
                Assert.AreEqual(2, result.Last().Id);
                Assert.AreEqual(2, result.Last().UserId);
                Assert.AreEqual(1, result.Last().CocktailId);
                Assert.AreEqual(3, result.Last().Value);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_NoRatingsAreFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_NoRatingsAreFound));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailRating, CocktailRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailRatingService(assertContext, mockDateTimeProvider.Object, mapper.Object);
                var result = await sut.GetAllRatingsAsync(1);

                Assert.IsNull(result);
            }

        }
    }
}
