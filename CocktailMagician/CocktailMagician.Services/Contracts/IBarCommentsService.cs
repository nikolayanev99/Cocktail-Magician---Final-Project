using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Services.DtoEntities;

namespace CocktailMagician.Services
{
    public interface IBarCommentsService
    {
        Task<BarCommentDto> CreateCommentAsync(BarCommentDto tempBarComment);
        Task<BarCommentDto> DeleteBarCommentAsync(int id);
        Task<ICollection<BarCommentDto>> GetBarCommentsAsync(int barId);
        Task<BarCommentDto> UpdateBarComment(int id, string newText);
    }
}