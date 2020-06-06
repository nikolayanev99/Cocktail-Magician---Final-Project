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
    public class CreateRatingAsync_Should
    {
        [TestMethod]
        public async Task Create_Rating_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Create_Rating_Correctly));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            var user = new User { Id = 1 };
            var bar = new Bar { Id = 1 };

            var ratingDto = new BarRatingDto
            {
                Id = 1,
                UserId = 1,
                BarId = 1,
                Value = 4,
            };

            mockBarRatingDtoMapper.Setup(x => x.MapDto(It.IsAny<BarRating>())).Returns(ratingDto);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateRatingAsync(ratingDto);

                Assert.IsInstanceOfType(result, typeof(BarRatingDto));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(1, result.BarId);
                Assert.AreEqual(1, result.UserId);
                Assert.AreEqual(4, result.Value);
            }
        }
        [TestMethod]
        public async Task EditRating_When_It_Exists()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(EditRating_When_It_Exists));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            var user = new User { Id = 1 };
            var bar = new Bar { Id = 1 };

            var rating = new BarRating
            {
                Id = 1,
                UserId = 1,
                BarId = 1,
                Value = 4,
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
                await arrangeContext.BarRatings.AddAsync(rating);
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateRatingAsync(ratingDto);
                Assert.IsInstanceOfType(result, typeof(BarRatingDto));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(1, result.BarId);
                Assert.AreEqual(1, result.UserId);
                Assert.AreEqual(5, result.Value);

            }
        }
        [TestMethod]
        public async Task ReturnNull_When_DtoIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_DtoIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();



            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateRatingAsync(null);

                Assert.IsNull(result);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_ParamsAreInvalid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_ParamsAreInvalid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarRatingDtoMapper = new Mock<IDtoMapper<BarRating, BarRatingDto>>();

            var ratingDto = new BarRatingDto
            {
                Id = 1,
                UserId = 0,
                BarId = 0,
                Value = 0,
            };

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarRatingService(assertContext, mockBarRatingDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateRatingAsync(ratingDto);

                Assert.IsNull(result);
            }
        }
    }
}
//public async Task<BarRatingDto> CreateRatingAsync(BarRatingDto tempBarRating)
//{
//    if (tempBarRating == null)
//    {
//        return null;
//    }
//    if (tempBarRating.Value == 0 || tempBarRating.UserId < 1 || tempBarRating.BarId < 1)
//    {
//        return null;
//    }
//    var rating = await this.GetRatingAsync(tempBarRating.BarId, tempBarRating.UserId);
//    if (rating == null)
//    {
//        var newBarRating = new BarRating
//        {
//            Id = tempBarRating.Id,
//            Value = tempBarRating.Value,
//            BarId = tempBarRating.BarId,
//            UserId = tempBarRating.UserId,
//            CreatedOn = this.dateTimeProvider.GetDateTime(),
//        };



//        await this.context.AddAsync(newBarRating);
//        await this.context.SaveChangesAsync();

//        var barRatingDto = this.dtoMapper.MapDto(newBarRating);

//        return barRatingDto;
//    }
//    var editdRating = await this.EditRatingAsync(tempBarRating.BarId, tempBarRating.UserId, tempBarRating.Value);
//    return editdRating;

//}