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
    public class CocktailRatingDtoMapper_Should
    {
        [TestMethod]
        public void ReturnCorrectInstance_OfType_CocktailRatingDto()
        {
            //Arrange
            var sut = new CocktailRatingDtoMapper();
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
            var cocktailRating = new CocktailRating
            {
                CocktailId = cocktail.Id,
                UserId = user.Id,
                Value = 5,
            };

            //Act
            var result = sut.MapDto(cocktailRating);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CocktailRatingDto));
            Assert.AreEqual(result.CocktailId, cocktailRating.CocktailId);
            Assert.AreEqual(result.UserId, cocktailRating.UserId);
            Assert.AreEqual(result.Value, cocktailRating.Value);
        }
        [TestMethod]
        public void ReturnCorrectInstance_OfType_ICollectionCocktailRatingDto()
        {
            //Arrange
            var sut = new CocktailRatingDtoMapper();
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
            var cocktailRatings = new List<CocktailRating>
            {
                new CocktailRating
                {
                    CocktailId = cocktail1.Id,
                    UserId = user1.Id,
                    Value = 5,
                },
                new CocktailRating
                {
                    CocktailId = cocktail2.Id,
                    UserId = user2.Id,
                    Value = 4,
                },
            };
            //Act
            var result = sut.MapDto(cocktailRatings);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ICollection<CocktailRatingDto>));
            Assert.AreEqual(result.First().CocktailId, cocktailRatings[0].CocktailId);
            Assert.AreEqual(result.First().UserId, cocktailRatings[0].UserId);
            Assert.AreEqual(result.First().Value, cocktailRatings[0].Value);
            Assert.AreEqual(result.Last().CocktailId, cocktailRatings[1].CocktailId);
            Assert.AreEqual(result.Last().UserId, cocktailRatings[1].UserId);
            Assert.AreEqual(result.Last().Value, cocktailRatings[1].Value);
        }
    }
}
