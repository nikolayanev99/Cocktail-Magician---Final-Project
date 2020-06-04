using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CocktailMagician.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "cocktail magician")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly IViewModelMapper<IngredientDto, IngredientViewModel> _ingredientVmMapper;

        public IngredientsController(IIngredientService ingredientService,
                                     IViewModelMapper<IngredientDto, IngredientViewModel> ingredientVmMapper)
        {
            this._ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            this._ingredientVmMapper = ingredientVmMapper ?? throw new ArgumentNullException(nameof(ingredientVmMapper));
        }



        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(int pageNumber = 1, int pageSize = 8)
        {

            var models = await this._ingredientService.GetIngredientsForPeginationAsync(pageSize, pageNumber);
            var countModels = await this._ingredientService.GetAllIngredientsAsync();

            var result = this._ingredientVmMapper.MapViewModel(models);

            var newResult = new PagedResult<IngredientViewModel>
            {
                Data = result.ToList(),
                TotalItems = countModels.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            return View(newResult);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await this._ingredientService.GetIngredientAsyng(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            var model = this._ingredientVmMapper.MapViewModel(ingredient);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IngredientViewModel ingridient)
        {
            if (id != ingridient.Id)
            {
                return NotFound();
            }
            if (ingridient.Name == null)
            {
                return NotFound();
            }
            var model = this._ingredientVmMapper.MapDTO(ingridient);

            await this._ingredientService.EditIngredientAsync(id, model.Name);



            return RedirectToAction("List", "Ingredients", new { area = "admin" });
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IngredientViewModel model) 
        {
            if (model.Name == null)
            {
                return NotFound();
            }

            var modelDto = this._ingredientVmMapper.MapDTO(model);

            await this._ingredientService.CreateIngredientAsync(modelDto);

            return RedirectToAction("List", "Ingredients", new { area = "admin" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await this._ingredientService.GetIngredientAsyng(id);

            if (ingredient == null)
            {
                return null;
            }

            var model = this._ingredientVmMapper.MapViewModel(ingredient);

            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            await this._ingredientService.DeleteIngredientAsync(id);

            return RedirectToAction("List", "Ingredients", new { area = "admin" });
        }
    }
}
