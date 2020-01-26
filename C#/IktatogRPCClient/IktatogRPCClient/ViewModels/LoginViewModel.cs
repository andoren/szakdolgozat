using Caliburn.Micro;
using Grpc.Core;
using Iktato;
using System;
using System.Collections.Generic;
using System.IO;
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
            string csatinfo = "localhost" + ":" + "443";
            var servercert = File.ReadAllText("cert/server.crt");
            SslCredentials creds = new  SslCredentials(servercert);
            Channel channel = new Channel(csatinfo,creds);        
            IktatoService.IktatoServiceClient client = new IktatoService.IktatoServiceClient(channel);           
            LoginMessage loginMessage = new LoginMessage() { Username = "misi", Password = "Kiscica" };
            User user = await client.LoginAsync(loginMessage, new CallOptions().WithHeaders(new Metadata() { new Metadata.Entry("Authorization", "Kiscica") }));
            if (user.Fullname == "") MessageBox.Show("Hibás felhasználó név vagy jelszó!");
            
        }
        public async void LoginButton() {
            try
            {
                await connectToServer();
                var manager = new WindowManager();
                manager.ShowWindow(new ContainerViewModel(), null, null);
                TryClose();
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
           
        }
        public void ExitButton() {
            TryClose();
        }
    }
}
