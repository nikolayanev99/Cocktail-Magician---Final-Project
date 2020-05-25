using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Mappers
{
    public class CocktailCommentViewModelMapper : IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel>
    {
        public CocktailCommentDto MapDTO(CocktailCommentViewModel entityViewModel)
        {
            if (entityViewModel == null)
            {
                return null;
            }

            return new CocktailCommentDto
            {
                Id = entityViewModel.Id,
                commentText = entityViewModel.Text,
                UserId = entityViewModel.UserId,
                CocktailId = entityViewModel.CocktailId,
                Username = entityViewModel.Author,
                CreatedOn = entityViewModel.CreatedOn,
            };
        }

        public ICollection<CocktailCommentDto> MapDTO(ICollection<CocktailCommentViewModel> entitiesViewModel)
        {
            return entitiesViewModel.Select(this.MapDTO).ToList();
        }

        public CocktailCommentViewModel MapViewModel(CocktailCommentDto dtoEntity)
        {
            if (dtoEntity == null)
            {
                return null;
            }

            return new CocktailCommentViewModel
            {
                Id = dtoEntity.Id,
                Text = dtoEntity.commentText,
                UserId = dtoEntity.UserId,
                CocktailId = dtoEntity.CocktailId,
                Author = dtoEntity.Username,
                CreatedOn = dtoEntity.CreatedOn,
            };
        }

        public ICollection<CocktailCommentViewModel> MapViewModel(ICollection<CocktailCommentDto> dtoEntities)
        {
            return dtoEntities.Select(this.MapViewModel).ToList();
        }
    }
}
