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
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Test.CocktailCommentTests
{
    [TestClass]
    public class CreateCommentAsync_Should
    {
        [TestMethod]
        public async Task Create_CocktailComment_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Create_CocktailComment_Correctly));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            var user = new User { Id = 1 };
            var cocktail = new Cocktail { Id = 1 };

            var commentDto = new CocktailCommentDto
            {
                Id = 1,
                UserId = 1,
                CocktailId = 1,
                commentText = "TestComment",
            };

            mapper.Setup(x => x.MapDto(It.IsAny<CocktailComment>())).Returns(commentDto);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailCommentService(assertContext, mapper.Object, mockDateTimeProvider.Object);
                var result = await sut.CreateCocktailCommentAsync(commentDto);

                Assert.IsInstanceOfType(result, typeof(CocktailCommentDto));
                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(1, result.CocktailId);
                Assert.AreEqual(1, result.UserId);
                Assert.AreEqual("TestComment", result.commentText);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_CocktailDtoIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_CocktailDtoIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailCommentService(assertContext, mapper.Object, mockDateTimeProvider.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateCocktailCommentAsync(null));
            }
        }
    }
}

