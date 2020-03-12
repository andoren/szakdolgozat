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
        private UserHelperSingleton userHelper;
        private CallOptions calloptions;

        public  BindableCollection<Privilege> GetPrivileges()
        {
            return new BindableCollection<Privilege>() { new Privilege() { Id = 1, Name = "Admin" }, new Privilege() { Id = 2, Name = "User" } };
        }

        private IktatoService.IktatoServiceClient client;

        public Telephely AddTelephely(string telephelyNeve)
        {
            return new Telephely() { Id = new Random().Next(1, 100), Name = telephelyNeve };
        }

        public bool ModifyTelephely(Telephely telephely)
        {
            return true;
        }
        #region Singleton props and methods
        private static ServerHelperSingleton serverHelper = new ServerHelperSingleton();

        public BindableCollection<Csoport> GetCsoportokByTelephely(Telephely selectedTelephely)
        {
            if (selectedTelephely != null)
            {
                if (selectedTelephely.Name == "Rákóczi")
                    return new BindableCollection<Csoport>() { new Csoport() { Id = 3, Name = "Munkaügy", Shortname = "M" }, new Csoport() { Id = 1, Name = "Szerződés", Shortname = "SZ" }, new Csoport() { Id = 1, Name = "Konyha", Shortname = "K" } };
                else return new BindableCollection<Csoport>() { new Csoport() { Id = 10, Name = "Munkaügy", Shortname = "M" }, new Csoport() { Id = 1, Name = "Szerződés", Shortname = "SZ" } };
            }
            else return new BindableCollection<Csoport>();
        }

        public bool ModifyPartnerUgyintezo(PartnerUgyintezo selectedPartnerUgyintezo, string ugyintezoNeve)
        {
            return true;
        }

        public bool DisableUser(User getUser)
        {
            return true;
        }

        public PartnerUgyintezo AddPartnerUgyintezoToPartner(Partner selectedPartner,string ugyintezoNeve)
        {
            return new PartnerUgyintezo() { Id = new Random().Next(1,50),Name = ugyintezoNeve};
        }

        public bool ModifyPartner(Partner selectedPartner)
        {
            return true;
        }

        public Partner AddPartnerToTelephely(Telephely selectedTelephely, string partnerNeve)
        {
            return new Partner() { Id = new Random().Next(1, 50), Name = partnerNeve };
        }

        public BindableCollection<Partner> GetPartnerekByTelephely(Telephely selectedTelephely)
        {
            if (selectedTelephely != null)
            {
                Partner partner1 = new Partner() { Id = new Random().Next(1, 4), Name = "Beszállító cica" };
                Partner partner2 = new Partner() { Id = new Random().Next(5, 9), Name = "KutyaPartner" };
                Partner partner3 = new Partner() { Id = new Random().Next(5, 9), Name = "E-on" };
                partner1.Ugyintezok.Add(new PartnerUgyintezo() { Id = 1, Name = "Cili" });
                partner1.Ugyintezok.Add(new PartnerUgyintezo() { Id = 2, Name = "Marci" });
                partner1.Ugyintezok.Add(new PartnerUgyintezo() { Id = 3, Name = "Elemér" });
                partner2.Ugyintezok.Add(new PartnerUgyintezo() { Id = 4, Name = "Bodri" });
                if (selectedTelephely.Name == "Rákóczi") return new BindableCollection<Partner>() { partner1, partner2 };
                else return new BindableCollection<Partner>() { partner3 };
            }
            else return new BindableCollection<Partner>();
        }

        public UserProxy AddUser(string newUsername, string newFullname, string newPassword, Privilege selectedPrivilege, BindableCollection<Telephely> selectedTelephelyek)
        {
            User user =  new User() { Id = new Random().Next(1,50), Fullname = newFullname, 
                Password = newPassword, 
                Privilege = selectedPrivilege, 
                Username = newUsername};
            foreach (var item in selectedTelephelyek)
            {
                user.Telephelyek.Add(item);
            }
            return new UserProxy(user);
        }

        public bool ModifyJelleg(Jelleg modifiedJelleg)
        {
            return true;
        }

        public bool ModifyCsoport(Csoport modifiedCsoport)
        {
            return true;
        }



        public static ServerHelperSingleton GetInstance()
        {
  
                 return serverHelper;
            
        }

        public BindableCollection<Jelleg> GetJellegekByTelephely(Telephely selectedTelephely)
        {
            if (selectedTelephely != null) {
                if (selectedTelephely.Name == "Rákóczi") return new BindableCollection<Jelleg>() { new Jelleg() { Id = new Random().Next(1, 20), Name = "Fax" } };
                else return new BindableCollection<Jelleg>();
            }
            else {
                return new BindableCollection<Jelleg>();
            }
        }

        public bool ModifyUser(User getUser)
        {
            return true;
        }

        public Jelleg AddJellegToTelephely(Telephely selectedTelephely, string jellegNeve)
        {
            return new Jelleg() { Id = new Random().Next(1, 40), Name = jellegNeve };
        }

        public  bool RemovePartnerUgyintezo(PartnerUgyintezo selectedUgyintezo)
        {
            return true;
        }

        public bool RemovePartner(Partner selectedPartner)
        {
            return true;
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
            userHelper = UserHelperSingleton.GetInstance();
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

        public bool RemoveJelleg(Jelleg selectedJelleg)
        {
            return true;
        }

        public Csoport AddCsoportToTelephely(Telephely valasztottTelephely, string csoportName, string csoportKod)
        {
            return new Csoport() { Id = new Random().Next(1,200), Name=csoportName , Shortname = csoportKod};
        }

        public bool RemoveTelephely(Telephely selectedTelephely)
        {
            return true;
        }

        public async void Logout() {
            await client.LogoutAsync(new EmptyMessage(), calloptions);
        }

        public Ugyintezo AddUgyintezoToTelephely(Telephely valasztottTelephely, string ugyintezoNeve)
        {
            return new Ugyintezo() {Id = new Random().Next(1,100),Name = ugyintezoNeve };
        }

        public bool RemoveCsoport(Csoport selectedCsoport)
        {
            return true;
        }

        public bool RemoveUgyintezoFromTelephely(Ugyintezo valasztottUgyintezo)
        {
            return true;
        }
        public BindableCollection<Ugyintezo> GetUgyintezokByTelephely(Telephely valasztottTelephely)
        {
            if (valasztottTelephely != null)
            {
                if (valasztottTelephely.Name == "Rákóczi")
                    return new BindableCollection<Ugyintezo>() { new Ugyintezo() { Id = 3, Name = "Pekár Mihály" }, new Ugyintezo() { Id = 1, Name = "Brachna Anita" } };
                else return new BindableCollection<Ugyintezo>() { new Ugyintezo() { Id = 10, Name = "Csík Attila" }, new Ugyintezo() { Id = 1, Name = "Bulla Viktor" } };
            }
            else return new BindableCollection<Ugyintezo>();
        }

        public BindableCollection<Telephely> GetTelephelyek()
        {
            return new BindableCollection<Telephely>() { new Telephely() { Id = 1, Name = "Rákóczi" }, new Telephely() { Id = 2, Name = "Vajda" } };
        }

        public async Task<BindableCollection<Ikonyv>> GetIkonyvsFromToAsync(int from, int to) {
            Ikonyv konyv = new Ikonyv()
            {
                CreatedBy = new User { Id = 1, Username = "misi" },
                Csoport = new Csoport() { Id = 5, Name = "Kiscica", Shortname = "KC" },
                Erkezett = "2020.02.15",
                HatIdo = "2020.02.20",
                Id = 232,
                Hivszam = "1234/412Hivszám",
                Iktatoszam = "B-R/KC/M/2020",
                Jelleg = new Jelleg() { Id = 1, Name = "Levél" },
                Irany = 0,
                Partner = new Partner() { Id = 1, Name = "MacskaKonzervGyártó" },
                Ugyintezo = new Ugyintezo() { Id = 1, Name = "Brachna Anita" },
                Szoveg = "Éhesek a cicám valamit jó volna baszni ennek a dolognak mert ez már nem állapot roar!",
                Targy = "Cica éhes",
                Telephely = new Telephely() { Id = 1, Name = "Rákóczi" }

            };
            konyv.Partner.Ugyintezok.Add(new PartnerUgyintezo() { Id = 1, Name = "FluffyBoy" });
            return new BindableCollection<Ikonyv>() {
               konyv
            };
           
        }

        public BindableCollection<UserProxy> GetAllUser()
        {
            User user = new User()
            {
                Id = 4,
                Fullname = "Brachna Anita",
                Username = "banita",
                Password = "Gerike a kedvencem2019",
                Privilege = new Privilege() { Id = 2, Name = "User" }
            };
            user.Telephelyek.Add(new Telephely() { Id = 1,Name = "Rákóczi"});
            return new BindableCollection<UserProxy>() {
                new UserProxy(user)
                ,
                new UserProxy(UserHelperSingleton.CurrentUser) 
            };
        }
    }
} 
