using System;
using System.IO;
using System.Windows;
using Grpc.Core;
using Iktato;
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
                Logger.Logging.LogToScreenAndFile("Hiba a szerver inditásakor. "+ex.Message);
            }
            Logger.Logging.LogToScreenAndFile("A szerver elindult ");
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
