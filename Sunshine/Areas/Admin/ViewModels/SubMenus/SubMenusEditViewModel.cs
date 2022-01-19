using Sunshine.Models;
using System.Collections.Generic;

namespace Sunshine.Areas.Admin.ViewModels.SubMenus
{
    public class SubMenusEditViewModel
    {
        public SubMenu SubMenu { get; set; }
        public Menu SelectedMenu { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
    }
}
