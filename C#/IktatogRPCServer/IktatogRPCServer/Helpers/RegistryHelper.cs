using Serilog;
using Serilog.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Events;
using System.Windows;

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
        public static LogEventLevel GetLogLevelToShow() {
            int rawlevel = 3;
            if (int.TryParse(Registry.GetValue(keyName, "LogToShow", "3").ToString(), out rawlevel))
            {
                if (rawlevel < 0 || rawlevel > 5)
                {
                    rawlevel = 3;
                }
            }   
            return (LogEventLevel)rawlevel ; 
        }

        public static LogEventLevel GetLogLevel() {   
            int rawlevel=3;
            if (int.TryParse(Registry.GetValue(keyName, "LogLevel", "3").ToString(), out rawlevel)) {
                if (rawlevel < 0 || rawlevel > 5)
                {
                    rawlevel = 3;
                }
            }
            return (LogEventLevel)rawlevel;
            
        }
        public static void SetLogLevelToShow(int level)
        {
            if (level < 0 || level > 5)
            {
                MessageBox.Show("Hibás LogEventLevel!");
                return;
            }

            Registry.SetValue(keyName, "LogToShow", level);
        }

        public static void SetLogLevel(int level)
        {
            if (level < 0 || level > 5) {
                MessageBox.Show("Hibás LogEventLevel!");
                return;
            } 
            Registry.SetValue(keyName, "LogLevel", level);

        }
    }
}
