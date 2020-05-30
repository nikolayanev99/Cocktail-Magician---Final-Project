using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class CocktailService : ICocktailService
    {
        private readonly CocktailMagicianContext _context;
        private readonly IDtoMapper<Cocktail, CocktailDto> _cocktailDtoMapper;
        private readonly IDateTimeProvider _provider;
        private readonly ICocktailIngredientService _cocktailIngredientService;
        private readonly IIngredientService _ingredientService;

        public CocktailService(CocktailMagicianContext context, IDtoMapper<Cocktail, CocktailDto> cocktailDtoMapper, IDateTimeProvider provider, ICocktailIngredientService cocktailIngredientService, IIngredientService ingredientService)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._cocktailDtoMapper = cocktailDtoMapper ?? throw new ArgumentNullException(nameof(cocktailDtoMapper));
            this._provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this._cocktailIngredientService = cocktailIngredientService ?? throw new ArgumentNullException(nameof(cocktailIngredientService));
            this._ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
        }

        public async Task<CocktailDto> GetCokctailAsync(int id)
        {
            var cocktail = await this._context.Cocktails
                .Where(v => v.IsDeleted == false)
                .Include(i => i.CocktailIngredients)
                .ThenInclude(ii => ii.Ingredient)
                .Include(c => c.CocktailComments)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (cocktail == null)
            {
                throw new ArgumentNullException("No entity found");
            }

            var cocktailDto = this._cocktailDtoMapper.MapDto(cocktail);

            return cocktailDto;
        }

        public async Task<ICollection<CocktailDto>> GetAllCocktailsAsync()
        {
            var cocktails = await this._context.Cocktails
                .Where(v => v.IsDeleted == false)
                .OrderBy(n => n.Name)
                .ToListAsync();

            var cocktailDto = this._cocktailDtoMapper.MapDto(cocktails);

            return cocktailDto;
        }

        public async Task<ICollection<CocktailDto>> GetCocktailsForPeginationAsync(int pageSize = 1, int pageNumber = 1)
        {
            int excludeRecodrds = (pageSize * pageNumber) - pageSize;
            
            var cocktails = await this._context.Cocktails
                .Where(v => v.IsDeleted == false)
                .OrderBy(n => n.Name)
                .Skip(excludeRecodrds)
                .Take(pageSize)
                .ToListAsync();

            var cocktailDto = this._cocktailDtoMapper.MapDto(cocktails);

            return cocktailDto;
        }

        public async Task<CocktailDto> CreateCocktailAsync(CocktailDto tempCocktailDto)
        {
            if (tempCocktailDto == null)
            {
                throw new ArgumentNullException("No entity found");
            }

            if (tempCocktailDto.Ingredients.Count == 0)
            {
                throw new ArgumentNullException("The ingredients are missing");
            }

            var cocktail = new Cocktail
            {
                Name = tempCocktailDto.Name,
                ShortDescription = tempCocktailDto.ShortDescription,
                ImageUrl = tempCocktailDto.ImageUrl,
                ImageThumbnailUrl = tempCocktailDto.ImageThumbnailUrl,
            };

            await _context.Cocktails.AddAsync(cocktail);
            await _context.SaveChangesAsync();

            foreach (var item in tempCocktailDto.Ingredients)
            {
                var ingredient = await this._ingredientService.GetIngredientByNameAsync(item);

                if (ingredient == null)
                {
                    return null;
                }

                var cocktailIngredient = await this._cocktailIngredientService.CreateCocktailIngredientAsync(cocktail.Id, ingredient.Id);
                cocktail.CocktailIngredients.Add(cocktailIngredient);
                await this._context.SaveChangesAsync();
            }

            var cocktailDto = this._cocktailDtoMapper.MapDto(cocktail);

            return cocktailDto;
        }

        public async Task<CocktailDto> UpdateCocktailAsync(int id, string newName, string shortDescription, string longDescription)
        {
            var cocktail = await this._context.Cocktails
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(ii => ii.Id == id);

            if (cocktail == null)
            {
                throw new ArgumentNullException("No entity found");
            }

            try
            {
                cocktail.Name = newName;
                cocktail.ShortDescription = shortDescription;
                cocktail.LongDescription = longDescription;

                this._context.Update(cocktail);
                await this._context.SaveChangesAsync();
                var updatedDto = this._cocktailDtoMapper.MapDto(cocktail);

                return updatedDto;
            }
            catch (Exception)
            {

                throw new ArgumentException();
            }
        }

        public async Task<CocktailDto> DeleteCocktailAsync(int id) 
        {
            var cocktail = this._context.Cocktails
                .FirstOrDefault(i => i.Id == id);

            if (cocktail == null)
            {
                throw new ArgumentNullException("No entity found");
            }

            cocktail.IsDeleted = true;
            cocktail.DeletedOn = this._provider.GetDateTime();
            
            this._context.Update(cocktail);
            await this._context.SaveChangesAsync();

            var coctailDto = this._cocktailDtoMapper.MapDto(cocktail);
            return coctailDto;
        }

        public async Task<ICollection<CocktailDto>> SearchCocktailsAsync(string searchString) 
        {
            var cocktails = await this._context.Cocktails
                .Where(i => i.IsDeleted == false)
                .Include(i => i.CocktailIngredients)
                .ThenInclude(i => i.Ingredient)
                .Select(i => this._cocktailDtoMapper.MapDto(i))
                .ToListAsync();

            var cocktailByIngredients = new List<CocktailDto>();

            foreach (var item in cocktails)
            {
                foreach (var ingredient in item.Ingredients)
                {
                    if (ingredient.ToLower().Contains(searchString.ToLower()))
                    {
                        cocktailByIngredients.Add(item);
                    }
                }
            }

            var cocktailByName = cocktails.Where(i => i.Name.ToLower().Contains(searchString.ToLower()));

            var result = cocktailByIngredients.Union(cocktailByName);




            return result.ToList();
        }
    }
}
