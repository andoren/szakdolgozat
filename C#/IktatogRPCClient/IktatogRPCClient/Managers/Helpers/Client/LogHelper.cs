using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace IktatogRPCClient.Models.Managers.Helpers.Client
{
    public static class LogHelper
    {
        private static LoggingLevelSwitch serverLevelSwitch = new LoggingLevelSwitch();
        public static void Initialize() {
            serverLevelSwitch.MinimumLevel = LogEventLevel.Warning;
            string LogPath = Directory.GetCurrentDirectory() + "\\logs\\log.txt";
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(serverLevelSwitch)
           .WriteTo.File(LogPath, shared: true, rollingInterval: RollingInterval.Day)    
           .CreateLogger();
        
        }
        public static void SetLoglevel(LogEventLevel level) {
            serverLevelSwitch.MinimumLevel = level;
        }
    }
}
