using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sunshine.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService()
        {
            _cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
        }

        public async Task<string> UplaodFileAsync(IFormFile file, string fileName)
        {
            Stream stream = file.OpenReadStream();
            var uploadParams = new RawUploadParams()
            {
                File = new FileDescription("files/" + fileName, stream),
                PublicId = fileName,
                Overwrite = true,
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.ToString();
        }
        
        public async Task<string> UplaodImageAsync(IFormFile image, string imageName)
        {
            Stream stream = image.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageName, stream),
                PublicId = imageName,
                Overwrite = true,
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.ToString();
        }
    }
}
