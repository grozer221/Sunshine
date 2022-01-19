using Sunshine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunshine.Areas.Admin.ViewModels.Menus
{
    public class MenusDetailsViewModel
    {
        public Menu Menu{ get; set; }
        public IEnumerable<SubMenu> SubMenus { get; set; }
    }
}
