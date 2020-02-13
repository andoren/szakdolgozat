using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using System.Configuration;
using Iktato;
using System.Threading;

namespace IktatogRPCClient.Models.Managers
{
    class ServerHelper
    {
        AuthToken token;
        private CallOptions calloptions;
        private IktatoService.IktatoServiceClient client;
        private static ServerHelper serverHelper;
        private ServerHelper()
        {

        }
        private ServerHelper(string token)
        {
            this.token = new AuthToken() { Token = token };
            InitializeConnection();
        }
        private void InitializeConnection() {

            client = new IktatoService.IktatoServiceClient(GetChannel());
            calloptions = new CallOptions().WithHeaders(new Metadata() { new Metadata.Entry("Authorization", token.Token) });
        }
        private static Channel GetChannel() {
            string hostname = ConfigurationManager.AppSettings["Hostname"];
            string hostport = ConfigurationManager.AppSettings["Port"];
            string csatinfo = $"{hostname}:{hostport}";
            var servercert = File.ReadAllText("cert/server.crt");
            SslCredentials creds = new SslCredentials(servercert);
            Channel channel = new Channel(csatinfo, creds);
            return channel;
        }
        public static ServerHelper GetInstance() {
            lock (serverHelper) {
                if (serverHelper == null) throw new InvalidCallOperation("For first call you have to call the GetInstance with token parameter.");
                else return serverHelper;
            }        
        }
        public static ServerHelper GetInstance(string token) {
            lock (serverHelper) {
                if (serverHelper == null) serverHelper = new ServerHelper(token);
                return serverHelper;
            }
        }
        public async void Logout() {
            await client.LogoutAsync(token, calloptions);
        }
        public static async Task<User> Login(LoginMessage message) {

            User user =  await new IktatoService.IktatoServiceClient(GetChannel()).LoginAsync(message);
            if (user.Username == "") throw new LoginErrorException("Hibás felhasznlónév vagy jelszó! Próbálja megújra");
            return user;
        }
    }
} 
