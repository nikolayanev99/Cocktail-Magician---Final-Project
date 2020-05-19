using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailMagician.Services.DtoMappers
{
    public class CocktailCommentDtoMapper : IDtoMapper<CocktailComment, CocktailCommentDto>
    {
        public CocktailCommentDto MapDto(CocktailComment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("No entity found");
            }
            return new CocktailCommentDto
            {
                Id = entity.Id,
                commentText = entity.commentText,
                UserId = entity.UserId,
                Username = entity.User.UserName,
                CocktailId = entity.CocktailId,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                DeletedOn = entity.DeletedOn,
                IsDeleted = entity.IsDeleted
            };
        }

        public ICollection<CocktailCommentDto> MapDto(ICollection<CocktailComment> entities)
        {
            return entities.Select(this.MapDto).ToList();
        }
    }
}
