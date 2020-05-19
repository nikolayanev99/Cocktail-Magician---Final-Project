using CocktailMagician.Services.DtoEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailRatingService
    {
        public Task<CocktailRatingDto> GetRatingAsync(int userId, int cocktailId);
        public Task<ICollection<CocktailRatingDto>> GetAllRatingsAsync(int cocktailId);
        public Task<CocktailRatingDto> CreateRatingAsync(CocktailRatingDto cocktailRatingDto);
        public Task<CocktailRatingDto> UpdateRatingAsync(CocktailRatingDto newCocktailRatingDto);
        public Task<CocktailRatingDto> AddEditRatingAsync(CocktailRatingDto newCocktailRatingDto);
    }
}
