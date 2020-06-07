using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Test.CocktailCommentTests
{
    [TestClass]
    public class UpdateCocktailCommentAsync_Should
    {
        [TestMethod]
        public async Task CorrectlyUpdateCocktailCommentModel()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(CorrectlyUpdateCocktailCommentModel));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            var comment1 = new CocktailComment
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                commentText = "TestComment1",
            };

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.CocktailComments.AddAsync(comment1);
                await arrangeContext.SaveChangesAsync();
            }

            mapper.Setup(x => x.MapDto(It.IsAny<CocktailComment>())).Returns(It.IsAny<CocktailCommentDto>);

            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailCommentService(assertContext, mapper.Object, mockDateTimeProvider.Object);

                var result = await sut.UpdateCocktailComment(1, "TestComment2");

                var newComment = await assertContext.CocktailComments.FirstAsync();

                Assert.AreEqual("TestComment2", newComment.commentText);

            }
        }

        [TestMethod]
        public async Task ReturnCorrectTypeOfModel()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectTypeOfModel));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();
            var createdOn = DateTime.UtcNow;

            var comment1 = new CocktailComment
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                commentText = "TestComment1",
                CreatedOn = createdOn
            };

            var commentDto = new CocktailCommentDto
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                commentText = "TestComment1",
                Username = "ColaFan",
                CreatedOn = createdOn
            };

            mapper.Setup(x => x.MapDto(It.IsAny<CocktailComment>())).Returns(commentDto);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.CocktailComments.AddAsync(comment1);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                var sut = new CocktailCommentService(assertContext, mapper.Object, mockDateTimeProvider.Object);

                var result = await sut.UpdateCocktailComment(1, "TestComment2");
                Assert.IsInstanceOfType(result, typeof(CocktailCommentDto));
            }
        }

        [TestMethod]
        public async Task ThrowExWhen_NoCommentFound()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ThrowExWhen_NoCommentFound));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();
            var createdOn = DateTime.UtcNow;

            var comment1 = new CocktailComment
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                commentText = "TestComment1",
                CreatedOn = createdOn
            };

            mapper.Setup(x => x.MapDto(It.IsAny<CocktailComment>())).Returns(It.IsAny<CocktailCommentDto>);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.CocktailComments.AddAsync(comment1);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                var sut = new CocktailCommentService(assertContext, mapper.Object, mockDateTimeProvider.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.UpdateCocktailComment(2, "TestComment2"));
            }
        }
    }
}
