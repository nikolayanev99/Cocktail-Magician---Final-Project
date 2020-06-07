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
using System.Text;

namespace CocktailMagician.Test.CocktailCommentTests
{
    [TestClass]
    public class CocktailCommentConstructor_Should
    {

        [TestMethod]
        public void Constructor_CreateCorrectInstanceOfCocktailComment()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_CreateCorrectInstanceOfCocktailComment));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new CocktailCommentService(assertContext, mapper.Object, mockDateTimeProvider.Object);
                Assert.IsNotNull(sut);
            }
        }

        [TestMethod]
        public void Throw_When_ContextIsNullInCocktailComment()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_ContextIsNullInCocktailComment));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mapper = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailCommentService(null, mapper.Object, mockDateTimeProvider.Object));
            }
        }

        [TestMethod]
        public void Throw_When_ProviderIsNullInCocktailComment()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_ProviderIsNullInCocktailComment));
            var mapper = new Mock<IDtoMapper<CocktailComment, CocktailCommentDto>>();
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailCommentService(assertContext, mapper.Object, null));
            }
        }

        [TestMethod]
        public void Throw_When_DtoMappertIsNullInCocktailComment()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_DtoMappertIsNullInCocktailComment));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new CocktailCommentService(assertContext, null, mockDateTimeProvider.Object));
            }
        }
    }
}
