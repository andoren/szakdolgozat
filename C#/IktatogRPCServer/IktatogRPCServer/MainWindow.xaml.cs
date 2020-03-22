using System;
using System.IO;
using System.Windows;
using Grpc.Core;
using Iktato;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.File;
using Serilog.Sinks.Observable;

namespace IktatogRPCServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, System.IObserver<Serilog.Events.LogEvent>
    {
        public MainWindow()
        {
            InitializeComponent();
            //StartServer();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
               .WriteTo.File(Directory.GetCurrentDirectory()+"\\logs\\log.txt", shared: true, rollingInterval: RollingInterval.Day)
               .WriteTo.Observers(events => events.Subscribe(this))
               .CreateLogger();

        }

        Server server;
        const int Port = 443;
        const string Ip = "localhost";
        public void StartServer()
        {
            try
            {
                Log.Debug("Mainwindow.StartServer: Ssl cert bolvasása.");
                var servercert = File.ReadAllText("certs/server.crt");
                Log.Debug("Mainwindow.StartServer: Ssl key bolvasása.");
                var serverkey = File.ReadAllText("certs/server.key");
                
                KeyCertificatePair keyCertificatePair = new KeyCertificatePair(servercert, serverkey);
                SslServerCredentials credentials = new SslServerCredentials(new[] { keyCertificatePair });
                Log.Debug("Mainwindow.StartServer: Server Binding port es cím");
                server = new Server
                {
                    Services = { IktatoService.BindService(new Service.SerivceForgRPC ()) },
                    Ports = { new ServerPort(Ip, Port, credentials)},
                    
                };
                Log.Debug("Mainwindow.StartServer: sikeres binding Ip:{Ip} Port: {Port}",Ip,Port);
                server.Start();
                StartServerButton.IsEnabled = false;
                StopServerButton.IsEnabled = true;

            }
            catch (Exception ex)
            {
                Log.Error("Következő hiba történt a szerver indulásakor: {Message}",ex);
            }
            Log.Information("A szerver elindult ");
            
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
            Log.CloseAndFlush();
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
            Log.CloseAndFlush();
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
