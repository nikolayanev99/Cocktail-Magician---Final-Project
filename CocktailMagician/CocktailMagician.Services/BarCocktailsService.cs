using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Models;

namespace CocktailMagician.Services
{
    public class BarCocktailsService : IBarCocktailsService
    {
        private readonly CocktailMagicianContext contex;

        public BarCocktailsService(CocktailMagicianContext contex)
        {
            this.contex = contex ?? throw new ArgumentNullException(nameof(contex));
        }

        public async Task<BarCocktail> CreateBarCocktail(int barId, int cocktailId)
        {
            if (barId < 1 || cocktailId < 1)
            {
                return null;
            }
            var barCocktail = new BarCocktail
            {
                BarId = barId,
                CocktailId = cocktailId,
            };
            await this.contex.BarCocktails.AddAsync(barCocktail);
            await this.contex.SaveChangesAsync();

            return barCocktail;
        }
    }
}
