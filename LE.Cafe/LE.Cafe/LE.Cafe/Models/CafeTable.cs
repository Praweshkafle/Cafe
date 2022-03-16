using System;
using System.Collections.Generic;
using System.Text;

namespace LE.Cafe.Models
{
    public class CafeTable
    {
        public string display_name { get; set; }
        public bool is_active { get; set; }
        public bool is_reserved { get; set; }
        public string name { get; set; }
        public int table_category_id { get; set; }
        public int table_id { get; set; }
    }

}
