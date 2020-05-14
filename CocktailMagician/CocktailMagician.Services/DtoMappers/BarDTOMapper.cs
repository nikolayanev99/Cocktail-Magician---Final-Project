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
            };
        }

        public ICollection<BarDTO> MapDto(ICollection<Bar> entities)
        {
            return entities.Select(this.MapDto).ToList();
        }
    }
}
