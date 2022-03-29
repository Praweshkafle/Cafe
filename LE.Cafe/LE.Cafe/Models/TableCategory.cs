using LE.Cafe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LE.Cafe.Models
{
    public class TableCategory:BaseViewModel
    {
        public long table_category_id { get; set; }

        public string name { get; set; }

        public long position { get; set; }

        public bool is_shown { get; set; } = true;

        public string code { get; set; }
    }
}
