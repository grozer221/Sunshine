using Sunshine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunshine.Areas.Admin.ViewModels
{
    public class FilesIndexViewModel
    {
        public IEnumerable<File> Files { get; set; }
        public int PagesCount { get; set; }
        public int PageNumber { get; set; }
    }
}
