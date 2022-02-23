using Microsoft.AspNetCore.Http;
using Sunshine.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Models
{
    public class File : BaseModel
    {
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Введіть ім'я")]
        public string Name { get; set; }
        
        [Display(Name = "Повне ім'я")]
        public string FullName { get; set; }
        
        [Display(Name = "Шлях")]
        public string Url { get; set; }

        [NotMapped]
        [Display(Name = "Файл")]
        [Required(ErrorMessage = "Виберіть файл")]
        public IFormFile Upload { get; set; }
    }
}
