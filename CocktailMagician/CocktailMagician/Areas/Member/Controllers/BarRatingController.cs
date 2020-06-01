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
    public class BarRatingController : Controller
    {
        private readonly IBarService barService;
        private readonly IBarCommentsService barCommentsService;
        private readonly IBarRatingService barRatingService;
        private readonly IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper;
        private readonly IViewModelMapper<BarDTO, BarViewModel> barVmMapper;
        private readonly IViewModelMapper<BarRatingDto, BarRatingViewModel> barRatingVmMapper;

        public BarRatingController(IBarService barService, 
                                   IBarCommentsService barCommentsService, 
                                   IBarRatingService barRatingService, 
                                   IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper, 
                                   IViewModelMapper<BarDTO, BarViewModel> barVmMapper, 
                                   IViewModelMapper<BarRatingDto, BarRatingViewModel> barRatingVmMapper)
        {
            this.barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this.barCommentsService = barCommentsService ?? throw new ArgumentNullException(nameof(barCommentsService));
            this.barRatingService = barRatingService ?? throw new ArgumentNullException(nameof(barRatingService));
            this.barCommentVmMapper = barCommentVmMapper ?? throw new ArgumentNullException(nameof(barCommentVmMapper));
            this.barVmMapper = barVmMapper ?? throw new ArgumentNullException(nameof(barVmMapper));
            this.barRatingVmMapper = barRatingVmMapper ?? throw new ArgumentNullException(nameof(barRatingVmMapper));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> AddRating(BarViewModel bar)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var barRating = new BarRatingViewModel
            {
                Value = (bar.SelectedRating),
                BarId = bar.Id,
                UserId = userId,
            };

            var barRatingDto = this.barRatingVmMapper.MapDTO(barRating);

            var rating = await this.barRatingService.CreateRatingAsync(barRatingDto);

            var currentBar = await this.barService.GetBarAsync(rating.BarId);

            var barVM = this.barVmMapper.MapViewModel(currentBar);

            var DtoComments = await this.barCommentsService.GetBarCommentsAsync(barVM.Id);
            barVM.Comments = this.barCommentVmMapper.MapViewModel(DtoComments);
            barVM.AverageRating = this.barRatingService.GetAverageBarRating(bar.Id);

            return RedirectToAction("Details", "Bars", new { area = "", id = barVM.Id });

        }
    }
}
