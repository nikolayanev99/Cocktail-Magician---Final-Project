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
    public class BarRatingDtoMapper_Should
    {
        [TestMethod]
        public void ReturnCorrectInstance_OfType_BarRatingDto()
        {
            //Arrange
            var sut = new BarRatingDtoMapper();

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
            var barRating = new BarRating
            {
                BarId = 1,
                UserId = 1,
                Value = 5,
                CreatedOn = DateTime.MinValue,
            };

            //Act
            var result = sut.MapDto(barRating);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BarRatingDto));
            Assert.AreEqual(result.BarId, barRating.BarId);
            Assert.AreEqual(result.UserId, barRating.UserId);
            Assert.AreEqual(result.Value, barRating.Value);
        }
        [TestMethod]
        public void ReturnCorrectInstance_OfType_ICollectionBarRatingDto()
        {
            //Arrange
            var sut = new BarRatingDtoMapper();
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
            var barRatings = new List<BarRating>
            {
                   new BarRating
                   {
                   BarId = 1,
                   UserId = 1,
                   Value=2,
                   CreatedOn = DateTime.MinValue,
                   },

                  new BarRating
                  {
                   BarId = 2,
                   UserId = 2,
                   Value=5,
                   CreatedOn = DateTime.MinValue,
                  }
            };
            //Act
            var result = sut.MapDto(barRatings);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ICollection<BarRatingDto>));
            Assert.AreEqual(result.First().BarId, barRatings[0].BarId);
            Assert.AreEqual(result.First().UserId, barRatings[0].UserId);
            Assert.AreEqual(result.First().Value, barRatings[0].Value);
            Assert.AreEqual(result.Last().BarId, barRatings[1].BarId);
            Assert.AreEqual(result.Last().UserId, barRatings[1].UserId);
            Assert.AreEqual(result.Last().Value, barRatings[1].Value);
        }
    }
}
