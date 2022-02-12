using Microsoft.AspNetCore.Http;
using Sunshine.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sunshine.Models
{
    public class New : BaseModel
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Display(Name = "Короткий текст")]
        public string ShortText { get; set; }
        [Display(Name = "Повний текст")]
        public string Text { get; set; }
        [Display(Name = "Картинка")]
        public string Image { get; set; }
        [NotMapped]
        [FileExtension]
        [Display(Name = "Картинка")]
        public IFormFile ImageUpload { get; set; }
    }
}
