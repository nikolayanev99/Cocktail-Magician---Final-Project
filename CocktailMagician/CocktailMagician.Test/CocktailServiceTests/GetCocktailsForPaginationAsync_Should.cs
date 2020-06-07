using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.CocktailServiceTests
{
    [TestClass]
    public class GetCocktailsForPaginationAsync_Should
    {
        [TestMethod]
        public async Task Return_CorrectCollection_When_ParamsAreValid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Return_CorrectCollection_When_ParamsAreValid));
            var mockCocktailDtoMapper = new Mock<IDtoMapper<Cocktail, CocktailDto>>();
            var mockCocktailIngretientService = new Mock<ICocktailIngredientService>();
            var mockIngredientsService = new Mock<IIngredientService>();
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

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
            var cocktail3 = new Cocktail
            {
                Id = 3,
                Name = "TestCocktail3",
            };
            var list = new List<CocktailDto>
            {
                new CocktailDto
                {
                    Id = 1,
                    Name = "TestCocktail1",
                },
                new CocktailDto
                {
                    Id = 2,
                    Name = "TestCocktail2",
                }
            };
            mockCocktailDtoMapper.Setup(x => x.MapDto(It.IsAny<ICollection<Cocktail>>())).Returns(list);
            using (var arrangeContext = new CocktailMagicianContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail1);
                await arrangeContext.Cocktails.AddAsync(cocktail2);
                await arrangeContext.Cocktails.AddAsync(cocktail3);
                await arrangeContext.SaveChangesAsync();
            }
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailService(assertContext, mockCocktailDtoMapper.Object, mockDateTimeProvider.Object, mockCocktailIngretientService.Object, mockIngredientsService.Object);
                var result = await sut.GetCocktailsForPeginationAsync(2, 1);

                Assert.IsInstanceOfType(result, typeof(ICollection<CocktailDto>));
                Assert.AreEqual(1, result.First().Id);
                Assert.AreEqual("TestCocktail1", result.First().Name);
                Assert.AreEqual(2, result.Last().Id);
                Assert.AreEqual("TestCocktail2", result.Last().Name);
            }
        }
    }
}
