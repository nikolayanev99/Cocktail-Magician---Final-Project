using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Services.DtoMappers.Contracts;
using CocktailMagician.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class CocktailCommentService : ICocktailCommentService
    {
        private readonly CocktailMagicianContext _context;
        private readonly IDtoMapper<CocktailComment, CocktailCommentDto> _cocktailCommentDtoMapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CocktailCommentService(CocktailMagicianContext context, IDtoMapper<CocktailComment, CocktailCommentDto> cocktailCommentDtoMapper, IDateTimeProvider dateTimeProvider)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._cocktailCommentDtoMapper = cocktailCommentDtoMapper ?? throw new ArgumentNullException(nameof(cocktailCommentDtoMapper));
            this._dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<ICollection<CocktailCommentDto>> GetCocktailCommentsAsync(int cocktailId)
        {
            var comments = await this._context.CocktailComments
                .Include(u => u.User)
                .Include(c => c.Cocktail)
                .Where(d => d.IsDeleted == false)
                .Where(id => id.CocktailId == cocktailId)
                .ToListAsync();

            var cocktailCommentDto = this._cocktailCommentDtoMapper.MapDto(comments);
            return cocktailCommentDto;
        }

        public async Task<CocktailCommentDto> CreateCocktailCommentAsync(CocktailCommentDto cocktailCommentDto)
        {
            if (cocktailCommentDto == null)
            {
                throw new ArgumentNullException("No cocktail comment found.");
            }

            var cocktailComment = new CocktailComment
            {
                Id = cocktailCommentDto.Id,
                commentText = cocktailCommentDto.commentText,
                UserId = cocktailCommentDto.UserId,
                CocktailId = cocktailCommentDto.CocktailId,
                User = await this._context.Users
                .FirstOrDefaultAsync(a => a.UserName == cocktailCommentDto.Username),
                CreatedOn = this._dateTimeProvider.GetDateTime(),
                ModifiedOn = cocktailCommentDto.ModifiedOn,
                DeletedOn = cocktailCommentDto.DeletedOn,
                IsDeleted = cocktailCommentDto.IsDeleted
            };

            await this._context.CocktailComments.AddAsync(cocktailComment);
            await this._context.SaveChangesAsync();

            return this._cocktailCommentDtoMapper.MapDto(cocktailComment);
        }

        public async Task<CocktailCommentDto> UpdateCocktailComment(int id, string newComment) 
        {
            var cocktailComment = this._context.CocktailComments
                .Where(i => i.IsDeleted == false)
                .FirstOrDefault(ii => ii.Id == id);

            if (cocktailComment == null)
            {
                throw new ArgumentNullException("No cocktail comment found.");
            }

            cocktailComment.commentText = newComment;
            cocktailComment.ModifiedOn = this._dateTimeProvider.GetDateTime();

            this._context.Update(cocktailComment);
            await this._context.SaveChangesAsync();

            return this._cocktailCommentDtoMapper.MapDto(cocktailComment);
        }

        public async Task<CocktailCommentDto> DeleteCocktailComment(int id, int cocktailId)
        {
            var comment = await this._context.CocktailComments
                .Where(i => i.IsDeleted == false)
                .Where(ii => ii.Id == id)
                .Where(id => id.CocktailId == cocktailId)
                .FirstOrDefaultAsync();

            if (comment == null)
            {
                throw new ArgumentNullException("No cocktail comment found.");
            }

            comment.IsDeleted = true;
            comment.DeletedOn = this._dateTimeProvider.GetDateTime();
            
            this._context.Update(comment);
            await this._context.SaveChangesAsync();

            return this._cocktailCommentDtoMapper.MapDto(comment);
        }
    }
}
