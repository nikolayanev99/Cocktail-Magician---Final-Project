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
    public class CocktailDtoMapper_Should
    {
        [TestMethod]
        public void ReturnCorrectInstance_OfType_CocktailDto()
        {
            //Arrange
            var sut = new CocktailDtoMapper();

            var cocktail = new Cocktail
            {
                Id = 1,
                Name = "TestCocktail1",
            };

            //Act
            var result = sut.MapDto(cocktail);

            //Assert

            Assert.IsInstanceOfType(result, typeof(CocktailDto));
            Assert.AreEqual(result.Id, cocktail.Id);
            Assert.AreEqual(result.Name, cocktail.Name);
        }
        [TestMethod]
        public void ReturnCorrectInstance_OfType_ICollectionCocktailDto()
        {
            //Arrange
            var sut = new CocktailDtoMapper();

            var cocktails = new List<Cocktail>
            {
                new Cocktail
                {
                    Id = 1,
                    Name = "TestCocktail1",
                },
                new Cocktail
                {
                    Id = 2,
                    Name = "TestCocktail2",
                },
            };
            //Act 
            var result = sut.MapDto(cocktails);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ICollection<CocktailDto>));
            Assert.AreEqual(result.First().Id, cocktails[0].Id);
            Assert.AreEqual(result.First().Name, cocktails[0].Name);
            Assert.AreEqual(result.Last().Id, cocktails[1].Id);
            Assert.AreEqual(result.Last().Name, cocktails[1].Name);
        }
    }
}
