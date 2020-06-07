using System;
using System.Collections.Generic;
using System.Text;
using CocktailMagician.Data;
using CocktailMagician.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CocktailMagician.Test.BarCocktailsServiceTests
{
    [TestClass]
    public class BarCocktailsConstructor_Should
    {
        [TestMethod]
        public void Constructor_CreateInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_CreateInstance));
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarCocktailsService(assertContext);

                Assert.IsNotNull(sut);
            }
        }
        [TestMethod]
        public void Throw_When_ContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_ContextIsNull));
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
               Assert.ThrowsException<ArgumentNullException>(()=> new BarCocktailsService(null));
            }
        }
    }
}
