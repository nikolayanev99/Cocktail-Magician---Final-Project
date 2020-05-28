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
        public double GetAverageBarRating(int barId)
        {
            var anyResults = this.context.BarRatings
                .Any(r => r.BarId == barId);
            if (anyResults == false)
            {
                return 0;
            }
            var result = this.context.BarRatings
                .Where(r => r.BarId == barId)
                .ToList()
                .Average(r => r.Value);
            if (result == 0)
            {
                return 0;
            }

            return Double.Parse($"{result:f1}");

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

        public async Task<BarRatingDto> EditRatingAsync(int barId,int userId, double newValue)
        {
            var rating = await this.context.BarRatings
                .Include(r => r.User)
                .Include(r => r.Bar)
                .Where(r => r.IsDeleted == false)
                .FirstOrDefaultAsync(r => r.UserId == userId && r.BarId == barId);

            if (rating == null)
            {
                return null;
            }

            rating.Value = newValue;

            this.context.Update(rating);
            await this.context.SaveChangesAsync();

            var ratingDto = this.dtoMapper.MapDto(rating);
            return ratingDto;
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
            var rating = await this.GetRatingAsync(tempBarRating.BarId, tempBarRating.UserId);
            if (rating == null)
            {
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
            var editdRating = await this.EditRatingAsync(tempBarRating.BarId, tempBarRating.UserId, tempBarRating.Value);
            return editdRating;

        }

    }
}
