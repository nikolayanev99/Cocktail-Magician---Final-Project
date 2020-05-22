﻿using System;
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

namespace CocktailMagician.Web.Controllers
{
    public class BarsController : Controller
    {

        private readonly IBarService barService;
        private readonly IBarCommentsService barCommentsService;
        private readonly IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper;
        private readonly IViewModelMapper<BarDTO, BarViewModel> barVmMapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BarsController(IBarService barService, 
                              IBarCommentsService barCommentsService,
                              IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper,
                              IViewModelMapper<BarDTO, BarViewModel> barVmMapper,
                              IWebHostEnvironment webHostEnvironment)
        {
            this.barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this.barCommentsService = barCommentsService ?? throw new ArgumentNullException(nameof(barCommentsService));
            this.barCommentVmMapper = barCommentVmMapper ?? throw new ArgumentNullException(nameof(barCommentVmMapper));
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
            var barCommentDtos = await this.barCommentsService.GetBarCommentsAsync(id);
            barVM.Comments = this.barCommentVmMapper.MapViewModel(barCommentDtos);

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
        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.barService.DeleteBarAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(BarViewModel bar)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var author = HttpContext.User.Identity.Name;

            var barComment = new BarCommentViewModel
            {
                Text = bar.CurrentComment,
                BarId = bar.Id,
                UserId = userId,
                Author = author,
            };

            var barCommentDto = this.barCommentVmMapper.MapDTO(barComment);

            var comment = await this.barCommentsService.CreateCommentAsync(barCommentDto);

            var currentBar = await this.barService.GetBarAsync(comment.BarId);

            var barVM = this.barVmMapper.MapViewModel(currentBar);
            var DtoComments = await this.barCommentsService.GetBarCommentsAsync(barVM.Id);
            barVM.Comments = this.barCommentVmMapper.MapViewModel(DtoComments);
                

            return View("Details", barVM);
        }
    }
}
