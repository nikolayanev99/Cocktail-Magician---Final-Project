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

namespace CocktailMagician.Test.BarCommentTests
{
    [TestClass]
    public class GetBarCommentsAsync_Should
    {
        [TestMethod]
        public async Task Return_CorrectModel_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_CorrectModel_When_ParamsAreValid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarCommentDtoMapper = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            var user = new User { Id = 1 };
            var bar = new Bar { Id = 1 };

            var comment1 = new BarComment
            {
                Id = 1,
                UserId = 1,
                BarId = 1,
                Text = "TestComment1",
            };
            var comment2 = new BarComment
            {
                Id = 2,
                UserId = 1,
                BarId = 1,
                Text = "TestComment2",
            };
            var list = new List<BarCommentDto>
            {
                new BarCommentDto
                {
                    Id = 1,
                    UserId = 1,
                    BarId = 1,
                    Text = "TestComment1",
                },
                new BarCommentDto
                {
                    Id = 2,
                    UserId = 1,
                    BarId = 1,
                    Text = "TestComment2",
                }
            };
            mockBarCommentDtoMapper.Setup(x => x.MapDto(It.IsAny<ICollection<BarComment>>())).Returns(list);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Bars.AddAsync(bar);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.BarComments.AddAsync(comment1);
                await arrangeContext.BarComments.AddAsync(comment2);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarCommentsService(assertContext, mockBarCommentDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetBarCommentsAsync(1);

                Assert.IsInstanceOfType(result, typeof(ICollection<BarCommentDto>));
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual(1, result.First().UserId);
                Assert.AreEqual(1, result.First().BarId);
                Assert.AreEqual("TestComment1", result.First().Text);
                Assert.AreEqual(2, result.Last().Id);
                Assert.AreEqual(1, result.Last().UserId);
                Assert.AreEqual(1, result.Last().BarId);
                Assert.AreEqual("TestComment2", result.Last().Text);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_NoCommentsFound()
        {

            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_NoCommentsFound));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarCommentDtoMapper = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarCommentsService(assertContext, mockBarCommentDtoMapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetBarCommentsAsync(1);

                Assert.IsNull(result);
            }
        }
    }
}
