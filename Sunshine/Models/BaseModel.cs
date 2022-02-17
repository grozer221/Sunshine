using System;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Models
{
    public class BaseModel
    {
        [Display(Name = "Id")]
        public int Id{ get; set; }
        [Display(Name = "Створено")]
        public DateTime? CreatedAt { get; set; }
        [Display(Name = "Оновлено")]
        public DateTime? UpdatedAt { get; set; }
    }
}
