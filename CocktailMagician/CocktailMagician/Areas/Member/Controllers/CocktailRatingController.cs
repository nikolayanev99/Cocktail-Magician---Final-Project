using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Areas.Member.Models;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagician.Web.Areas.Member.Controllers
{
    [Area("Member")]
    //[Authorize(Roles="bar crawler")]
    public class CocktailRatingController : Controller
    {
        private readonly IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> cocktailRatingVmMapper;
        private readonly ICocktailRatingService cocktailRatingService;
        private readonly ICocktailService cocktailService;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper;
        private readonly ICocktailCommentService cocktailCommentService;
        private readonly IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper;

        public CocktailRatingController(IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> cocktailRatingVmMapper,
                                        ICocktailRatingService cocktailRatingService,
                                        ICocktailService cocktailService,
                                        IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper,
                                        ICocktailCommentService cocktailCommentService,
                                        IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper)
        {
            this.cocktailRatingVmMapper = cocktailRatingVmMapper ?? throw new ArgumentNullException(nameof(cocktailRatingVmMapper));
            this.cocktailRatingService = cocktailRatingService ?? throw new ArgumentNullException(nameof(cocktailRatingService));
            this.cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this.cocktailVmMapper = cocktailVmMapper ?? throw new ArgumentNullException(nameof(cocktailVmMapper));
            this.cocktailCommentService = cocktailCommentService ?? throw new ArgumentNullException(nameof(cocktailCommentService));
            this.cocktailCommentVmMapper = cocktailCommentVmMapper ?? throw new ArgumentNullException(nameof(cocktailCommentVmMapper));
        }

        [HttpPost, ActionName("AddRating")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRating(CocktailViewModel cocktail)
        {

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cocktailRating = new CocktailRatingViewModel
            {
                Value = (cocktail.SelectedRating),
                CocktailId = cocktail.Id,
                UserId = userId,
            };

            var cocktailRatingDto = this.cocktailRatingVmMapper.MapDTO(cocktailRating);

            var rating = await this.cocktailRatingService.CreateRatingAsync(cocktailRatingDto);

            var currentCocktail = await this.cocktailService.GetCokctailAsync(rating.CocktailId);

            var cocktailVM = this.cocktailVmMapper.MapViewModel(currentCocktail);

            var DtoComments = await this.cocktailCommentService.GetCocktailCommentsAsync(cocktailVM.Id);
            cocktailVM.Comments = this.cocktailCommentVmMapper.MapViewModel(DtoComments);
            cocktailVM.AverageRating = this.cocktailRatingService.GetAverageCocktailRating(cocktail.Id);

            return RedirectToAction("Details", "Cocktails", cocktailVM);


        }
    }
}
    