using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Services
{
    public class BarRatingService : IBarRatingService
    {
        private readonly CocktailMagicianContext context;
        private readonly IDtoMapper<BarRating, BarRatingDto> dtoMapper;
        private readonly IDateTimeProvider dateTimeProvider;

        public BarRatingService(CocktailMagicianContext context, IDtoMapper<BarRating, BarRatingDto> dtoMapper, IDateTimeProvider dateTimeProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<BarRatingDto> GetRatingAsync(int barId, int userId)
        {
            var barRating = await this.context.BarRatings
                .Include(r => r.User)
                .Include(r => r.Bar)
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(r => r.UserId == userId && r.BarId == barId);

            if (barRating == null)
            {
                return null;
            }

            var barRatingDto = this.dtoMapper.MapDto(barRating);
            return barRatingDto;

        }

        public async Task<ICollection<BarRatingDto>> GetAllBarRatingAsync(int barId)
        {
            var barRating = await this.context.BarRatings
                .Include(r => r.User)
                .Include(r => r.Bar)
                .Where(r => r.IsDeleted == false)
                .Where(r => r.BarId == barId)
                .ToListAsync();

            var barRatingDtos = this.dtoMapper.MapDto(barRating);

            return barRatingDtos;

        }

        public async Task<BarRatingDto> CreateRatingAsync(BarRatingDto tempBarRating)
        {
            if (tempBarRating == null)
            {
                return null;
            }
            if (tempBarRating.Value == 0 || tempBarRating.UserId < 1 || tempBarRating.BarId < 1)
            {
                return null;
            }


            var newBarRating = new BarRating
            {
                Id = tempBarRating.Id,
                Value = tempBarRating.Value,
                BarId = tempBarRating.BarId,
                UserId = tempBarRating.UserId,
                CreatedOn = this.dateTimeProvider.GetDateTime(),
            };

            await this.context.AddAsync(newBarRating);
            await this.context.SaveChangesAsync();

            var barRatingDto = this.dtoMapper.MapDto(newBarRating);

            return barRatingDto;
        }

    }
}
