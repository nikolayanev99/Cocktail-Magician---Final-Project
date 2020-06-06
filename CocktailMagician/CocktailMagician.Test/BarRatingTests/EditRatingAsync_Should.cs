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
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.BarRatingTests
{
    [TestClass]
    public class EditRatingAsync_Should
    {
        [TestMethod]
        public async Task Update_Rating_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Update_Rating_Correctly));
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
                Value = 4,
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
                var result = await sut.EditRatingAsync(1, 1, 4);
                var editedRating = await assertContext.BarRatings.FirstAsync();

                Assert.IsInstanceOfType(result, typeof(BarRatingDto));
                Assert.AreEqual(4, editedRating.Value);
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
                var result = await sut.EditRatingAsync(1, 1, 4);

                Assert.IsNull(result);
            }
        }
    }
}
//public async Task<BarRatingDto> EditRatingAsync(int barId, int userId, double newValue)
//{
//    var rating = await this.context.BarRatings
//        .Include(r => r.User)
//        .Include(r => r.Bar)
//        .Where(r => r.IsDeleted == false)
//        .FirstOrDefaultAsync(r => r.UserId == userId && r.BarId == barId);

//    if (rating == null)
//    {
//        return null;
//    }

//    rating.Value = newValue;

//    this.context.Update(rating);
//    await this.context.SaveChangesAsync();

//    var ratingDto = this.dtoMapper.MapDto(rating);
//    return ratingDto;
//}