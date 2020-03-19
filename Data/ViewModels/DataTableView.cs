using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class DataTableView
    {
        public IEnumerable<ActivityVM> data { get; set; }
        public int length { get; set; }
        public int filterLength { get; set; }
    }
}
