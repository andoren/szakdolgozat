using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IktatogRPCServer.Logger
{
    public class Logging
    {
        public static TextBox loggingText;
        public static MainWindow main;
        public static void LogToScreenAndFile(string Message)
        {
            main.Dispatcher.Invoke(() =>
            {
                lock (loggingText)
                {
                    loggingText.Text += DateTime.Now + " - " + Message + Environment.NewLine;
                }
            });
        }
    }
}
