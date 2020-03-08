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
using System.Windows;
using Caliburn.Micro;

namespace IktatogRPCClient.Models.Managers
{
    class ServerHelper
    {
        //TODO THE WHOLE HELPER! :(
        AuthToken token;
        private CallOptions calloptions;
        private IktatoService.IktatoServiceClient client;
        #region Singleton props and methods
        private static ServerHelper serverHelper;
        private static readonly object lockable = new object();
        public static ServerHelper GetInstance()
        {
            lock (typeof (ServerHelper))
            {
                if (serverHelper == null) throw new InvalidCallOperation("For the first time you have to call the GetInstance with token parameter.");
                else return serverHelper;
            }
        }

 
        public static ServerHelper GetInstance(string token)
        {
            lock (typeof(ServerHelper))
            {
                if (serverHelper == null) serverHelper = new ServerHelper(token);
                return serverHelper;
            }
        }
        #endregion
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

        public async void Logout() {
            await client.LogoutAsync(new EmptyMessage(), calloptions);
        }

        internal Ugyintezo AddUgyintezoToTelephely(Telephely valasztottTelephely, string ugyintezoNeve)
        {
            return new Ugyintezo() {Id = new Random().Next(1,100),Name = ugyintezoNeve };
        }

        public  async static Task<User> Login(LoginMessage message) {
            try
            {
                User user = await new IktatoService.IktatoServiceClient(GetChannel()).LoginAsync(message);
                
                return user;
            }
            catch (RpcException re) {
                
                if(re.StatusCode == StatusCode.Unauthenticated) throw new LoginErrorException("Hibás felhasznlónév vagy jelszó! Próbálja megújra");
                return new User();
            }
            catch (Exception e) {
                MessageBox.Show("Sima Exception");

                return new User();
            }
            
        }

        public bool RemoveUgyintezoFromTelephely(Ugyintezo valasztottUgyintezo)
        {
            return true;
        }
        public BindableCollection<Ugyintezo> GetUgyintezokByTelephely(Telephely valasztottTelephely)
        {
            if (valasztottTelephely.Name == "Rákóczi")
                return new BindableCollection<Ugyintezo>() { new Ugyintezo() { Id = 3, Name = "Pekár Mihály" }, new Ugyintezo() { Id = 1, Name = "Brachna Anita" } };
            else return new BindableCollection<Ugyintezo>() { new Ugyintezo() { Id = 10, Name = "Csík Attila" }, new Ugyintezo() { Id = 1, Name = "Bulla Viktor" } };
        }

        public BindableCollection<Telephely> GetTelephelyek()
        {
            return new BindableCollection<Telephely>() { new Telephely() { Id = 1, Name = "Rákóczi" }, new Telephely() { Id = 2, Name = "Vajda" } };
        }

        public async Task<BindableCollection<Ikonyv>> GetIkonyvsFromToAsync(int from, int to) {
            return  new BindableCollection<Ikonyv>() {
                new Ikonyv() {
                CreatedBy = new User { Id= 1, Username = "misi" },
                Csoport = new Csoport() {Id = 5, Name="Kiscica", Shortname= "KC" },
                Erkezett = "2020.02.15",
                HatIdo = "2020.02.20",
                Id=232,
                Hivszam="1234/412Hivszám",
                Iktatoszam= "B-R/KC/M/2020",
                Jelleg = new Jelleg(){  Id=1 , Name="Levél"},
                Irany = 0,
                Partner = new Partner(){ Id= 1, Name = "MacskaKonzervGyártó", Ugyintezok = new PartnerUgyintezo(){ Id= 1, Name="FluffyBoy" } },
                Ugyintezo = new Ugyintezo(){Id = 1, Name="Brachna Anita"},
                Szoveg="Éhesek a cicám valamit jó volna baszni ennek a dolognak mert ez már nem állapot roar!",
                Targy="Cica éhes",
                Telephely = new Telephely(){ Id = 1 , Name= "Rákóczi"}

                } 
            };
        }
    }
} 
