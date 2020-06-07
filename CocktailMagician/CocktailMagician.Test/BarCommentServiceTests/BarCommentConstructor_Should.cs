using System;
using System.Collections.Generic;
using System.Text;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CocktailMagician.Test.BarCommentTests
{
    [TestClass]
    public class BarCommentConstructor_Should
    {
        [TestMethod]
        public void Constructor_CreateInstance()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Constructor_CreateInstance));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarCommentDtoMapper = new Mock<IDtoMapper<BarComment, BarCommentDto>>();

            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                var sut = new BarCommentsService(assertContext, mockBarCommentDtoMapper.Object, mockDateTimeProvider.Object);
                Assert.IsNotNull(sut);
            }
        }
        [TestMethod]
        public void Throw_When_ContextIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_ContextIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockBarCommentDtoMapper = new Mock<IDtoMapper<BarComment, BarCommentDto>>();
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(()=> new BarCommentsService(null, mockBarCommentDtoMapper.Object, mockDateTimeProvider.Object));
            }
        }
        [TestMethod]
        public void Throw_When_ProviderIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_ProviderIsNull));
            var mockBarCommentDtoMapper = new Mock<IDtoMapper<BarComment, BarCommentDto>>();
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new BarCommentsService(assertContext, mockBarCommentDtoMapper.Object,null));
            }
        }
        [TestMethod]
        public void Throw_When_DtoMappertIsNull()
        {
            //Arrange
            var options = TestUtilities.GetOptions(nameof(Throw_When_DtoMappertIsNull));
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            //Act and Assert
            using (var assertContext = new CocktailMagicianContext(options))
            {
                Assert.ThrowsException<ArgumentNullException>(() => new BarCommentsService(assertContext, null, mockDateTimeProvider.Object));
            }
        }

    }
}
