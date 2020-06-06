using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IIngredientService
    {
        public Task<IngredientDto> GetIngredientAsyng(int id);
        public Task<ICollection<IngredientDto>> GetAllIngredientsAsync();
        public Task<IngredientDto> CreateIngredientAsync(IngredientDto ingredientDto);
        public Task<IngredientDto> EditIngredientAsync(int id, string newName);
        public Task<IngredientDto> DeleteIngredientAsync(int id);
        public Task<Ingredient> GetIngredientByNameAsync(string ingredientName);
        public Task<ICollection<IngredientDto>> GetIngredientsForPeginationAsync(int pageSize = 1, int pageNumber = 1);
        public Task<ICollection<IngredientDto>> GetCocktailIngredientsAsync(int cocktailId);
    }
}
