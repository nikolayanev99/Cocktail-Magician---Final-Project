using CocktailMagician.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "cocktail magician")]
    public class CocktailsController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly ICocktailService _cocktailService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> _cocktailVmMapper;

        public CocktailsController(IIngredientService ingredientService,
                                   ICocktailService cocktailService,
                                   IWebHostEnvironment webHostEnvironment,
                                   IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper)
        {
            this._ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            this._cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this._webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            this._cocktailVmMapper = cocktailVmMapper ?? throw new ArgumentNullException(nameof(cocktailVmMapper));
        }

        public async Task<IActionResult> Create()
        {
            var allIngredients = await _ingredientService.GetAllIngredientsAsync();

            var model = new CreateCocktailViewModel
            {
                AllIngredients = allIngredients
                .Select(i => new SelectListItem(i.Name, i.Name))
                .ToList()
            };

            return View(model);
        }

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
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "storage\\images\\cocktails");
                    model.CocktailViewModel.ImageUrl = Guid.NewGuid().ToString() + " " + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, model.CocktailViewModel.ImageUrl.ToString());
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var cocktailDto = this._cocktailVmMapper.MapDTO(model.CocktailViewModel);
                await this._cocktailService.CreateCocktailAsync(cocktailDto);
                return RedirectToAction("List", "Cocktails", new { area = "" });
            }

            return RedirectToAction("List", "Cocktails", new { area = "" });
        }
    }
}
