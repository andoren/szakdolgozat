using System;
using System.Configuration;
using System.IO;
using System.Windows;
using Grpc.Core;
using Iktato;
using Microsoft.Win32;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.File;
using Serilog.Sinks.Observable;

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
            ContentControl.Content = ladingPage;
            LogPath = RegistryHelper.GetLogPath();
        }


        LadingPage ladingPage = new LadingPage(); 
        Server server;
        int Port = int.Parse(ConfigurationManager.AppSettings["APPPORT"]);
        string Ip = ConfigurationManager.AppSettings["APPHOST"];
        private static LoggingLevelSwitch serverLevelSwitch = new LoggingLevelSwitch();
        private static string _logPath = "";

        public static string LogPath
        {
            get { return _logPath; }
            set { _logPath = value; }
        }

        public void StartServer_Click(object sender, RoutedEventArgs e)
        {
            StartLogger();
            StartServer();
        }
        public  void ChangeLogLevel(LogEventLevel level) {
            serverLevelSwitch.MinimumLevel = level;
        }
        private void StartLogger()
        {
            serverLevelSwitch.MinimumLevel = RegistryHelper.GetLogLevel();
            if (LogPath == "") LogPath = Directory.GetCurrentDirectory() + "\\logs\\log.txt";
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(serverLevelSwitch)
           .WriteTo.File(LogPath, shared: true, rollingInterval: RollingInterval.Day)
           .WriteTo.Observers(events => events.Subscribe(ladingPage))
           .CreateLogger();           
        }
        private void StartServer()
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
                    Services = { IktatoService.BindService(new Service.SerivceForgRPC()) },
                    //Ports = { new ServerPort(Ip, Port, credentials) }
                    Ports = { new ServerPort(Ip, Port, ServerCredentials.Insecure) }

                };
                Log.Debug("Mainwindow.StartServer: sikeres binding Ip:{Ip} Port: {Port}", Ip, Port);
                server.Start();
                StartServerButton.IsEnabled = false;
                StopServerButton.IsEnabled = true;

            }
            catch (Exception ex)
            {
                Log.Error("Következő hiba történt a szerver indulásakor: {Message}", ex);
            }
            Log.Information("A szerver elindult ");

        }

  
        private void StopServerAndQuit_Click(object sender, RoutedEventArgs e)
        {
            if (server != null) {
                server.ShutdownAsync();
            }
            Log.Warning("A szerver leáll.");
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
                Log.Warning("A szerver leáll.");
            }
            Log.CloseAndFlush();
        }
        private void Logging_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new ManageLogging();
        }

        private void SqlButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new ManageSql();
        }

        private void LadingPageButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = ladingPage;
        }

        private void UserManageButton_Click(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new UserManage();
        }
    }
}
