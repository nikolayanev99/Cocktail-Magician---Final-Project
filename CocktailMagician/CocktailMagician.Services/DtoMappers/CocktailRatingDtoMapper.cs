using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailMagician.Services.DtoMappers
{
    public class CocktailRatingDtoMapper : IDtoMapper<CocktailRating, CocktailRatingDto>
    {
        public CocktailRatingDto MapDto(CocktailRating entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("No entity found");
            }
            return new CocktailRatingDto
            {

                Value = entity.Value,
                UserId = entity.UserId,
                Username = entity.User.UserName,
                CocktailId = entity.CocktailId,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                DeletedOn = entity.DeletedOn,
                IsDeleted = entity.IsDeleted
            };
        }

        public ICollection<CocktailRatingDto> MapDto(ICollection<CocktailRating> entities)
        {
            return entities.Select(this.MapDto).ToList();
        }
    }
}
