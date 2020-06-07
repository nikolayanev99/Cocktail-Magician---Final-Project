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

namespace CocktailMagician.Test.BarCommentTests
{
    [TestClass]
    public class CreateCommentAsync_Should
    {
        [TestMethod]
        public async Task Create_Comment_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Create_Comment_Correctly));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarCommentDtoMapper = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            var user = new User { Id = 1 };
            var bar = new Bar { Id = 1 };

            var commentDto = new BarCommentDto
            {
                Id = 1,
                UserId = 1,
                BarId = 1,
                Text = "TestComment",
            };

            mockBarCommentDtoMapper.Setup(x => x.MapDto(It.IsAny<BarComment>())).Returns(commentDto);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarCommentsService(assertContext, mockBarCommentDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateCommentAsync(commentDto);

                Assert.IsInstanceOfType(result, typeof(BarCommentDto));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(1, result.BarId);
                Assert.AreEqual(1, result.UserId);
                Assert.AreEqual("TestComment", result.Text);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_DtoIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_DtoIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarCommentDtoMapper = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarCommentsService(assertContext, mockBarCommentDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateCommentAsync(null);

                Assert.IsNull(result);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_DtoParamsAreInvalid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_DtoParamsAreInvalid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarCommentDtoMapper = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            var user = new User { Id = 1 };
            var bar = new Bar { Id = 1 };

            var commentDto = new BarCommentDto
            {
                Id = 1,
                UserId = 0,
                BarId = 0,
                Text = null,
            };

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarCommentsService(assertContext, mockBarCommentDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateCommentAsync(null);

                Assert.IsNull(result);
            }
        }
    }
}
