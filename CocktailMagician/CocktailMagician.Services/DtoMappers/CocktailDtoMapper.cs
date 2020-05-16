using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailMagician.Services.DtoMappers
{
    public class CocktailDtoMapper : IDtoMapper<Cocktail, CocktailDto>
    {
        public CocktailDto MapDto(Cocktail entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("No entity found");
            }
            return new CocktailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ShortDescription = entity.ShortDescription,
                LongDescription = entity.LongDescription,
                ImageUrl = entity.ImageUrl,
                ImageThumbnailUrl = entity.ImageThumbnailUrl,
                Ingredients = entity.CocktailIngredients
                       .Select(n => n.Ingredient.Name)
                       .ToList(),
                // TODO: average rating for Cocktail
            };

        }

        public ICollection<CocktailDto> MapDto(ICollection<Cocktail> entities)
        {
            return entities.Select(this.MapDto).ToList();
        }
    }
}
