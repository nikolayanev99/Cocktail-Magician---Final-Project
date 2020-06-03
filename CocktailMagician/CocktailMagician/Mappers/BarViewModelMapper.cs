using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;

namespace CocktailMagician.Web.Mappers
{
    public class BarViewModelMapper : IViewModelMapper<BarDTO, BarViewModel>
    {
        public BarDTO MapDTO(BarViewModel entityViewModel)
        {
            if (entityViewModel == null)
            {
                return null;
            }

            return new BarDTO
            {
                Id = entityViewModel.Id,
                Name = entityViewModel.Name,
                Info = entityViewModel.Info,
                Address = entityViewModel.Address,
                PhotoPath = entityViewModel.PhotoPath,
                AverageRating = entityViewModel.AverageRating,
                
            };
        }

        public ICollection<BarDTO> MapDTO(ICollection<BarViewModel> entitiesViewModel)
        {
            return entitiesViewModel.Select(this.MapDTO).ToList();
        }

        public BarViewModel MapViewModel(BarDTO dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }

            return new BarViewModel
            {
                Id = dtoEntity.Id,
                Name = dtoEntity.Name,
                Info = dtoEntity.Info,
                Address = dtoEntity.Address,
                PhotoPath = dtoEntity.PhotoPath,
                AverageRating = dtoEntity.AverageRating,
            };
        }

        public ICollection<BarViewModel> MapViewModel(ICollection<BarDTO> dtoEntities)
        {
            return dtoEntities.Select(this.MapViewModel).ToList();
        }
    }
}
