using Sunshine.Models;
using System.Collections.Generic;

namespace Sunshine.Areas.Admin.ViewModels
{
    public class MenusEditViewModel
    {
        public Menu Menu { get; set; }
        public IEnumerable<SubMenu> SubMenus { get; set; }
    }
}
