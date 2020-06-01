using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagician.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class CocktailCommentController : Controller
    {
        private readonly ICocktailService cocktailService;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper;
        private readonly ICocktailCommentService cocktailCommentService;
        private readonly IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper;
        private readonly ICocktailRatingService cocktailRatingService;

        public CocktailCommentController(ICocktailService cocktailService, 
                                         IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper, 
                                         ICocktailCommentService cocktailCommentService, 
                                         IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper, 
                                         ICocktailRatingService cocktailRatingService)
        {
            this.cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this.cocktailVmMapper = cocktailVmMapper ?? throw new ArgumentNullException(nameof(cocktailVmMapper));
            this.cocktailCommentService = cocktailCommentService ?? throw new ArgumentNullException(nameof(cocktailCommentService));
            this.cocktailCommentVmMapper = cocktailCommentVmMapper ?? throw new ArgumentNullException(nameof(cocktailCommentVmMapper));
            this.cocktailRatingService = cocktailRatingService ?? throw new ArgumentNullException(nameof(cocktailRatingService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(CocktailViewModel cocktail)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var author = HttpContext.User.Identity.Name;

            var cocktailComment = new CocktailCommentViewModel
            {
                Text = cocktail.CurrentComment,
                CocktailId = cocktail.Id,
                UserId = userId,
                Author = author
            };

            var cocktailCommentDto = this.cocktailCommentVmMapper.MapDTO(cocktailComment);

            var comment = await this.cocktailCommentService.CreateCocktailCommentAsync(cocktailCommentDto);

            var currentCocktail = await this.cocktailService.GetCokctailAsync(comment.CocktailId);

            var cocktailVm = this.cocktailVmMapper.MapViewModel(currentCocktail);
            var dtoComments = await this.cocktailCommentService.GetCocktailCommentsAsync(cocktailVm.Id);
            cocktailVm.Comments = this.cocktailCommentVmMapper.MapViewModel(dtoComments);
            cocktailVm.AverageRating = this.cocktailRatingService.GetAverageCocktailRating(cocktailComment.Id);

            return RedirectToAction("Details", "Cocktails", new { area = "", id = cocktailVm.Id });
        }
    }

}
