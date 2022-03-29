using System;
using System.Collections.Generic;
using System.Text;

namespace LE.Cafe.Models
{
    public class TableOrder
    {
        public long table_order_id { get; set; }
        public long table_reservation_id { get; set; }
        public long menu_item_id { get; set; }
        public decimal menu_rate { get; set; }
        public string ready_time { get; set; }
        public string delivery_time { get; set; }
        public decimal quantity { get; set; }
        public string order_time { get; set; }
        public string item_name { get; set; }
        public decimal totalAmount { get; set; }
    }
}
