using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer
{
    class RegistryHelper
    {
        const string userRoot = "HKEY_CURRENT_USER";
        const string subkey = "OtemplomIktato";
        const string keyName = userRoot + "\\" + subkey;
        public static string GetLogPath() {
            return (string)Registry.GetValue(keyName, "LogPath", "");
        }
        public static void SetLogPath(string Path) {
            Registry.SetValue(keyName, "LogPath", Path + "\\logs.txt");
        }
    }
}
