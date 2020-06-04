using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly CocktailMagicianContext _context;
        private readonly IDtoMapper<Ingredient, IngredientDto> _ingredientDtoMapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public IngredientService(CocktailMagicianContext context, IDtoMapper<Ingredient, IngredientDto> ingredientDtoMapper, IDateTimeProvider dateTimeProvider)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._ingredientDtoMapper = ingredientDtoMapper ?? throw new ArgumentNullException(nameof(ingredientDtoMapper));
            this._dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<IngredientDto> GetIngredientAsyng(int id)
        {
            var ingredient = await this._context.Ingredients
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(ii => ii.Id == id);

            if (ingredient == null)
            {
                return null;
            }

            var ingredientDto = this._ingredientDtoMapper.MapDto(ingredient);
            return ingredientDto;
        }

        public async Task<Ingredient> GetIngredientByNameAsync(string ingredientName)
        {
            var ingredient = await this._context.Ingredients
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Name == ingredientName);

            if (ingredient == null)
            {
                return null;
            }

            return ingredient;
        }

        public async Task<ICollection<IngredientDto>> GetAllIngredientsAsync() 
        {
            var ingredients = await this._context.Ingredients
                .Where(i => i.IsDeleted == false)
                .ToListAsync();

            if (!ingredients.Any())
            {
                throw new ArgumentNullException("No ingredients are found");
            }

            var ingredientsDto = this._ingredientDtoMapper.MapDto(ingredients);

            return ingredientsDto;
        }

        public async Task<IngredientDto> CreateIngredientAsync(IngredientDto ingredientDto)
        {
            var ingredient = new Ingredient
            {
                Name = ingredientDto.Name
            };

            await this._context.Ingredients.AddAsync(ingredient);
            await this._context.SaveChangesAsync();

            var newIngredientDto = this._ingredientDtoMapper.MapDto(ingredient);
            return newIngredientDto;
        }

        public async Task<IngredientDto> EditIngredientAsync(int id, string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentNullException("The new name is invalid");
            }

            var ingredient = this._context.Ingredients
                .Where(i => i.IsDeleted == false)
                .FirstOrDefault(ii => ii.Id == id);

            if (ingredient == null)
            {
                return null;
            }

            try
            {
                ingredient.Name = newName;

                this._context.Update(ingredient);
                await this._context.SaveChangesAsync();

                var ingredientDto = this._ingredientDtoMapper.MapDto(ingredient);

                return ingredientDto;
            }
            catch (Exception)
            {

                throw new ArgumentException();
            }
        }

        public async Task<IngredientDto> DeleteIngredientAsync(int id) 
        {
            var ingredient = await this._context.Ingredients
                .Where(i => i.IsDeleted == false)
                .Include(ii => ii.CocktailIngredients)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (ingredient.CocktailIngredients.Count > 0)
            {
                throw new ArgumentException("There are cocktails using this ingredient");
            }

            ingredient.IsDeleted = true;
            ingredient.DeletedOn = this._dateTimeProvider.GetDateTime();
            this._context.Update(ingredient);

            await this._context.SaveChangesAsync();

            var ingredientDto = this._ingredientDtoMapper.MapDto(ingredient);

            return ingredientDto;
        }

        public async Task<ICollection<IngredientDto>> GetIngredientsForPeginationAsync(int pageSize = 1, int pageNumber = 1)
        {
            int excludeRecodrds = (pageSize * pageNumber) - pageSize;

            var ingredients = await this._context.Ingredients
                .Where(v => v.IsDeleted == false)
                .OrderBy(n => n.Name)
                .Skip(excludeRecodrds)
                .Take(pageSize)
                .ToListAsync();

            var intredientDto = this._ingredientDtoMapper.MapDto(ingredients);

            return intredientDto;
        }
    }
}
