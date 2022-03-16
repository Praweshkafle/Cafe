using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace LE.Cafe.HelperClass
{
   public class HttpHelper
    {
        public const string BaseUri = "";

        public static RestClient GetClient()
        {
            return new RestClient();
        }

        public static RestClient GetClient(Uri uri)
        {
            return new RestClient(uri);
        }

    }
}
