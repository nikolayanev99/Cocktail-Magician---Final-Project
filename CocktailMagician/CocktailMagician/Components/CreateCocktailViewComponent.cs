using CocktailMagician.Services.Contracts;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Components
{
    public class CreateCocktailViewComponent : ViewComponent
    {
        private readonly IIngredientService _ingredientService;

        public CreateCocktailViewComponent(IIngredientService ingredientService)
        {
            this._ingredientService = ingredientService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
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
    }
}
