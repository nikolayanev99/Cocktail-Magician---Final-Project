using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;

namespace CocktailMagician.Web.Mappers
{
    public class BarRatingViewModelMapper : IViewModelMapper<BarRatingDto, BarRatingViewModel>
    {
        public BarRatingDto MapDTO(BarRatingViewModel entityViewModel)
        {
            if (entityViewModel == null)
            {
                return null;
            }

            return new BarRatingDto
            {               
                Value = entityViewModel.Value,
                UserId = entityViewModel.UserId,
                BarId = entityViewModel.BarId,
                CreatedOn = entityViewModel.CreatedOn,
                ModifiedOn = entityViewModel.ModifiedOn,
                DeletedOn = entityViewModel.DeletedOn,
                IsDeleted = entityViewModel.IsDeleted,
            };
        }

        public ICollection<BarRatingDto> MapDTO(ICollection<BarRatingViewModel> entitiesViewModel)
        {
            return entitiesViewModel.Select(this.MapDTO).ToList();
        }

        public BarRatingViewModel MapViewModel(BarRatingDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }
            return new BarRatingViewModel
            {
                Value = dtoEntity.Value,
                UserId = dtoEntity.UserId,
                BarId = dtoEntity.BarId,
                CreatedOn = dtoEntity.CreatedOn,
                ModifiedOn = dtoEntity.ModifiedOn,
                DeletedOn = dtoEntity.DeletedOn,
                IsDeleted = dtoEntity.IsDeleted,
            };
        }

        public ICollection<BarRatingViewModel> MapViewModel(ICollection<BarRatingDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapViewModel).ToList();
        }
    }
}
