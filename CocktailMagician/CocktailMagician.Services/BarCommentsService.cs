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
    public class BarCommentsService : IBarCommentsService
    {
        private readonly CocktailMagicianContext context;
        private readonly IDtoMapper<BarComment, BarCommentDto> dtoMapper;
        private readonly IDateTimeProvider dateTimeProvider;

        public BarCommentsService(CocktailMagicianContext context, IDtoMapper<BarComment, BarCommentDto> dtoMapper, IDateTimeProvider dateTimeProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<ICollection<BarCommentDto>> GetBarCommentsAsync(int barId)
        {
            var barComment = await this.context.BarComments
                .Include(b => b.Bar)
                .Include(b => b.Author)
                .Where(b => b.IsDeleted == false)
                .Where(b => b.BarId == barId)
                .ToListAsync();

            if (barComment == null)
            {
                return null;
            }
            var barCommentDtos = this.dtoMapper.MapDto(barComment);

            return barCommentDtos;
        }
        public async Task<BarCommentDto> CreateCommentAsync(BarCommentDto tempBarComment)
        {
            if (tempBarComment == null)
            {
                return null;
            }
            if (tempBarComment.Text == null || tempBarComment.UserId < 1 || tempBarComment.BarId < 1)
            {
                return null;
            }
         

            var newBarComment = new BarComment
            {
                Id = tempBarComment.Id,
                Text = tempBarComment.Text,
                BarId = tempBarComment.BarId,
                UserId = tempBarComment.UserId,
                Author = this.context.Users
                .FirstOrDefault(a => a.UserName == tempBarComment.Author),
                CreatedOn = this.dateTimeProvider.GetDateTime(),
            };

            await this.context.AddAsync(newBarComment);
            await this.context.SaveChangesAsync();

            var barCommentDto = this.dtoMapper.MapDto(newBarComment);

            return barCommentDto;

        }
    }
}
