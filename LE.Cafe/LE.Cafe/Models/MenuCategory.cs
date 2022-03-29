using System;
using System.Collections.Generic;
using System.Text;

namespace LE.Cafe.Models
{
    public class MenuCategory
    {
        public long menu_category_id { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public bool is_visible_in_menu { get; set; } = true;
        public long position { get; set; }
        public virtual List<MenuItem> menu_items { get; set; }
    }
}
