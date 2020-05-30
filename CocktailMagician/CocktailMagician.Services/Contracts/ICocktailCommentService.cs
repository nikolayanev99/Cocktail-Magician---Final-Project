using CocktailMagician.Services.DtoEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailCommentService
    {
        public Task<ICollection<CocktailCommentDto>> GetCocktailCommentsAsync(int cocktailId);
        public Task<CocktailCommentDto> CreateCocktailCommentAsync(CocktailCommentDto cocktailCommentDto);
        public Task<CocktailCommentDto> UpdateCocktailComment(int id, string newComment);
        public Task<CocktailCommentDto> DeleteCocktailComment(int id, int cocktailId);
    }
}
