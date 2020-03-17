using System;
using System.Collections.Generic;
using System.IO;
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
using Grpc.Core;
using System.Threading;
using System.Management;

using Iktato;
using System.Diagnostics;
using System.Windows.Threading;
using Microsoft.VisualBasic.Devices;
using IktatogRPCServer.Logger;

namespace IktatogRPCServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //StartServer();
            Logging.main = this;
            Logging.loggingText = BoxToLog;


        }

        Server server;
        const int Port = 443;
        public void StartServer()
        {
            try
            {
                var servercert = File.ReadAllText("certs/server.crt");
                var serverkey = File.ReadAllText("certs/server.key");
                
                KeyCertificatePair keyCertificatePair = new KeyCertificatePair(servercert, serverkey);
                SslServerCredentials credentials = new SslServerCredentials(new[] { keyCertificatePair });
                server = new Server
                {
                    Services = { IktatoService.BindService(new Service.SerivceForgRPC ()) },
                    Ports = { new ServerPort("localhost", Port, credentials)},
                    
                };
                server.Start();
                StartServerButton.IsEnabled = false;
                StopServerButton.IsEnabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void StartServer_Click(object sender, RoutedEventArgs e)
        {
            StartServer();
        }

        private void StopServerAndQuit_Click(object sender, RoutedEventArgs e)
        {
            if (server != null) {
                server.ShutdownAsync();
            }
            this.Close();
        }

        private void StopServerButton_Click(object sender, RoutedEventArgs e)
        {
            if (server != null)
            {
                server.ShutdownAsync();
                StartServerButton.IsEnabled = true;
                StopServerButton.IsEnabled = false;
            }
        }
    }
}
