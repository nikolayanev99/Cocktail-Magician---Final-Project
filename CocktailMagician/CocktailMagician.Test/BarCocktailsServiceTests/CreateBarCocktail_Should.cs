using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Test.BarCocktailsServiceTests
{
    [TestClass]
    public class CreateBarCocktail_Should
    {
        [TestMethod]
        public async Task Create_BarCocktail_Correctly()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Create_BarCocktail_Correctly));

            var bar = new Bar { Id = 1 };
            var cocktail = new Cocktail { Id = 1 };

            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarCocktailsService(assertContext);

                var result = await sut.CreateBarCocktail(bar.Id, cocktail.Id);
                Assert.IsInstanceOfType(result, typeof(BarCocktail));
                Assert.AreEqual(1, result.BarId);
                Assert.AreEqual(1, result.CocktailId);
            }
        }
        [TestMethod]
        public async Task ReturnNull_When_BarIdIsInvalid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_BarIdIsInvalid));

            var bar = new Bar { Id = 0 };
            var cocktail = new Cocktail { Id = 1 };

            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarCocktailsService(assertContext);

                var result = await sut.CreateBarCocktail(bar.Id, cocktail.Id);
                Assert.IsNull(result);
            }

        }
        [TestMethod]
        public async Task ReturnNull_When_CocktailIdIsInvalid()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(ReturnNull_When_CocktailIdIsInvalid));

            var bar = new Bar { Id = 1 };
            var cocktail = new Cocktail { Id = 0 };

            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarCocktailsService(assertContext);

                var result = await sut.CreateBarCocktail(bar.Id, cocktail.Id);
                Assert.IsNull(result);
            }
        }
    }
}
