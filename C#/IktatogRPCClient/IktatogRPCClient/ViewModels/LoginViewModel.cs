using Caliburn.Micro;
using Grpc.Core;
using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IktatogRPCClient.ViewModels
{
    class LoginViewModel:Screen
    {
        public LoginViewModel()
        {

        }
        async Task connectToServer()
        {
            string csatinfo = "localhost" + ":" + "1991";
            Channel channel = new Channel(csatinfo, ChannelCredentials.Insecure);
            IktatoService.IktatoServiceClient client = new IktatoService.IktatoServiceClient(channel);
            LoginMessage login = new LoginMessage() { Username = "misi", Password = "Kiscica" };
            User user = await client.LoginAsync(login);
            MessageBox.Show(user.Fullname);

        }
        public async void LoginButton() {
            await connectToServer();
        }
    }
}
