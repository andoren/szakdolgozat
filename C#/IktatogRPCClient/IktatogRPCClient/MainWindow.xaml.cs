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

namespace IktatogRPCClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        async Task connectToServer() {
            string csatinfo = "localhost" + ":" + "1991";
            Channel channel = new Channel(csatinfo, ChannelCredentials.Insecure);
            IktatoService.IktatoServiceClient client = new IktatoService.IktatoServiceClient(channel);
            LoginMessage login = new LoginMessage() {Username = "misi",Password="Kiscica" };
            User user = await client.LoginAsync(login);
            MessageBox.Show(user.Fullname);
        
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await connectToServer();
        }
    }
}
