using System;
using System.Collections.Generic;
using System.Text;

namespace LE.Cafe.Models
{
    public class MenuItem
    {
        public long menu_item_id { get; set; }
        public long menu_category_id { get; set; }
        public long menu_unit_id { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public bool is_shown { get; set; } = true;
        public decimal rate { get; set; }
        public long position { get; set; }
    }
}
