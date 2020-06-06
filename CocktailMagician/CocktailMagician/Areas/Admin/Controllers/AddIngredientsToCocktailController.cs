using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Services;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "cocktail magician")]

    public class AddIngredientToCocktailController : Controller
    {
        private readonly CocktailMagicianContext context;
        private readonly ICocktailService cocktailService;
        private readonly IIngredientService _ingredientService;
        private readonly IViewModelMapper<IngredientDto, IngredientViewModel> _ingredientVmMapper;
        private readonly ICocktailIngredientService _cocktailIngredientService;

        public AddIngredientToCocktailController(CocktailMagicianContext context,
                                          ICocktailService cocktailService,
                                          IIngredientService ingredientService,
                                          IViewModelMapper<IngredientDto, IngredientViewModel> ingredientVmMapper,
                                          ICocktailIngredientService cocktailIngredientService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this._ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
            this._ingredientVmMapper = ingredientVmMapper ?? throw new ArgumentNullException(nameof(ingredientVmMapper));
            this._cocktailIngredientService = cocktailIngredientService ?? throw new ArgumentNullException(nameof(cocktailIngredientService));
        }

        public async Task<IActionResult> AvailableIngredients(int Id)
        {
            var cocktail = await this.cocktailService.GetCokctailAsync(Id);
            var allIngredients = await this._ingredientService.GetAllIngredientsAsync();

            var allIngredientsVm = allIngredients
                .Select(x => this._ingredientVmMapper.MapViewModel(x))
                .Select(x => new CheckBoxItem()
                {
                    Id = x.Id,
                    Title = x.Name,
                    isChecked = context.CocktailIngredients.Any(bc => bc.CocktailId == Id && bc.IngredientId == x.Id) ? true : false,
                }).ToList();

            var availableIngredients = await this._ingredientService.GetCocktailIngredientsAsync(Id);
            var availableIngredientsVm = this._ingredientVmMapper.MapViewModel(availableIngredients);

            var cocktailVm = new CocktailViewModel
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                SelectedIngredients = allIngredientsVm,
                IngredientsChange = availableIngredientsVm,
            };

            return View(cocktailVm);

        }
        public async Task<IActionResult> UpdateAvailableIngredients(CocktailViewModel tempCocktailVm)
        {
            var cocktail = await this.context.Cocktails
                .Where(b => b.IsDeleted == false)
                .Include(b => b.CocktailIngredients)
                .ThenInclude(bc => bc.Ingredient)
                .FirstOrDefaultAsync(c => c.Id == tempCocktailVm.Id);

            this.context.CocktailIngredients.RemoveRange(cocktail.CocktailIngredients);
            await this.context.SaveChangesAsync();

            var ingredientForCocktails = await this._ingredientService.GetCocktailIngredientsAsync(cocktail.Id);
            var ingredientForCocktailIds = ingredientForCocktails.Select(c => c.Id);


            foreach (var item in tempCocktailVm.SelectedIngredients)
            {
                if (item.isChecked == true && !ingredientForCocktailIds.Contains(item.Id))
                {
                    await this._cocktailIngredientService.CreateCocktailIngredientAsync(cocktail.Id, item.Id);
                }
            }

            return RedirectToAction("Details", "Cocktails", new { area = "", id = cocktail.Id });
        }
    }
}

