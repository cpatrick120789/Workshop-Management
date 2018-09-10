using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Security
{
    public static class RegistryManager
    {
        public static void Write(string key,string message)
        {
            try
            {
                var rk1 = Registry.CurrentUser;
                var rk2 = rk1.OpenSubKey("Software", true);
                if (rk2 == null) rk2 = rk1.OpenSubKey("Microsoft", true);
                if (rk2 == null) rk2 = rk1.OpenSubKey("Windows", true);
                rk2.SetValue(key, message);
            }
            catch (Exception)
            {
               
            }
        }
        public static string Read(string key)
        {
            try
            {
                var rk1 = Registry.CurrentUser;
                var rk2 = rk1.OpenSubKey("Software", false);
                if (rk2 == null) rk2 = rk1.OpenSubKey("Microsoft", false);
                if (rk2 == null) rk2 = rk1.OpenSubKey("Windows", false);
                var value = rk2.GetValue(key);
                return value.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
