using System;
using System.Collections.Generic;
using System.Text;
using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Test.DtoMappersTests
{
    [TestClass]
    public class BarCommentDtoMapper_Should
    {
        [TestMethod]
        public void ReturnCorrectInstance_OfType_BarCommentDto()
        {
            //Arrange 
            var sut = new BarCommentDtoMapper();
            var bar = new Bar
            {
                Id = 1,
                Name = "TestBar",
                Info = "TestInfo",

            };
            var user = new User
            {
                Id = 1,
                UserName = "testuser@test.test",
                Email = "testuser@test.test",
            };
            var barComment = new BarComment
            {
                BarId = 1,
                UserId = 1,
                Text = "TestComment",
                Author = user,
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapDto(barComment);
            //Assert
            Assert.IsInstanceOfType(result, typeof(BarCommentDto));
        }
        [TestMethod]
        public void Correctly_Map_FromBarComment_ToBarCommentDto()
        {
            //Arrange 
            var sut = new BarCommentDtoMapper();
            var bar = new Bar
            {
                Id = 1,
                Name = "TestBar",
                Info = "TestInfo",

            };
            var user = new User
            {
                Id = 1,
                UserName = "testuser@test.test",
                Email = "testuser@test.test",
            };
            var barComment = new BarComment
            {
                BarId = 1,
                UserId = 1,
                Text = "TestComment",
                Author = user,
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapDto(barComment);
            //Assert
            Assert.AreEqual(result.BarId, barComment.BarId);
            Assert.AreEqual(result.UserId, barComment.UserId);
            Assert.AreEqual(result.Text, barComment.Text);
            Assert.AreEqual(result.CreatedOn, barComment.CreatedOn);
        }
        [TestMethod]
        public void ReturnCorrectInstance_OfType_ICollectionBarCommentDto()
        {
            //Arrange 
            var sut = new BarCommentDtoMapper();
            var bar1 = new Bar
            {
                Id = 1,
                Name = "TestBar1",
                Info = "TestInfo1",

            };
            var bar2 = new Bar
            {
                Id = 2,
                Name = "TestBar2",
                Info = "TestInfo2",

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
            var barComments = new List<BarComment>
            {
                   new BarComment
                   {
                   BarId = 1,
                   UserId = 1,
                   Text = "TestComment1",
                   Author = user1,
                   CreatedOn = DateTime.MinValue,
                   },

                  new BarComment
                  {
                   BarId = 2,
                   UserId = 2,
                   Text = "TestComment2",
                   Author = user2,
                   CreatedOn = DateTime.MinValue,
                  }
            };
            //Act
            var result = sut.MapDto(barComments);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ICollection<BarCommentDto>));
        }
    }
}
