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

namespace CocktailMagician.Test.IngredientsServiceTests
{
    [TestClass]
    public class GetCocktailIngredientsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrectCountOfIngredientsInCocktail()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectCountOfIngredientsInCocktail));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var ingredient1 = new Ingredient
            {
                Id = 1,
                Name = "Cola",
                IsDeleted = false
            };
            var ingredient2 = new Ingredient
            {
                Id = 2,
                Name = "Ice",
                IsDeleted = false
            };
            var cocktail = new Cocktail
            {
                Id = 1,
                Name = "IceCola",
                ShortDescription = "iceiceBaby"

            };
            var cocktailIngr1 = new CocktailIngredient
            {
                CocktailId = cocktail.Id,
                IngredientId = ingredient1.Id,
            };
            var cocktailIngr2 = new CocktailIngredient
            {
                CocktailId = cocktail.Id,
                IngredientId = ingredient2.Id,
            };

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient1);
                await arrangeContext.Ingredients.AddAsync(ingredient2);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.CocktailIngredients.AddAsync(cocktailIngr1);
                await arrangeContext.CocktailIngredients.AddAsync(cocktailIngr2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);

                var result = await sut.GetCocktailIngredientsAsync(1);

                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public async Task ReturnInstanceOfCollectionIngredientDto()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnInstanceOfCollectionIngredientDto));
            var mapper = new Mock<IDtoMapper<Ingredient, IngredientDto>>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            var ingredient1 = new Ingredient
            {
                Id = 1,
                Name = "Cola",
                IsDeleted = false
            };
            var ingredient2 = new Ingredient
            {
                Id = 2,
                Name = "Ice",
                IsDeleted = false
            };
            var cocktail = new Cocktail
            {
                Id = 1,
                Name = "IceCola",
                ShortDescription = "iceiceBaby"

            };
            var cocktailIngr1 = new CocktailIngredient
            {
                CocktailId = cocktail.Id,
                IngredientId = ingredient1.Id,
            };
            var cocktailIngr2 = new CocktailIngredient
            {
                CocktailId = cocktail.Id,
                IngredientId = ingredient2.Id,
            };

            var ingredientsDto = new List<IngredientDto>
            {
                new IngredientDto{ Id=1, Name="Cola"}, new IngredientDto{Id=2, Name="Ice"}
            };

            mapper.Setup(x => x.MapDto(It.IsAny<ICollection<Ingredient>>())).Returns(ingredientsDto);

            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient1);
                await arrangeContext.Ingredients.AddAsync(ingredient2);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.CocktailIngredients.AddAsync(cocktailIngr1);
                await arrangeContext.CocktailIngredients.AddAsync(cocktailIngr2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CocktailMagicianContext(options))
            {
                //Act & Assert
                var sut = new IngredientService(assertContext, mapper.Object, mockDateTimeProvider.Object);

                var result = await sut.GetCocktailIngredientsAsync(1);

                Assert.IsInstanceOfType(result, typeof(ICollection<IngredientDto>));

            }
        }
    }
}
