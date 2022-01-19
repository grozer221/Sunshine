using Sunshine.Models;
using System.Collections.Generic;

namespace Sunshine.Areas.Admin.ViewModels.SubMenus
{
    public class SubMenusCreateViewModel
    {
        public SubMenu SubMenu { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
    }
}
