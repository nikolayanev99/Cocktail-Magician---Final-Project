using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;

namespace CocktailMagician.Services.DtoMappers
{
    public class BarCommentDtoMapper:IDtoMapper<BarComment,BarCommentDto>
    {
        public BarCommentDto MapDto(BarComment entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new BarCommentDto
            {
                Id = entity.Id,
                Text=entity.Text,
                UserId=entity.UserId,
                BarId=entity.BarId,
                Author=entity.Author.Email.Split('@')[0],
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                DeletedOn = entity.DeletedOn,
                IsDeleted = entity.IsDeleted,
            };
        }

        public ICollection<BarCommentDto> MapDto(ICollection<BarComment> entities)
        {
            return entities.Select(this.MapDto).ToList();
        }
    }
}
