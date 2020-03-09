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
using IktatogRPCClient.Models.Managers.Helpers.Client;

namespace IktatogRPCClient.Models.Managers
{
    class ServerHelperSingleton
    {
        //TODO THE WHOLE HELPER! :(
        private UserHelperSingleton userHelper = UserHelperSingleton.GetInstance();
        private CallOptions calloptions;
        private IktatoService.IktatoServiceClient client;
        #region Singleton props and methods
        private static ServerHelperSingleton serverHelper = new ServerHelperSingleton();
        private static readonly object lockable = new object();

        public static ServerHelperSingleton GetInstance()
        {
  
                 return serverHelper;
            
        }

 
        
        #endregion
        private ServerHelperSingleton()
        {
            
        }

        public bool ModifyUgyintezo(Ugyintezo modifiedUgyintezo)
        {
            return true;
        }

        public void InitializeConnection() {

            client = new IktatoService.IktatoServiceClient(GetChannel());
            calloptions = new CallOptions().WithHeaders(new Metadata() { new Metadata.Entry("Authorization", userHelper.Token.Token) });
        }
        public Channel GetChannel() {
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

        public Ugyintezo AddUgyintezoToTelephely(Telephely valasztottTelephely, string ugyintezoNeve)
        {
            return new Ugyintezo() {Id = new Random().Next(1,100),Name = ugyintezoNeve };
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
