using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Mappers.Contracts
{
    public interface IViewModelMapper<TDto, TViewModel>
    {
        TViewModel MapViewModel(TDto dtoEntity);
        ICollection<TViewModel> MapViewModel(ICollection<TDto> dtoEntities);
       
    }
}
