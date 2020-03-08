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
            mySelf = Process.GetCurrentProcess();
            theCPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            theMemCounter = new PerformanceCounter("Process", "Private Bytes",mySelf.ProcessName);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(250);
            timer.Tick += SetProcessLabel;
            timer.Start();
          

        }
        Process mySelf;
        PerformanceCounter theCPUCounter;
        PerformanceCounter theMemCounter;
        int AllMemoryInMb = Convert.ToInt32((new ComputerInfo().TotalPhysicalMemory / (Math.Pow(1024, 2))) + 0.5);
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
        private void SetProcessLabel(object sender, EventArgs e) {

            
          
            float processor = theCPUCounter.NextValue() / Environment.ProcessorCount;
            float memory = theMemCounter.NextValue()/(1024*1024);
            ProcessorLabel.Text = string.Format("{0}%",(int)processor);
            MemoryLable.Text = $"{(int)memory}MB/{AllMemoryInMb}MB";
            CpuBar.Value = (int)processor;
            MemoryBar.Value = (int)((((memory) / AllMemoryInMb) * 100));

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
