using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Models
{
    public class Menu : BaseModel
    {
        [Display(Name="Назва")]
        public string Name { get; set; }
        [Display(Name = "Сортування")]
        public int Sorting { get; set; }
        [Display(Name = "Під меню")]
        public virtual IEnumerable<SubMenu> SubMenus { get; set; }
    }
}
