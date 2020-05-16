using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailMagician.Services.DtoMappers
{
    public class IngredientDtoMapper : IDtoMapper<Ingredient, IngredientDto>
    {
        public IngredientDto MapDto(Ingredient entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("No entity found");
            }

            return new IngredientDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public ICollection<IngredientDto> MapDto(ICollection<Ingredient> entities)
        {
            return entities.Select(this.MapDto).ToList();
        }
    }
}
