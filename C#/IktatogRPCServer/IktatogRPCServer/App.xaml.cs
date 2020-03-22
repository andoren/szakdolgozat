using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Serilog.Events;

namespace IktatogRPCServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            if (e.Args.Length > 1)
            {
                
               
               if (e.Args.Contains("-d")) window.SetServerLogLevel(LogEventLevel.Debug) ;
               if (e.Args.Contains("-s")) window.StartServer_Click(null, null);
              
               
            }
            window.Show();
        }
 
    }
}
