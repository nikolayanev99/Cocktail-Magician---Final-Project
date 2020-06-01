using System;
using System.Collections.Generic;
using System.Text;
using CocktailMagician.Models.Abstract;

namespace CocktailMagician.Models
{
    public class BarCocktail:Entity
    {
        public int BarId { get; set; }
        public Bar Bar { get; set; }

        public int CocktailId { get; set; }

        public Cocktail Cocktail { get; set; }
    }
}
