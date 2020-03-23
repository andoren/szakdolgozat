using Caliburn.Micro;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using IktatogRPCClient.ViewModels;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IktatogRPCClient
{
    public class Bootstrapper:BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            LogHelper.Initialize();
            if (e.Args.Contains("-d")) LogHelper.SetLoglevel(LogEventLevel.Debug);
            DisplayRootViewFor<LoginViewModel>();
        }
    }
}
