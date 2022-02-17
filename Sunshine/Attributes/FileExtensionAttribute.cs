using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Sunshine.Attributes
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly string[] extensions = { "jpg", "png" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as FormFile;

            if (file != null)
            {
                string extension = Path.GetExtension(file.FileName);
                bool result = extensions.Any(x => extension.EndsWith(x));
                if (!result)
                    return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Дозволені розширення для файлів: " + string.Join(", ", extensions);
        }
    }
}
