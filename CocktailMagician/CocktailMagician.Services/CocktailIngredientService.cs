using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class CocktailIngredientService : ICocktailIngredientService
    {
        private readonly CocktailMagicianContext _contex;

        public CocktailIngredientService(CocktailMagicianContext contex)
        {
            this._contex = contex ?? throw new ArgumentNullException(nameof(contex));
        }

        public async Task<CocktailIngredient> CreateCocktailIngredientAsync(int cocktailId, int ingredientId ) 
        {
            var cocktailIngredient = new CocktailIngredient
            {
                CocktailId = cocktailId,
                IngredientId = ingredientId
            };

            await this._contex.CocktailIngredients.AddAsync(cocktailIngredient);
            await this._contex.SaveChangesAsync();

            return cocktailIngredient;
        }
    }
}
