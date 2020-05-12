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
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((object sender, UnhandledExceptionEventArgs e) => {
                MessageBox.Show((e.ExceptionObject as Exception).Message);
            });
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            if (e.Args.Length > 0)
            {
                if (e.Args.Contains("-d")) RegistryHelper.SetLogLevel(2);
                if (e.Args.Contains("-s")) window.StartServer_Click(null, null);                          
            }
            window.Show();
        }
 
    }
}
