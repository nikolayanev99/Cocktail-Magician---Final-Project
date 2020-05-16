using CocktailMagician.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Models
{
    public class CocktailIngredient : IDeleted
    {
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
