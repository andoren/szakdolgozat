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
using IktatogRPCServer.Exceptions;
using System.IO;

namespace IktatogRPCServer
{
   public class RegistryHelper
    {
        const string userRoot = "HKEY_CURRENT_USER";
        const string subkey = "OtemplomIktato";
        const string keyName = userRoot + "\\" + subkey;
        public static string GetLogPath() {
            return (string)Registry.GetValue(keyName, "LogPath", "");
        }
        public static void SetLogPath(string Path) {
            if (!Directory.Exists(Path)) throw new DirectoryNotFoundException($"A mappa nem létezik: {Path}");
            else Registry.SetValue(keyName, "LogPath", Path + "\\logs.txt");

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
            LogEventLevel currentServerLogLevel = GetLogLevel();
            if (level < 0 || level > 5)
            {
                throw new InvalidLogLevelToShowException($"Hibás naplózási érték: {level}!");
                
            }
            if(level < (int)currentServerLogLevel) throw new InvalidLogLevelToShowException($"Hibás naplózási érték. A mutatott naplózási érték nem lehet kisebb mint a szerveré!");
            Registry.SetValue(keyName, "LogToShow", level);
        }

        public static void SetLogLevel(int level)
        {
            if (level < 0 || level > 5) {
                throw new InvalidLogLevelException($"Hibás naplózási érték: {level}!");
            } 
            Registry.SetValue(keyName, "LogLevel", level);

        }
    }
}
