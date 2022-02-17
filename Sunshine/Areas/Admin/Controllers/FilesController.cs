using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunshine.Areas.Admin.ViewModels;
using Sunshine.Models;
using Sunshine.Repositories;

namespace Sunshine.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class FilesController : Controller
    {
        private readonly FilesRepository _filesRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilesController(FilesRepository filesRepository, IWebHostEnvironment webHostEnvironment)
        {
            _filesRepository = filesRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Files
        public async Task<IActionResult> Index()
        {
            string protocol = HttpContext.Request.IsHttps ? "https" : "http";
            FilesIndexViewModel filesIndexViewModel = new FilesIndexViewModel
            {
                Files = await _filesRepository.Get(),
                WebRootPath = $"{protocol}://{HttpContext.Request.Host.Value}"
            };
            return View(filesIndexViewModel);
        }

        // GET: Admin/Files/Details/5
        public async Task<IActionResult> Details(int id)
        {
            string protocol = HttpContext.Request.IsHttps ? "https" : "http";
            FilesDetailsViewModel filesDetailsViewModel = new FilesDetailsViewModel
            {
                File = await _filesRepository.GetById(id),
                WebRootPath = $"{protocol}://{HttpContext.Request.Host.Value}"
            };
            if (filesDetailsViewModel.File == null)
                return NotFound();
            return View(filesDetailsViewModel);
        }

        // GET: Admin/Files/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Files/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,Upload")] File file)
        {
            if (ModelState.IsValid)
            {
                if (file.Upload != null)
                {
                    string uploadsDir = System.IO.Path.Combine(_webHostEnvironment.WebRootPath, "media/files");
                    string fileName = Guid.NewGuid().ToString() + "_" + file.Name + System.IO.Path.GetExtension(file.Upload.FileName);
                    string filePath = System.IO.Path.Combine(uploadsDir, fileName);
                    using(System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                    {
                        await file.Upload.CopyToAsync(fs);
                    }
                    file.Name = fileName;
                    await _filesRepository.Create(file);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Виберіть файл");
            }
            return View(file);
        }

        // GET: Admin/Files/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            File file = await _filesRepository.GetById(id);
            if (file == null)
                return NotFound();
            return View(file);
        }

        // POST: Admin/Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _filesRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
