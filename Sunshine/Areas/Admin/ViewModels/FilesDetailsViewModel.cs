using Sunshine.Models;
using System.Collections.Generic;

namespace Sunshine.Areas.Admin.ViewModels
{
    public class FilesIndexViewModel
    {
        public IEnumerable<File> Files { get; set; }
        public string WebRootPath { get; set; }
    }
}
