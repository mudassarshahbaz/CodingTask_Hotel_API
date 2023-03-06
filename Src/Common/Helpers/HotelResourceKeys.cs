using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class HotelResourceKeys
    {
        public static string InvalidJsonContent = GetValue(nameof(InvalidJsonContent));
        public static string HotelNotFound = GetValue(nameof(HotelNotFound));
        private static string GetValue(string key)
        {
            return""; //HotelResource.ResourceManager.GetString(key);
        }
    }
}
