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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CocktailMagician.Web.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly ICocktailService _cocktailService;
        private readonly ICocktailCommentService _cocktailCommentService;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> _cocktailVmMapper;
        private readonly IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> _cocktailCommentVmMapper;


        public CocktailsController(ICocktailService cocktailService,
                                   IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper,
                                   ICocktailCommentService cocktailCommentService,
                                   IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper)
        {
            this._cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this._cocktailVmMapper = cocktailVmMapper ?? throw new ArgumentNullException(nameof(cocktailVmMapper));
            this._cocktailCommentService = cocktailCommentService ?? throw new ArgumentNullException(nameof(cocktailCommentService));
            this._cocktailCommentVmMapper = cocktailCommentVmMapper ?? throw new ArgumentNullException(nameof(cocktailCommentVmMapper));
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var models = await this._cocktailService.GetAllCocktailsAsync();

            var result = this._cocktailVmMapper.MapViewModel(models);

            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cocktail = await this._cocktailService.GetCokctailAsync(id);
            if (cocktail == null)
            {
                return NotFound();
            }

            var result = this._cocktailVmMapper.MapViewModel(cocktail);
            var cocktailComments = await this._cocktailCommentService.GetCocktailCommentAsync(id);
            result.Comments = this._cocktailCommentVmMapper.MapViewModel(cocktailComments);
            result.Ingredients = cocktail.Ingredients;

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
            var dtoComments = await this._cocktailCommentService.GetCocktailCommentAsync(cocktailVm.Id);
            cocktailVm.Comments = this._cocktailCommentVmMapper.MapViewModel(dtoComments);

            return View("Details", cocktailVm);
        }
    }
}
