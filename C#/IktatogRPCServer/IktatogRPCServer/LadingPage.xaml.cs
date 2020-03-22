using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IktatogRPCServer
{
    /// <summary>
    /// Interaction logic for LadingPage.xaml
    /// </summary>
    public partial class LadingPage : UserControl, IObserver<Serilog.Events.LogEvent>
    {
        public LadingPage()
        {
            InitializeComponent();
        }

        public void OnNext(LogEvent value)
        {
            Dispatcher.Invoke(()=> {
                if(value.Level == LogEventLevel.Warning || value.Level == LogEventLevel.Information)BoxToLog.Text += value.MessageTemplate.Text + "\n";
            }); 
        }

        public void OnError(Exception error)
        {
            Dispatcher.Invoke(() => {
                BoxToLog.Text += error.Message + "\n";
            });

        }

        public void OnCompleted()
        {

        }
    }
}
