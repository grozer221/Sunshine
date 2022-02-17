using System;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Models
{
    public class SubMenu : BaseModel
    {
        [Display(Name="Назва")]
        [Required(ErrorMessage = "Введіть назва")]
        public string Name { get; set; }

        [Display(Name = "Текст")]
        [Required(ErrorMessage = "Введіть текст")]
        public string Text { get; set; }

        [Display(Name = "Сортування")]
        public int Sorting { get; set; }

        [Display(Name = "Меню")]
        public int MenuId { get; set; }

        [Display(Name = "Меню")]
        public virtual Menu Menu { get; set; }
    }
}
