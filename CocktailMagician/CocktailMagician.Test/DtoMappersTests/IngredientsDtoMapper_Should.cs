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
    public class IngredientsDtoMapper_Should
    {
        [TestMethod]
        public void ReturnCorrectInstance_OfType_IngredientDto()
        {
            //Arrange
            var sut = new IngredientDtoMapper();

            var ingredient = new Ingredient
            {
                Id = 1,
                Name = "TestIngredient",
            };

            //Act
            var result = sut.MapDto(ingredient);

            //Assert
            Assert.IsInstanceOfType(result, typeof(IngredientDto));
            Assert.AreEqual(result.Id, ingredient.Id);
            Assert.AreEqual(result.Name, ingredient.Name);

        }
        [TestMethod]
        public void ReturnCorrectInstance_OfType_ICollectionIngredientDto()
        {
            //Arrange
            var sut = new IngredientDtoMapper();

            var ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    Id = 1,
                    Name = "TestIngredient1",
                },
                new Ingredient
                {
                    Id = 2,
                    Name = "TestIngredient2",
                },
            };
            //Act
            var result = sut.MapDto(ingredients);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ICollection<IngredientDto>));
            Assert.AreEqual(result.First().Id, ingredients[0].Id);
            Assert.AreEqual(result.First().Name, ingredients[0].Name);
            Assert.AreEqual(result.Last().Id, ingredients[1].Id);
            Assert.AreEqual(result.Last().Name, ingredients[1].Name);
        }
    }
}
