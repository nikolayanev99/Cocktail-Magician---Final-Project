using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Services.DtoEntities;

namespace CocktailMagician.Services
{
    public interface IBarRatingService
    {
        Task<BarRatingDto> CreateRatingAsync(BarRatingDto tempBarRating);
        Task<ICollection<BarRatingDto>> GetAllBarRatingAsync(int barId);
        Task<BarRatingDto> GetRatingAsync(int barId, int userId);
    }
}