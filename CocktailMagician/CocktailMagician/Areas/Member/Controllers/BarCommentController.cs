using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CocktailMagician.Services;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagician.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class BarCommentController : Controller
    {
        private readonly IBarService barService;
        private readonly IBarCommentsService barCommentsService;
        private readonly IBarRatingService barRatingService;
        private readonly IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper;
        private readonly IViewModelMapper<BarDTO, BarViewModel> barVmMapper;

        public BarCommentController(IBarService barService, 
                                    IBarCommentsService barCommentsService, 
                                    IBarRatingService barRatingService, 
                                    IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper, 
                                    IViewModelMapper<BarDTO, BarViewModel> barVmMapper)
        {
            this.barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this.barCommentsService = barCommentsService ?? throw new ArgumentNullException(nameof(barCommentsService));
            this.barRatingService = barRatingService ?? throw new ArgumentNullException(nameof(barRatingService));
            this.barCommentVmMapper = barCommentVmMapper ?? throw new ArgumentNullException(nameof(barCommentVmMapper));
            this.barVmMapper = barVmMapper ?? throw new ArgumentNullException(nameof(barVmMapper));
        }

        public IBarCommentsService BarCommentsService => barCommentsService;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(BarViewModel bar)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var author = HttpContext.User.Identity.Name;

            var barComment = new BarCommentViewModel
            {
                Text = bar.CurrentComment,
                BarId = bar.Id,
                UserId = userId,
                Author = author,
            };

            var barCommentDto = this.barCommentVmMapper.MapDTO(barComment);

            var comment = await this.BarCommentsService.CreateCommentAsync(barCommentDto);

            var currentBar = await this.barService.GetBarAsync(comment.BarId);

            var barVM = this.barVmMapper.MapViewModel(currentBar);
            var DtoComments = await this.BarCommentsService.GetBarCommentsAsync(barVM.Id);
            barVM.Comments = this.barCommentVmMapper.MapViewModel(DtoComments);
            barVM.AverageRating = this.barRatingService.GetAverageBarRating(bar.Id);

            return RedirectToAction("Details", "Bars", new { area = "", id = barVM.Id });
        }

    }
}
