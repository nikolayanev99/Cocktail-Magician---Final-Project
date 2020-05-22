using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;

namespace CocktailMagician.Web.Mappers
{
    public class BarCommentViewModelMapper : IViewModelMapper<BarCommentDto, BarCommentViewModel>
    {
        public BarCommentDto MapDTO(BarCommentViewModel entityViewModel)
        {
            if (entityViewModel == null)
            {
                return null;
            }

            return new BarCommentDto
            {
                Id = entityViewModel.Id,
                Text = entityViewModel.Text,
                UserId = entityViewModel.UserId,
                BarId = entityViewModel.BarId,
                Author = entityViewModel.Author,               
                CreatedOn = entityViewModel.CreatedOn,

            };
        }

        public ICollection<BarCommentDto> MapDTO(ICollection<BarCommentViewModel> entitiesViewModel)
        {
            return entitiesViewModel.Select(this.MapDTO).ToList();
        }

        public BarCommentViewModel MapViewModel(BarCommentDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }

            return new BarCommentViewModel
            {
                Id = dtoEntity.Id,
                Text = dtoEntity.Text,
                UserId = dtoEntity.UserId,
                BarId = dtoEntity.BarId,
                Author = dtoEntity.Author,
                CreatedOn = dtoEntity.CreatedOn,
            };
            
        }

        public ICollection<BarCommentViewModel> MapViewModel(ICollection<BarCommentDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapViewModel).ToList();
        }
    }
}
