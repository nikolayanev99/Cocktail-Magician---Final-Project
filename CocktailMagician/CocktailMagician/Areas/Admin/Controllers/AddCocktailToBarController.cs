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

    public class AddCocktailToBarController : Controller
    {
        private readonly CocktailMagicianContext context;
        private readonly IBarService barService;
        private readonly IBarCocktailsService barCocktailsService;
        private readonly ICocktailService cocktailService;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper;

        public AddCocktailToBarController(CocktailMagicianContext context, 
                                          IBarService barService, 
                                          IBarCocktailsService barCocktailsService, 
                                          ICocktailService cocktailService, 
                                          IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this.barCocktailsService = barCocktailsService ?? throw new ArgumentNullException(nameof(barCocktailsService));
            this.cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this.cocktailVmMapper = cocktailVmMapper ?? throw new ArgumentNullException(nameof(cocktailVmMapper));
        }

        public async Task<IActionResult> AvailableCocktails(int barId)
        {
            var bar = await this.barService.GetBarAsync(barId);
            var allCocktails = await this.cocktailService.GetAllCocktailsAsync();

            var allCocktailsVm = allCocktails
                .Select(x => this.cocktailVmMapper.MapViewModel(x))
                .Select(x => new CheckBoxItem()
                {
                    Id = x.Id,
                    Title = x.Name,
                    isChecked = context.BarCocktails.Any(bc => bc.BarId == barId && bc.CocktailId==x.Id) ? true : false,
                }).ToList();

            var availableCocktails = await this.cocktailService.GetBarCocktailsAsync(barId);
            var availableCocktailsVm = this.cocktailVmMapper.MapViewModel(availableCocktails);

            var barVm = new BarViewModel
            {
                Id = bar.Id,
                Name=bar.Name,
                SelectedCocktails = allCocktailsVm,
                Cocktails = availableCocktailsVm,
            };

            return View(barVm);

        }
        public async Task<IActionResult> UpdateAvailableCocktails(BarViewModel tempBarVm)
        {
            var bar = await this.context.Bars
                .Where(b => b.IsDeleted == false)
                .Include(b=>b.BarCocktails) 
                .ThenInclude(bc=>bc.Cocktail)
                .FirstOrDefaultAsync(bar => bar.Id == tempBarVm.Id);

            this.context.BarCocktails.RemoveRange(bar.BarCocktails);
            await this.context.SaveChangesAsync();

            var cocktailsForBar = await this.cocktailService.GetBarCocktailsAsync(bar.Id);
            var cocktailsForBarIds=cocktailsForBar.Select(c => c.Id);


            foreach (var item in tempBarVm.SelectedCocktails)
            {
                if (item.isChecked == true && !cocktailsForBarIds.Contains(item.Id))
                {
                    await this.barCocktailsService.CreateBarCocktail(bar.Id, item.Id);
                }
            }

            return RedirectToAction("Details", "Bars", new { area = "", id = bar.Id });
        }
    }
}

