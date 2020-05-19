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

        public async Task<ICollection<CocktailRatingDto>> GetAllRatingsAsync(int cocktailId)
        {
            var ratings = await this._context.CocktailRatings
                .Where(cc => cc.IsDeleted == false)
                .Where(cc => cc.CocktailId == cocktailId)
                .ToListAsync();

            if (ratings.Count == 0)
            {
                return null;
            }

            return this._cocktailRatingDtoMapper.MapDto(ratings);
        }

        public async Task<CocktailRatingDto> CreateRatingAsync(CocktailRatingDto cocktailRatingDto) 
        {
            if (cocktailRatingDto == null)
            {
                throw new ArgumentNullException("No cocktail rating found.");
            }

            var cocktailRating = new CocktailRating
            {
                Value = cocktailRatingDto.Value,
                UserId = cocktailRatingDto.UserId,
                CocktailId = cocktailRatingDto.CocktailId,
                CreatedOn = this._dateTimeProvider.GetDateTime(),
                ModifiedOn = cocktailRatingDto.ModifiedOn,
                DeletedOn = cocktailRatingDto.DeletedOn,
                IsDeleted = cocktailRatingDto.IsDeleted
            };

            await this._context.CocktailRatings.AddAsync(cocktailRating);
            await this._context.SaveChangesAsync();

            return this._cocktailRatingDtoMapper.MapDto(cocktailRating);
        }

        public async Task<CocktailRatingDto> UpdateRatingAsync(CocktailRatingDto newCocktailRatingDto)
        {
            if (newCocktailRatingDto == null)
            {
                throw new ArgumentNullException("No cocktail rating found.");
            }

            var cocktailRatingDto = await this.GetRatingAsync(newCocktailRatingDto.UserId, newCocktailRatingDto.CocktailId);
            
            if (cocktailRatingDto == null)
            {
                return null;
            }

            cocktailRatingDto.Value = cocktailRatingDto.Value;
            
            this._context.Update(cocktailRatingDto);
            await this._context.SaveChangesAsync();


            return cocktailRatingDto;
        }

        public async Task<CocktailRatingDto> AddEditRatingAsync(CocktailRatingDto newCocktailRatingDto)
        {
            if (newCocktailRatingDto == null)
            {
                throw new ArgumentNullException("No cocktail rating found.");
            }

            var cocktailRatingDto = await this.GetRatingAsync(newCocktailRatingDto.UserId, newCocktailRatingDto.CocktailId);

            if (cocktailRatingDto == null)
            {
                cocktailRatingDto = await this.CreateRatingAsync(newCocktailRatingDto);
                
                return cocktailRatingDto;
            }

            cocktailRatingDto.Value = newCocktailRatingDto.Value;
            cocktailRatingDto = await this.UpdateRatingAsync(newCocktailRatingDto);
            
            
            return cocktailRatingDto;
        }
    }
}
