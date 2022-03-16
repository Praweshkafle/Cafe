using System;
using System.Collections.Generic;
using System.Text;

namespace LE.Cafe.Models
{
    public class TableReservation
    {
        public long table_reservation_id { get; set; }
        public long table_id { get; set; }
        public long user_id { get; set; }
        public bool is_current { get; set; } = true;
        public string reserved_from { get; set; }
        public string reserved_to { get; set; }
        public long? table_reservation_online_id { get; set; }
        public bool is_billed { get; set; } = false;
        public bool is_synced { get; set; } = false;
    }
}
