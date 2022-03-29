using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace LE.Cafe.HelperClass
{
   public class ConnectionHelper
    {
        public static bool IsInternetConnectionAvailable()
        {
            var current = Connectivity.NetworkAccess;
            return current == NetworkAccess.Internet;
        }
    }
}
