using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public static class License
    {
        public static string GetKey()
        {
            var mos =new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
            var serial = mos.Get().Cast<ManagementObject>()
                .Aggregate("", (current, mo) => current + mo.GetPropertyValue("SerialNumber").ToString());
            return serial;
        }

        public static bool CheckLisence()
        {
            var key1 = Security.RegistryManager.Read("key1");
            var key2 = Security.RegistryManager.Read("key2");
            var key3 = Security.RegistryManager.Read("key3");
            var now = DateTime.Now;
            if (key1 == null || key2 == null || key3 == null)
            {
                var message1 = now.ToString(CultureInfo.GetCultureInfo("en"));
                RegistryManager.Write("key1",message1);
                var message2 = now.AddDays(30).ToString(CultureInfo.GetCultureInfo("en"));
                RegistryManager.Write("key2", message2);
                var message3 = now.AddSeconds(1).ToString(CultureInfo.GetCultureInfo("en"));
                RegistryManager.Write("key3", message3);
                return true;
            }
            var current = DateTime.Parse(key3, CultureInfo.GetCultureInfo("en"));
            var start = DateTime.Parse(key1, CultureInfo.GetCultureInfo("en"));
            var end = DateTime.Parse(key2, CultureInfo.GetCultureInfo("en"));
            if (now < start || now > end) return false;
            if (now < current) return false;
            var message4 = now.AddSeconds(1).ToString(CultureInfo.GetCultureInfo("en"));
            RegistryManager.Write("key3", message4);
            return true;
        }
    }
}
