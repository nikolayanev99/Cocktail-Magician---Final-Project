using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Mappers
{
    public class IngredientViewModelMapper : IViewModelMapper<IngredientDto, IngredientViewModel>
    {
        public IngredientDto MapDTO(IngredientViewModel entityViewModel)
        {
            if (entityViewModel == null)
            {
                return null;
            }

            return new IngredientDto
            {
                Id = entityViewModel.Id,
                Name = entityViewModel.Name
            };
        }

        public ICollection<IngredientDto> MapDTO(ICollection<IngredientViewModel> entitiesViewModel)
        {
            return entitiesViewModel.Select(this.MapDTO).ToList();
        }

        public IngredientViewModel MapViewModel(IngredientDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }
            return new IngredientViewModel
            {
                Id = dtoEntity.Id,
                Name = dtoEntity.Name
            };
        }

        public ICollection<IngredientViewModel> MapViewModel(ICollection<IngredientDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapViewModel).ToList();
        }
    }
}
