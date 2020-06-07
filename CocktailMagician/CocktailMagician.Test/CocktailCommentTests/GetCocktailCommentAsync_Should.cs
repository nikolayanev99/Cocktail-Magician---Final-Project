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

namespace CocktailMagician.Test.CocktailCommentTests
{
    [TestClass]
    public class GetCocktailCommentAsync_Should
    {
        [TestMethod]
        public async Task Return_CorrectModelComment_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_CorrectModelComment_When_ParamsAreValid));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            var user = new User { Id = 1 };
            var cocktail = new Cocktail { Id = 1 };

            var comment1 = new CocktailComment
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                commentText = "TestComment1",
            };
            var comment2 = new CocktailComment

            {
                Id = 2,
                UserId = 1,
                CocktailId = 1,
                commentText = "TestComment2",
            };
            var list = new List<CocktailCommentDto>
            {
                new CocktailCommentDto
                {
                    Id = 1,
                    UserId = 1,
                    CocktailId = 1,
                    commentText = "TestComment1",
                },
                new CocktailCommentDto
                {
                    Id = 2,
                    UserId = 1,
                    CocktailId = 1,
                    commentText = "TestComment2",
                }
            };
            mapper.Setup(x => x.MapDto(It.IsAny<ICollection<CocktailComment>>())).Returns(list);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.CocktailComments.AddAsync(comment1);
                await arrangeContext.CocktailComments.AddAsync(comment2);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailCommentService(assertContext, mapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetCocktailCommentsAsync(1);

                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailCommentDto>));
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual(1, result.First().UserId);
                Assert.AreEqual(1, result.First().CocktailId);
                Assert.AreEqual("TestComment1", result.First().commentText);
                Assert.AreEqual(2, result.Last().Id);
                Assert.AreEqual(1, result.Last().UserId);
                Assert.AreEqual(1, result.Last().CocktailId);
                Assert.AreEqual("TestComment2", result.Last().commentText);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_NoCommentsFoundInCocktail()
        {

            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_NoCommentsFoundInCocktail));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailCommentService(assertContext, mapper.Object, mockDateTimeProvider.Object);
                var result = await sut.GetCocktailCommentsAsync(1);

                Assert.IsNull(result);
            }
        }
    }
}
