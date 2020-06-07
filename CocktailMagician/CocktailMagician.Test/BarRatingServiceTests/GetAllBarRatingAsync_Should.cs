using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetAllBarRatingAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectModels_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectModels_When_ParamsAreValid));
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

            var list = new List<BarRatingDto>()
            {
                new BarRatingDto
                {
                    Id = 1,
                    UserId = 1,
                    BarId = 1,
                    Value = 5,
                },

               new BarRatingDto
               {
                    Id = 2,
                    UserId = 2,
                    BarId = 1,
                    Value = 3,
               }
            };
            mockBarRatingDtoMapper.Setup(x => x.MapDto(It.IsAny<ICollection<BarRating>>())).Returns(list);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Users.AddAsync(user1);
                await arrangeContext.Users.AddAsync(user2);
                await arrangeContext.BarRatings.AddAsync(rating1);
                await arrangeContext.BarRatings.AddAsync(rating2);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetAllBarRatingAsync(1);

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual(1, result.First().UserId);
                Assert.AreEqual(1, result.First().BarId);
                Assert.AreEqual(5, result.First().Value);
                Assert.AreEqual(2, result.Last().Id);
                Assert.AreEqual(2, result.Last().UserId);
                Assert.AreEqual(1, result.Last().BarId);
                Assert.AreEqual(3, result.Last().Value);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_NoRatingsFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectModels_When_ParamsAreValid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetAllBarRatingAsync(1);

                Assert.IsNull(result);
            }

        }
    }
}
