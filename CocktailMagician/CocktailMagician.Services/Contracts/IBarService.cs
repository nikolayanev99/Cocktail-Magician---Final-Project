using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Services.DtoEntities;

namespace CocktailMagician.Services
{
    public interface IBarService
    {
        Task<BarDTO> CreateBarAsync(BarDTO tempBarDTO);
        Task<bool> DeleteBarAsync(int id);
        Task<BarDTO> EditBarAsync(BarDTO barDTO);
        Task<ICollection<BarDTO>> GetAllBarsAsync();
        Task<BarDTO> GetBarAsync(int id);
    }
}