using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CocktailMagician.Services
{
    public class CocktailRatingService : ICocktailRatingService
    {
        private readonly CocktailMagicianContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IDtoMapper<CocktailRating, CocktailRatingDto> _cocktailRatingDtoMapper;

        public CocktailRatingService(CocktailMagicianContext context, IDateTimeProvider dateTimeProvider, IDtoMapper<CocktailRating, CocktailRatingDto> cocktailRatingDtoMapper)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            this._cocktailRatingDtoMapper = cocktailRatingDtoMapper ?? throw new ArgumentNullException(nameof(cocktailRatingDtoMapper));
        }
        public async Task<CocktailRatingDto> GetRatingAsync(int userId, int cocktailId)
        {
            var rating = await this._context.CocktailRatings
                .Where(r => r.IsDeleted == false)
                .Where(rr => rr.UserId == userId)
                .Where(c => c.CocktailId == cocktailId)
                .FirstOrDefaultAsync();

            if (rating == null)
            {
                return null;
            }

            return this._cocktailRatingDtoMapper.MapDto(rating);
        }
        public double GetAverageCocktailRating(int cocktailId)
        {
            var anyResults = this._context.CocktailRatings
                .Any(r => r.CocktailId == cocktailId);
            if (anyResults == false)
            {
                return 0;
            }
            var result = this._context.CocktailRatings
                .Where(r => r.CocktailId == cocktailId)
                .ToList()
                .Average(r => r.Value);
            if (result == 0)
            {
                return 0;
            }

            return Double.Parse($"{result:f1}");

        }

        public async Task<ICollection<CocktailRatingDto>> GetAllRatingsAsync(int cocktailId)
        {
            var ratings = await this._context.CocktailRatings
                .Include(cc => cc.Cocktail)
                .Include(cc => cc.User)
                .Where(cc => cc.IsDeleted == false)
                .Where(cc => cc.CocktailId == cocktailId)
                .ToListAsync();

            return this._cocktailRatingDtoMapper.MapDto(ratings);
        }

        public async Task<CocktailRatingDto> CreateRatingAsync(CocktailRatingDto cocktailRatingDto)
        {
            if (cocktailRatingDto == null)
            {
                throw new ArgumentNullException("No cocktail rating found.");
            }
            if (cocktailRatingDto.Value == 0 || cocktailRatingDto.UserId < 1 || cocktailRatingDto.CocktailId < 1)
            {
                return null;
            }
            var rating = await this.GetRatingAsync(cocktailRatingDto.UserId, cocktailRatingDto.CocktailId);

            if (rating == null)
            {
                var cocktailRating = new CocktailRating
                {
                    Id = cocktailRatingDto.Id,
                    Value = cocktailRatingDto.Value,
                    UserId = cocktailRatingDto.UserId,
                    CocktailId = cocktailRatingDto.CocktailId,
                    CreatedOn = this._dateTimeProvider.GetDateTime(),
                };

                await this._context.CocktailRatings.AddAsync(cocktailRating);
                await this._context.SaveChangesAsync();

                return this._cocktailRatingDtoMapper.MapDto(cocktailRating);

            }
            var editedRating = await this.UpdateRatingAsync(cocktailRatingDto.CocktailId, cocktailRatingDto.UserId, cocktailRatingDto.Value);

            return editedRating;
        }

        public async Task<CocktailRatingDto> UpdateRatingAsync(int cocktailId, int userId, double newValue)
        {

            var rating = await this._context.CocktailRatings
                .Where(r => r.IsDeleted == false)
                .Where(rr => rr.UserId == userId)
                .FirstOrDefaultAsync(c => c.CocktailId == cocktailId && c.UserId == userId);

            if (rating == null)
            {
                return null;
            }

            rating.Value = newValue;

            this._context.Update(rating);
            await this._context.SaveChangesAsync();


            return this._cocktailRatingDtoMapper.MapDto(rating);
        }

    }
}
