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
        public BarViewModel MapViewModel(BarDTO dtoEntity)
        {
            throw new NotImplementedException();
        }

        public ICollection<BarViewModel> MapViewModel(ICollection<BarDTO> dtoEntities)
        {
            throw new NotImplementedException();
        }
    }
}
