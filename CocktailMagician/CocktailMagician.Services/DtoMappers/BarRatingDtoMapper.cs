using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;

namespace CocktailMagician.Services.DtoMappers
{
    public class BarRatingDtoMapper : IDtoMapper<BarRating, BarRatingDto>
    {
        public BarRatingDto MapDto(BarRating entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new BarRatingDto
            {
                Id = entity.Id,
                Value = entity.Value,
                UserId = entity.UserId,
                BarId = entity.BarId,               
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                DeletedOn = entity.DeletedOn,
                IsDeleted = entity.IsDeleted,
            };
        }

        public ICollection<BarRatingDto> MapDto(ICollection<BarRating> entities)
        {
            return entities.Select(this.MapDto).ToList();
        }
    }
}
