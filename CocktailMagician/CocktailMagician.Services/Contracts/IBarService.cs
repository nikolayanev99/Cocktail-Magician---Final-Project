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
        Task<ICollection<BarDTO>> GetBarsForPeginationAsync(int pageSize = 1, int pageNumber = 1);
        Task<ICollection<BarDTO>> SearchBarsAsync(string searchString);
        Task<ICollection<BarDTO>> GetThreeBarsAsync(int num);
    }
}