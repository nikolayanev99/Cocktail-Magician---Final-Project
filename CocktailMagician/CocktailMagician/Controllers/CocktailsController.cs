using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CocktailMagician.Web.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly ICocktailService _cocktailService;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> _cocktailVmMapper;

        public CocktailsController(ICocktailService cocktailService, IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper)
        {
            this._cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this._cocktailVmMapper = cocktailVmMapper ?? throw new ArgumentNullException(nameof(cocktailVmMapper));
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

            return View(result);
        }
    }
}
