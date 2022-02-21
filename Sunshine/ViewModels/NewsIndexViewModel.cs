using Sunshine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunshine.ViewModels
{
    public class NewsIndexViewModel
    {
        public IEnumerable<New> News { get; set; }
        public int PagesCount { get; set; }
        public int PageNumber { get; set; }
    }
}
