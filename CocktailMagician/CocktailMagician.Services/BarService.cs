using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Services
{
    public class BarService : IBarService
    {
        private readonly CocktailMagicianContext context;
        private readonly IDtoMapper<Bar, BarDTO> barDTOMapper;
        private readonly IDateTimeProvider dateTimeProvider;

        public BarService(CocktailMagicianContext context, IDtoMapper<Bar, BarDTO> barDTOMapper, IDateTimeProvider dateTimeProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.barDTOMapper = barDTOMapper ?? throw new ArgumentNullException(nameof(barDTOMapper));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<BarDTO> GetBarAsync(int id)
        {
            var bar = await this.context.Bars
                .Include(b => b.Comments)
                .Include(b=>b.BarCocktails)
                .ThenInclude(bc=>bc.Cocktail)
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bar == null)
            {
                return null;
            }
            var barDTO = this.barDTOMapper.MapDto(bar);

            return barDTO;
        }

        public async Task<ICollection<BarDTO>> GetAllBarsAsync()
        {
            var bars = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .ToListAsync();

            if (!bars.Any())
            {
                return null;
            }

            var barDtos = this.barDTOMapper.MapDto(bars);

            return barDtos;
        }

        public async Task<BarDTO> CreateBarAsync(BarDTO tempBarDTO)
        {
            if (tempBarDTO == null)
            {
                return null;
            }

            var bar = new Bar
            {
                Name = tempBarDTO.Name,
                Info = tempBarDTO.Info,
                Address = tempBarDTO.Address,
                PhotoPath = tempBarDTO.PhotoPath,
                CreatedOn = this.dateTimeProvider.GetDateTime(),
            };

            await this.context.Bars.AddAsync(bar);
            await this.context.SaveChangesAsync();

            var barDTO = this.barDTOMapper.MapDto(bar);

            return barDTO;

        }

        public async Task<BarDTO> EditBarAsync(BarDTO barDTO)
        {
            var bar = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Id == barDTO.Id);

            if (bar == null)
            {
                return null;
            }

            bar.Name = barDTO.Name == null ? bar.Name : barDTO.Name;
            bar.Info = barDTO.Info == null ? bar.Info : barDTO.Info;
            bar.Address = barDTO.Address == null ? bar.Address : barDTO.Address;
            bar.ModifiedOn = this.dateTimeProvider.GetDateTime();
            this.context.Update(bar);
            await this.context.SaveChangesAsync();
            var newBarDto = this.barDTOMapper.MapDto(bar);

            return newBarDto;
        }

        public async Task<bool> DeleteBarAsync(int id)
        {
            var bar = await this.context.Bars
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bar == null)
            {
                return false;
            }

            bar.IsDeleted = true;
            bar.DeletedOn = this.dateTimeProvider.GetDateTime();

            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<BarDTO>> GetBarsForPeginationAsync(int pageSize = 1, int pageNumber = 1)
        {
            int excludeRecodrds = (pageSize * pageNumber) - pageSize;

            var bars = await this.context.Bars
                .Where(v => v.IsDeleted == false)
                .OrderBy(n => n.Name)
                .Skip(excludeRecodrds)
                .Take(pageSize)
                .ToListAsync();

            var barDto = this.barDTOMapper.MapDto(bars);

            return barDto;
        }

        public async Task<ICollection<BarDTO>> SearchBarsAsync(string searchString) 
        {
            int ratingNumber;

            if (int.TryParse(searchString, out ratingNumber))
            {
                var allBars = await this.context.Bars
               .Where(i => i.IsDeleted == false)
               .Include(i => i.Ratings)
               .OrderBy(i=> i.Name)
               .Select(i => this.barDTOMapper.MapDto(i))
               .ToListAsync();

                var barsByRating = allBars.Where(r => Math.Floor(r.AverageRating) == ratingNumber);

                return barsByRating.ToList();
            }
            else
            {
                var bars = await this.context.Bars
                .Where(i => i.IsDeleted == false)
                .Include(r => r.Ratings)
                .OrderBy(i => i.Name)
                .Select(i => this.barDTOMapper.MapDto(i))
                .ToListAsync();


                var barByName = bars.Where(i => i.Name.ToLower().Contains(searchString.ToLower()));
                var barByAdress = bars.Where(i => i.Address.ToLower().Contains(searchString.ToLower()));

                var result = barByName.Union(barByAdress);

                return result.ToList();
            }
        }
    }
}
