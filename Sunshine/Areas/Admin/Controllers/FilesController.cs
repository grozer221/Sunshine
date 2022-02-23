using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sunshine.Areas.Admin.ViewModels;
using Sunshine.Models;
using Sunshine.Repositories;
using Sunshine.Services;
using Sunshine.ViewModels;

namespace Sunshine.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class FilesController : Controller
    {
        private readonly FilesRepository _filesRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CloudinaryService _cloudinaryService;

        public FilesController(FilesRepository filesRepository, IWebHostEnvironment webHostEnvironment, CloudinaryService cloudinaryService)
        {
            _filesRepository = filesRepository;
            _webHostEnvironment = webHostEnvironment;
            _cloudinaryService = cloudinaryService;
        }

        // GET: Admin/Files
        public async Task<IActionResult> Index(int page = 1)
        {
            FilesIndexViewModel filesIndexViewModel = await _filesRepository.Get(page);
            string protocol = HttpContext.Request.IsHttps ? "https" : "http";
            //filesIndexViewModel.WebRootPath = $"{protocol}://{HttpContext.Request.Host.Value}";
            return View(filesIndexViewModel);
        }

        // GET: Admin/Files/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var file = await _filesRepository.GetById(id);
            if (file == null)
                return NotFound();
            return View(file);
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
                    //string uploadsDir = System.IO.Path.Combine(_webHostEnvironment.WebRootPath, "media/files");
                    string fileName = Guid.NewGuid().ToString() + "_" + file.Name + System.IO.Path.GetExtension(file.Upload.FileName);
                    //string filePath = System.IO.Path.Combine(uploadsDir, fileName);
                    //using(System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                    //{
                    //    await file.Upload.CopyToAsync(fs);
                    //}

                    //using (AmazonS3Client client = new AmazonS3Client("AKIAWLBT5FIB3Z6FNWT3", "ZCDv8mqZtUeflrlHZpcwq1VXo7IYw/HCYO39G6fC", RegionEndpoint.EUWest3))
                    //{
                    //    using (System.IO.MemoryStream newMemoryStream = new System.IO.MemoryStream())
                    //    {
                    //        file.Upload.CopyTo(newMemoryStream);


                    //        var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    //        {
                    //            InputStream = newMemoryStream,
                    //            BucketName = "zdo-sone4ko",
                    //            StorageClass = S3StorageClass.StandardInfrequentAccess,
                    //            Key = "zdo-sone4ko/path/to/" + fileName,
                    //            CannedACL = S3CannedACL.PublicRead
                    //        };
                    //        var fileTransferUtility = new TransferUtility(client);
                    //        await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                    //    }
                    //}
                    file.Name = file.Upload.FileName;
                    file.FullName = fileName;
                    file.Url = await _cloudinaryService.UplaodFileAsync(file.Upload, fileName);
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
