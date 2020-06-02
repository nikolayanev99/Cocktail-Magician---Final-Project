using System.Threading.Tasks;
using CocktailMagician.Models;

namespace CocktailMagician.Services
{
    public interface IBarCocktailsService
    {
        Task<BarCocktail> CreateBarCocktail(int barId, int cocktailId);
    }
}