using System;
using System.Collections.Generic;
using System.Text;

namespace LE.Cafe.Models
{
    public class User
        {
            public int user_id { get; set; }
            public string full_name { get; set; }
            public string email { get; set; }
            public string permanent_address { get; set; }
            public string temporary_address { get; set; }
            public string primary_contact { get; set; }
            public string secondary_contact { get; set; }
            public string roles { get; set; }
            public string token { get; set; }
            public string image_path { get; set; }
            public OrganisationDetail organisation_detail { get; set; }
        }

    }

