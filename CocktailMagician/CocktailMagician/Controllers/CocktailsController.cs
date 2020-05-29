using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Mvc;
using cloudscribe.Pagination.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using CocktailMagician.Web.Areas.Member.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CocktailMagician.Web.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly ICocktailService _cocktailService;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> _cocktailVmMapper;
        private readonly ICocktailCommentService _cocktailCommentService;
        private readonly IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> _cocktailCommentVmMapper;
        private readonly IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> cocktailRatingVmMapper;
        private readonly ICocktailRatingService cocktailRatingService;

        public CocktailsController(ICocktailService cocktailService,
                                   IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper,
                                   ICocktailCommentService cocktailCommentService,
                                   IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper,
                                   IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> cocktailRatingVmMapper,
                                   ICocktailRatingService cocktailRatingService)
        {
            _cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            _cocktailVmMapper = cocktailVmMapper ?? throw new ArgumentNullException(nameof(cocktailVmMapper));
            _cocktailCommentService = cocktailCommentService ?? throw new ArgumentNullException(nameof(cocktailCommentService));
            _cocktailCommentVmMapper = cocktailCommentVmMapper ?? throw new ArgumentNullException(nameof(cocktailCommentVmMapper));
            this.cocktailRatingVmMapper = cocktailRatingVmMapper ?? throw new ArgumentNullException(nameof(cocktailRatingVmMapper));
            this.cocktailRatingService = cocktailRatingService ?? throw new ArgumentNullException(nameof(cocktailRatingService));
        }




        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> List()
        //{
        //    var models = await this._cocktailService.GetAllCocktailsAsync();

        //    var result = this._cocktailVmMapper.MapViewModel(models);

        //    return View(result);
        //}

        public async Task<IActionResult> List(int pageNumber = 1, int pageSize = 4)
        {

            var models = await this._cocktailService.GetCocktailsForPeginationAsync(pageSize, pageNumber);
            var countModels = await this._cocktailService.GetAllCocktailsAsync();

            var result = this._cocktailVmMapper.MapViewModel(models);

            var newResult = new PagedResult<CocktailViewModel>
            {
                Data = result.ToList(),
                TotalItems = countModels.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            return View(newResult);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cocktail = await this._cocktailService.GetCokctailAsync(id);
            if (cocktail == null)
            {
                return NotFound();
            }

            var result = this._cocktailVmMapper.MapViewModel(cocktail);
            var cocktailComments = await this._cocktailCommentService.GetCocktailCommentsAsync(id);
            result.Comments = this._cocktailCommentVmMapper.MapViewModel(cocktailComments);
            result.Ingredients = cocktail.Ingredients;
            result.AverageRating = this.cocktailRatingService.GetAverageCocktailRating(id);

            return View(result);
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

            var cocktailCommentDto = this._cocktailCommentVmMapper.MapDTO(cocktailComment);

            var comment = await this._cocktailCommentService.CreateCocktailCommentAsync(cocktailCommentDto);

            var currentCocktail = await this._cocktailService.GetCokctailAsync(comment.CocktailId);

            var cocktailVm = this._cocktailVmMapper.MapViewModel(currentCocktail);
            var dtoComments = await this._cocktailCommentService.GetCocktailCommentsAsync(cocktailVm.Id);
            cocktailVm.Comments = this._cocktailCommentVmMapper.MapViewModel(dtoComments);
            cocktailVm.AverageRating = this.cocktailRatingService.GetAverageCocktailRating(cocktailComment.Id);
            return View("Details", cocktailVm);
        }
        [HttpPost]
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

            var currentCocktail = await this._cocktailService.GetCokctailAsync(rating.CocktailId);

            var cocktailVM = this._cocktailVmMapper.MapViewModel(currentCocktail);

            var DtoComments = await this._cocktailCommentService.GetCocktailCommentsAsync(cocktailVM.Id);
            cocktailVM.Comments = this._cocktailCommentVmMapper.MapViewModel(DtoComments);
            cocktailVM.AverageRating = this.cocktailRatingService.GetAverageCocktailRating(cocktail.Id);

            return View("Details", cocktailVM);
        }
    }
}
