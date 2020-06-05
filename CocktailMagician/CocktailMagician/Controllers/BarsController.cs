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
using System.Security.Claims;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using CocktailMagician.Services.Contracts;

namespace CocktailMagician.Web.Controllers
{
    public class BarsController : Controller
    {

        private readonly IBarService barService;
        private readonly IBarCommentsService barCommentsService;
        private readonly IBarRatingService barRatingService;
        private readonly IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper;
        private readonly IViewModelMapper<BarDTO, BarViewModel> barVmMapper;
        private readonly IViewModelMapper<BarRatingDto, BarRatingViewModel> barRatingVmMapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICocktailService cocktailService;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper;

        public BarsController(IBarService barService,
                              IBarCommentsService barCommentsService,
                              IBarRatingService barRatingService,
                              IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper,
                              IViewModelMapper<BarDTO, BarViewModel> barVmMapper,
                              IViewModelMapper<BarRatingDto, BarRatingViewModel> barRatingVmMapper,
                              IWebHostEnvironment webHostEnvironment,
                              ICocktailService cocktailService,
                              IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper)
        {
            this.barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this.barCommentsService = barCommentsService ?? throw new ArgumentNullException(nameof(barCommentsService));
            this.barRatingService = barRatingService ?? throw new ArgumentNullException(nameof(barRatingService));
            this.barCommentVmMapper = barCommentVmMapper ?? throw new ArgumentNullException(nameof(barCommentVmMapper));
            this.barVmMapper = barVmMapper ?? throw new ArgumentNullException(nameof(barVmMapper));
            this.barRatingVmMapper = barRatingVmMapper ?? throw new ArgumentNullException(nameof(barRatingVmMapper));
            this.webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            this.cocktailService=cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this.cocktailVmMapper = cocktailVmMapper;
        }




        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 4)
        {

            var models = await this.barService.GetBarsForPeginationAsync(pageSize, pageNumber);
            var countModels = await this.barService.GetAllBarsAsync();

            var result = this.barVmMapper.MapViewModel(models);

            var newResult = new PagedResult<BarViewModel>
            {
                Data = result.ToList(),
                TotalItems = countModels.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            return View(newResult);
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
            var barCommentDtos = await this.barCommentsService.GetBarCommentsAsync(id);
            barVM.Comments = this.barCommentVmMapper.MapViewModel(barCommentDtos);
            barVM.AverageRating = this.barRatingService.GetAverageBarRating(id);
            var cocktailsForBar = await this.cocktailService.GetBarCocktailsAsync(id);
            barVM.Cocktails = this.cocktailVmMapper.MapViewModel(cocktailsForBar);
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
        [HttpPut]
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

        public async Task<IActionResult> Search(string searchString, int pageNumber = 1, int pageSize = 4)
        {
            if (searchString == null)
            {
                return Redirect("Index");
            }

            int excludeRecodrds = (pageSize * pageNumber) - pageSize;

            var result = from b in await this.barService.SearchBarsAsync(searchString)
                         select b;

            var barsCount = result.Count();

            result = result
                .Skip(excludeRecodrds)
                .Take(pageSize);

            var models = this.barVmMapper.MapViewModel(result.ToList());

            var newResult = new PagedResult<BarViewModel>
            {
                Data = models.ToList(),
                TotalItems = barsCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            return View(newResult);
        }



    }
}
