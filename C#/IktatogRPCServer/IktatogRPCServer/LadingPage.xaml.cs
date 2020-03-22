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
        static LogEventLevel LogLevelToShow = LogEventLevel.Debug;
        public void OnNext(LogEvent value)
        {
            Dispatcher.Invoke(()=> {
                if((int)value.Level >= (int)LogLevelToShow) BoxToLog.Text += $"{DateTime.Now.ToString()} {value.RenderMessage()}\n";
            }); 
        }
        public static void SetLogLEvelToShow(LogEventLevel logEventLevel) {
            LogLevelToShow = logEventLevel;
        }
        public void OnError(Exception error)
        {
            Dispatcher.Invoke(() => {
                BoxToLog.Text += $"{DateTime.Now.ToString()} {error.Message}\n";
            });

        }

        public void OnCompleted()
        {

        }
    }
}
