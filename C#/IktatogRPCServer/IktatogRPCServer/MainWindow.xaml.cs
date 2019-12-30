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
using Grpc.Core;
using Iktato;
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
            StartServer();
        }
        Server server;
        const int Port = 1991;
        public void StartServer()
        {
            try
            {

                server = new Server
                {
                    Services = { IktatoService.BindService(new Service.SerivceForgRPC ()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure)}
                };
                server.Start();
                MessageBox.Show("The server has started successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
