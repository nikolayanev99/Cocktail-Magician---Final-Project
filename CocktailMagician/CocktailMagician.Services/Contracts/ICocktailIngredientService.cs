using CocktailMagician.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailIngredientService
    {
        public Task<CocktailIngredient> CreateCocktailIngredientAsync(int cocktailId, int ingredientId);
    }
}
