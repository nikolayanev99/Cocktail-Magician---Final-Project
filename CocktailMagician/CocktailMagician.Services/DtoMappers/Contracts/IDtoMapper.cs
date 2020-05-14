using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.DtoMappers.Contracts
{
    public interface IDtoMapper<TEntity, TDto>
    {
        TDto MapDto(TEntity entity);
        ICollection<TDto> MapDto(ICollection<TEntity> entities);
    }
}
