using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunshine.Models;
using Sunshine.Repositories;
using Sunshine.Services;
using Sunshine.ViewModels;

namespace Sunshine.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class NewsController : Controller
    {
        private readonly NewsRepository _newsRep;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CloudinaryService _cloudinaryService;

        public NewsController(NewsRepository newsRep, IWebHostEnvironment webHostEnvironment, CloudinaryService cloudinaryService)
        {
            _newsRep = newsRep;
            _webHostEnvironment = webHostEnvironment;
            _cloudinaryService = cloudinaryService;
        }

        // GET: Admin/News
        public async Task<IActionResult> Index(int page = 1)
        {
            NewsIndexViewModel newsIndexViewModel =  await _newsRep.Get(page);
            return View(newsIndexViewModel);
        }

        // GET: Admin/News/Details/5
        public async Task<IActionResult> Details(int id)
        {
            New neww = await _newsRep.GetById(id);
            if (neww == null)
                return NotFound();

            return View(neww);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ShortText,Text,ImageUpload")] New neww)
        {
            if (ModelState.IsValid)
            {
                string imageName = "noimage.jpg";
                if (neww.ImageUpload != null)
                {
                    //string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/news");
                    imageName = Guid.NewGuid().ToString() + "_" + neww.ImageUpload.FileName;
                    //string filePath = Path.Combine(uploadsDir, imageName);
                    //FileStream fs = new FileStream(filePath, FileMode.Create);
                    //await neww.ImageUpload.CopyToAsync(fs);
                    //fs.Close();
                    neww.Image = await _cloudinaryService.UplaodImageAsync(neww.ImageUpload, imageName);
                }
                await _newsRep.Create(neww);
                return RedirectToAction(nameof(Index));
            }
            return View(neww);
        }

        // GET: Admin/News/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var neww = await _newsRep.GetById(id);
            if (neww == null)
                return NotFound();
            return View(neww);
        }

        // POST: Admin/News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ShortText,Text,ImageUpload,Image")] New neww)
        {
            if (id != neww.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (neww.ImageUpload != null)
                {
                    //string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/news");
                    //if (neww.Image != null && !string.Equals(neww.Image, "noimage.jpg"))
                    //{
                    //    string oldImagePath = Path.Combine(uploadsDir, neww.Image);
                    //    if (System.IO.File.Exists(oldImagePath))
                    //    {
                    //        System.IO.File.Delete(oldImagePath);
                    //    }
                    //}

                    string imageName = Guid.NewGuid().ToString() + "_" + neww.ImageUpload.FileName;
                    //string filePath = Path.Combine(uploadsDir, imageName);
                    //FileStream fs = new FileStream(filePath, FileMode.Create);
                    //await neww.ImageUpload.CopyToAsync(fs);
                    //fs.Close();
                    neww.Image = await _cloudinaryService.UplaodImageAsync(neww.ImageUpload, imageName);
                }
                await _newsRep.Update(neww);
                return RedirectToAction(nameof(Index));
            }
            return View(neww);
        }

        // GET: Admin/News/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var neww = await _newsRep.GetById(id);
            if (neww == null)
                return NotFound();

            return View(neww);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var neww = await _newsRep.GetById(id);
            //if (neww.Image != null && !string.Equals(neww.Image, "noimage.jpg"))
            //{
            //    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/news");
            //    string oldImagePath = Path.Combine(uploadsDir, neww.Image);
            //    if (System.IO.File.Exists(oldImagePath))
            //    {
            //        System.IO.File.Delete(oldImagePath);
            //    }
            //}
            await _newsRep.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
