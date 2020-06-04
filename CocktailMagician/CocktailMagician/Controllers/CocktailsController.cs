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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

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
        private readonly IWebHostEnvironment webHostEnvironment;

        public CocktailsController(ICocktailService cocktailService,
                                   IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper,
                                   ICocktailCommentService cocktailCommentService,
                                   IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper,
                                   IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> cocktailRatingVmMapper,
                                   ICocktailRatingService cocktailRatingService,
                                   IWebHostEnvironment webHostEnvironment)
        {
            _cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            _cocktailVmMapper = cocktailVmMapper ?? throw new ArgumentNullException(nameof(cocktailVmMapper));
            _cocktailCommentService = cocktailCommentService ?? throw new ArgumentNullException(nameof(cocktailCommentService));
            _cocktailCommentVmMapper = cocktailCommentVmMapper ?? throw new ArgumentNullException(nameof(cocktailCommentVmMapper));
            this.cocktailRatingVmMapper = cocktailRatingVmMapper ?? throw new ArgumentNullException(nameof(cocktailRatingVmMapper));
            this.cocktailRatingService = cocktailRatingService ?? throw new ArgumentNullException(nameof(cocktailRatingService));
            this.webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
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

        public async Task<IActionResult> Search(string searchString, int pageNumber = 1, int pageSize = 4)
        {
            if (searchString == null)
            {
                return Redirect("List");
            }

            int excludeRecodrds = (pageSize * pageNumber) - pageSize;

            var result = from b in await this._cocktailService.SearchCocktailsAsync(searchString)
                         select b;

            var cocktailsCount = result.Count();

            result = result
                .Skip(excludeRecodrds)
                .Take(pageSize);

            var models = this._cocktailVmMapper.MapViewModel(result.ToList());

            var newResult = new PagedResult<CocktailViewModel>
            {
                Data = models.ToList(),
                TotalItems = cocktailsCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            return View(newResult);
        }
        public async Task<IActionResult> GetCocktails(string cocktailName)
        {
            var cocktail = await this._cocktailService.GetCocktailByNameAsync(cocktailName);

            return RedirectToAction("Details", new { id = cocktail.Id });
        }

        [Area("cocktail magician")]
        [Authorize(Roles = "cocktail magician")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCocktailViewModel model) 
        {
            if (model.CocktailViewModel.Name == null ||
                model.CocktailViewModel.ShortDescription == null ||
                model.CocktailViewModel.LongDescription == null ||
                model.CocktailViewModel.Ingredients == null)
            {
                return RedirectToAction("List", "Cocktails", new { area = "" });
            }

            if (ModelState.IsValid)
            {

                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "storage\\images\\cocktails");
                    model.CocktailViewModel.ImageUrl = Guid.NewGuid().ToString() + " " + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, model.CocktailViewModel.ImageUrl.ToString());
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var cocktailDto = this._cocktailVmMapper.MapDTO(model.CocktailViewModel);
                await this._cocktailService.CreateCocktailAsync(cocktailDto);
                return RedirectToAction("List", "Cocktails", new { area = "" });
            }

            return View(model);
        }

    }
}
