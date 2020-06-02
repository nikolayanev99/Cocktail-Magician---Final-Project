using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;

namespace CocktailMagician.Services.DtoMappers
{
    public class BarDTOMapper : IDtoMapper<Bar, BarDTO>
    {
        public BarDTO MapDto(Bar entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new BarDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Info = entity.Info,
                Address = entity.Address,
                PhotoPath = entity.PhotoPath,
                AverageRating = entity.Ratings.Any() ? entity.Ratings.Average(r => r.Value) : 0.00,
                Cocktails = entity.BarCocktails
                            .Select(bc => bc.Cocktail.Name).ToList(),
                CreatedOn=entity.CreatedOn,
                ModifiedOn=entity.ModifiedOn,
                DeletedOn = entity.DeletedOn,
                IsDeleted = entity.IsDeleted,
            };
        }

        public ICollection<BarDTO> MapDto(ICollection<Bar> entities)
        {
            return entities.Select(this.MapDto).ToList();
        }
    }
}
