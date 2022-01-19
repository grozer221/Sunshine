using System.ComponentModel.DataAnnotations;

namespace Sunshine.Models
{
    public class SubMenu
    {
        public int Id { get; set; }
        [Display(Name="Назва")]
        public string Name { get; set; }
        [Display(Name = "Текст")]
        public string Text { get; set; }
        [Display(Name = "Сортування")]
        public int Sorting { get; set; }
        [Display(Name = "Меню")]
        public int MenuId { get; set; }
        [Display(Name = "Меню")]
        public virtual Menu Menu { get; set; }

    }
}
