using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CocktailMagician.Data;
using CocktailMagician.Models;
using CocktailMagician.Services;
using CocktailMagician.Web.Mappers.Contracts;
using CocktailMagician.Services.DtoEntities;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CocktailMagician.Web.Controllers
{
    public class BarsController : Controller
    {

        private readonly IBarService barService;
        private readonly IViewModelMapper<BarDTO, BarViewModel> barVmMapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BarsController(IBarService barService,
                              IViewModelMapper<BarDTO, BarViewModel> barVmMapper,
                              IWebHostEnvironment webHostEnvironment)
        {
            this.barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this.barVmMapper = barVmMapper ?? throw new ArgumentNullException(nameof(barVmMapper));
            this.webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }



        // GET: Bars
        public async Task<IActionResult> Index()
        {
            var models = await this.barService.GetAllBarsAsync();

            var result = this.barVmMapper.MapViewModel(models);

            return View(result);
        }

        // GET: Bars/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var bar = await this.barService.GetBarAsync(id);
            if (bar == null)
            {
                return NotFound();
            }
            var barVM = this.barVmMapper.MapViewModel(bar);

            return View(barVM);
        }

        // GET: Bars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bars/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarViewModel bar)
        {
            if (ModelState.IsValid)
            {
                
                if (bar.Photo != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    bar.PhotoPath = Guid.NewGuid().ToString() + " " + bar.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, bar.PhotoPath.ToString());
                    bar.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var barDTO = this.barVmMapper.MapDTO(bar);
                await this.barService.CreateBarAsync(barDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(bar);
        }

        // GET: Bars/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var bar = await this.barService.GetBarAsync(id);

            if (bar == null)
            {
                return NotFound();
            }

            var barVM = this.barVmMapper.MapViewModel(bar);

            return View(barVM);
        }

        // POST: Bars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BarViewModel bar)
        {
            if (id != bar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var barDTO = this.barVmMapper.MapDTO(bar);
                await this.barService.EditBarAsync(barDTO);

                return RedirectToAction(nameof(Index));
            }
            return View(bar);
        }

        // GET: Bars/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var bar = await this.barService.GetBarAsync(id);
            if (bar == null)
            {
                return NotFound();
            }
            var barVM = this.barVmMapper.MapViewModel(bar);

            return View(barVM);
        }

        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.barService.DeleteBarAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
