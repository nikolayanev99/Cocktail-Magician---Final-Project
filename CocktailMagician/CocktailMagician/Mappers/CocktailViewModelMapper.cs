using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Mappers
{
    public class CocktailViewModelMapper : IViewModelMapper<CocktailDto, CocktailViewModel>
    {
        public CocktailDto MapDTO(CocktailViewModel entityViewModel)
        {
            if (entityViewModel == null)
            {
                return null;
            }

            return new CocktailDto
            {
                Id = entityViewModel.Id,
                Name = entityViewModel.Name,
                Ingredients = entityViewModel.Ingredients,
                ImageUrl = entityViewModel.ImageUrl,
                ImageThumbnailUrl = entityViewModel.ImageThumbnailUrl,
                ShortDescription = entityViewModel.ShortDescription,
                LongDescription = entityViewModel.LongDescription,
                AverageRating = entityViewModel.AverageRating.Value

            };
        }
        public ICollection<CocktailDto> MapDTO(ICollection<CocktailViewModel> entitiesViewModel)
        {
            return entitiesViewModel.Select(this.MapDTO).ToList();
        }

        public CocktailViewModel MapViewModel(CocktailDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }

            return new CocktailViewModel
            {
                Id = dtoEntity.Id,
                Name = dtoEntity.Name,
                Ingredients = dtoEntity.Ingredients,
                ImageUrl = dtoEntity.ImageUrl,
                ImageThumbnailUrl = dtoEntity.ImageThumbnailUrl,
                ShortDescription = dtoEntity.ShortDescription,
                LongDescription = dtoEntity.LongDescription,
                AverageRating = dtoEntity.AverageRating
            };
        }
        public ICollection<CocktailViewModel> MapViewModel(ICollection<CocktailDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapViewModel).ToList();
        }
    }
}
