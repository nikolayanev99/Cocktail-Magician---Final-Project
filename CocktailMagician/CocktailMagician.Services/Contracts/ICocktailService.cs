using CocktailMagician.Services.DtoEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailService
    {
        public Task<CocktailDto> GetCokctailAsync(int id);
        public Task<ICollection<CocktailDto>> GetAllCocktailsAsync();
        public Task<CocktailDto> CreateCocktailAsync(CocktailDto tempCocktailDto);
        public Task<CocktailDto> UpdateCocktailAsync(int id, string newName, string shortDescription, string longDescription);
        public Task<CocktailDto> DeleteCocktailAsync(int id);
        public Task<ICollection<CocktailDto>> GetCocktailsForPeginationAsync(int pageSize = 1, int pageNumber = 1);
    }
}
