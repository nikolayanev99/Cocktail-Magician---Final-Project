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
    public class BarDtoMapper_Should
    {
        [TestMethod]
        public void ReturnCorrectInstance_Of_BarDto()
        {
            //Arrange 
            var sut = new BarDTOMapper();
            var bar = new Bar
            {
                Id = 1,
                Name = "TestBar",
                Info = "TestInfo",

            };
            //Act
            var result = sut.MapDto(bar);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BarDTO));
            Assert.AreEqual(result.Id, bar.Id);
            Assert.AreEqual(result.Name, bar.Name);
            Assert.AreEqual(result.Info, bar.Info);
        }
        [TestMethod]
        public void ReturnCorrectInstance_OfType_ICollectionBarDto()
        {

            //Arrange 
            var sut = new BarDTOMapper();
            var bars = new List<Bar>
            {
                 new Bar

                 {
                     Id = 1,
                     Name = "TestBar1",
                     Info = "TestInfo1",

                 },
                  new Bar
                 {
                     Id = 2,
                     Name = "TestBar2",
                     Info = "TestInfo2",

                 },
            };

            //Act
            var result = sut.MapDto(bars);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ICollection<BarDTO>));
            Assert.AreEqual(result.First().Id, bars[0].Id);
            Assert.AreEqual(result.First().Name, bars[0].Name);
            Assert.AreEqual(result.First().Info, bars[0].Info);
            Assert.AreEqual(result.Last().Id, bars[1].Id);
            Assert.AreEqual(result.Last().Name, bars[1].Name);
            Assert.AreEqual(result.Last().Info, bars[1].Info);
        }
    }
}
