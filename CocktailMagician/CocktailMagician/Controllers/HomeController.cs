using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CocktailMagician.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Models;

namespace CocktailMagician.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICocktailService _cocktailService;
        private readonly IBarService _barService;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> _cocktailVmMapper;
        private readonly IViewModelMapper<BarDTO, BarViewModel> _barVmMapper;

        public HomeController(ILogger<HomeController> logger,
            ICocktailService cocktailService,
            IBarService barService,
            IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper,
            IViewModelMapper<BarDTO, BarViewModel> barVmMapper)
        {
            this._logger = logger;
            this._cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this._barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this._cocktailVmMapper = cocktailVmMapper ?? throw new ArgumentNullException(nameof(cocktailVmMapper));
            this._barVmMapper = barVmMapper ?? throw new ArgumentNullException(nameof(barVmMapper));

        }

        public async Task<IActionResult> Index()
        {
            var topThreeBars = (await this._barService.GetThreeBarsAsync(3))
                .Select(b => this._barVmMapper.MapViewModel(b))
                .ToList();

            var topThreeCocktails = (await this._cocktailService.GetThreeCocktailsAsync(3))
                .Select(b => this._cocktailVmMapper.MapViewModel(b))
                .ToList();

            var model = new HomeViewModel
            {
                TopThreeBars = topThreeBars,
                TopThreeCocktails = topThreeCocktails
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
