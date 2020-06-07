using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Services.DtoEntities;

namespace CocktailMagician.Services
{
    public interface IBarCommentsService
    {
        Task<BarCommentDto> CreateCommentAsync(BarCommentDto tempBarComment);
        Task<ICollection<BarCommentDto>> GetBarCommentsAsync(int barId);
    }
}