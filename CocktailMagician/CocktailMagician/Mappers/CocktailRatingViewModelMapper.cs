using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Areas.Member.Models;

namespace CocktailMagician.Web.Mappers.Contracts
{
    public class CocktailRatingViewModelMapper : IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel>
    {
        public CocktailRatingDto MapDTO(CocktailRatingViewModel entityViewModel)
        {
            if (entityViewModel == null)
            {
                return null;
            }

            return new CocktailRatingDto
            {
                Value = entityViewModel.Value,
                UserId = entityViewModel.UserId,
                CocktailId = entityViewModel.CocktailId,
                CreatedOn = entityViewModel.CreatedOn,
                ModifiedOn = entityViewModel.ModifiedOn,
                DeletedOn = entityViewModel.DeletedOn,
                IsDeleted = entityViewModel.IsDeleted,
            };
        }

        public ICollection<CocktailRatingDto> MapDTO(ICollection<CocktailRatingViewModel> entitiesViewModel)
        {
            return entitiesViewModel.Select(this.MapDTO).ToList();
        }

        public CocktailRatingViewModel MapViewModel(CocktailRatingDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }
            return new CocktailRatingViewModel
            {
                Value = dtoEntity.Value,
                UserId = dtoEntity.UserId,
                CocktailId = dtoEntity.CocktailId,
                CreatedOn = dtoEntity.CreatedOn,
                ModifiedOn = dtoEntity.ModifiedOn,
                DeletedOn = dtoEntity.DeletedOn,
                IsDeleted = dtoEntity.IsDeleted,
            };
        }

        public ICollection<CocktailRatingViewModel> MapViewModel(ICollection<CocktailRatingDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapViewModel).ToList();
        }
    }
}
