using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Test.DtoMappersTests
{
    [TestClass]
    public class CocktailCommentDtoMapper_Should
    {
        [TestMethod]
        public void ReturnCorrectInstance_OfType_CocktailCommentDto()
        {
            //Arrange
            var sut = new CocktailCommentDtoMapper();
            var cocktail = new Cocktail
            {
                Id = 1,
                Name = "TestCocktail",

            };
            var user = new User
            {
                Id = 1,
                UserName = "testuser@test.test",
                Email = "testuser@test.test",
            };
            var cocktailComment = new CocktailComment
            {
                CocktailId = cocktail.Id,
                UserId = user.Id,
                commentText = "TestComment",
                User = user,
            };

            //Act
            var result = sut.MapDto(cocktailComment);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CocktailCommentDto));
            Assert.AreEqual(result.CocktailId, cocktailComment.CocktailId);
            Assert.AreEqual(result.UserId, cocktailComment.UserId);
            Assert.AreEqual(result.commentText, cocktailComment.commentText);

        }
        [TestMethod]
        public void ReturnCorrectInstance_OfType_ICollectionCocktailCommentDto()
        {

            //Arrange
            var sut = new CocktailCommentDtoMapper();
            var cocktail1 = new Cocktail
            {
                Id = 1,
                Name = "TestCocktail1",

            };
            var cocktail2 = new Cocktail
            {
                Id = 2,
                Name = "TestCocktail2",

            };
            var user1 = new User
            {
                Id = 1,
                UserName = "testuser1@test.test",
                Email = "testuser1@test.test",
            };
            var user2 = new User
            {
                Id = 2,
                UserName = "testuser2@test.test",
                Email = "testuser2@test.test",
            };
            var cocktailComments = new List<CocktailComment>
            {
                new CocktailComment
                {
                    CocktailId = cocktail1.Id,
                    UserId = user1.Id,
                    commentText = "TestComment1",
                    User = user1,
                },
                new CocktailComment
                {
                    CocktailId = cocktail2.Id,
                    UserId = user2.Id,
                    commentText = "TestComment2",
                    User = user2,
                },
            };

            //Act
            var result = sut.MapDto(cocktailComments);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ICollection<CocktailCommentDto>));
            Assert.AreEqual(result.First().CocktailId, cocktailComments[0].CocktailId);
            Assert.AreEqual(result.First().UserId, cocktailComments[0].UserId);
            Assert.AreEqual(result.First().commentText, cocktailComments[0].commentText);
            Assert.AreEqual(result.Last().CocktailId, cocktailComments[1].CocktailId);
            Assert.AreEqual(result.Last().UserId, cocktailComments[1].UserId);
            Assert.AreEqual(result.Last().commentText, cocktailComments[1].commentText);
        }
    }
}
