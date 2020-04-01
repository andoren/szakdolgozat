using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using System.Configuration;
using Iktato;

using System.Windows;
using Caliburn.Micro;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using Google.Protobuf;
using Serilog;
using System.Timers;

namespace IktatogRPCClient.Models.Managers
{
    class ServerHelperSingleton
    {
        private ServerHelperSingleton()
        {

        }



        #region Singleton props and methods
        private static ServerHelperSingleton serverHelper = new ServerHelperSingleton();
        private int chunkSize = 64 * 1024;

        private static Channel channel;

        private UserHelperSingleton userHelper;
        public static ServerHelperSingleton GetInstance()
        {

            return serverHelper;

        }

        public Channel GetChannel()
        {
            lock (typeof(ServerHelperSingleton)) {
                if (channel == null)
                {
                    userHelper = UserHelperSingleton.GetInstance();
                    CreateNewChannel();
                }
                else {
                    if (channel.State == ChannelState.TransientFailure || channel.State == ChannelState.Shutdown) {
                        CreateNewChannel();
                    }
                }
            }
            return channel;
        }
        private void CreateNewChannel() {
            string hostname = ConfigurationManager.AppSettings["Hostname"];
            string hostport = ConfigurationManager.AppSettings["Port"];
            string csatinfo = $"{hostname}:{hostport}";
            var servercert = File.ReadAllText("cert/server.crt");
            SslCredentials creds = new SslCredentials(servercert);
            //Channel channel = new Channel(csatinfo, ChannelCredentials.Insecure);
            channel = new Channel(csatinfo, creds, new[] { new ChannelOption("grpc.keepalive_permit_without_calls", 1) }); 
        }

        private IktatoService.IktatoServiceClient Client()
        {
            
            Log.Debug("{Class} Csatlakozás inicializációja", GetType());
            IktatoService.IktatoServiceClient client = new IktatoService.IktatoServiceClient(GetChannel());   
            Log.Debug("{Class} Calloption beállítása.", GetType());
            return client;
        }
        private CallOptions GetCallOption() {
            return new CallOptions().WithHeaders(new Metadata()
             {
                 new Metadata.Entry("Authorization", userHelper.Token.Token)
             });
        }
        #endregion

        #region Getters
        public BindableCollection<Telephely> GetAllTelephely()
        {
            BindableCollection<Telephely> telephelyek = new BindableCollection<Telephely>();

            try
            {
                var stream = Client().GetAllTelephely(new EmptyMessage(), GetCallOption());
                while (stream.ResponseStream.MoveNext().Result)
                {
                    telephelyek.Add(stream.ResponseStream.Current);
                }

            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return telephelyek;
        }

        public async Task<BindableCollection<Year>> GetYears()
        {
            BindableCollection<Year> years = new BindableCollection<Year>();

            try
            {
                var stream = Client().GetYears(new EmptyMessage(), GetCallOption());
                while (await stream.ResponseStream.MoveNext())
                {
                    years.Add(stream.ResponseStream.Current);
                }

            }
            catch (RpcException ex) {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return years;
        }
        public async Task<BindableCollection<PartnerUgyintezo>> GetPartnerUgyintezoByPartnerAsync(Partner selectedPartner)
        {
            BindableCollection<PartnerUgyintezo> ugyintezok = new BindableCollection<PartnerUgyintezo>();
            try
            {
                var stream = Client().GetPartnerUgyintezoByPartner(selectedPartner, GetCallOption());
                while (await stream.ResponseStream.MoveNext())
                {
                    ugyintezok.Add(stream.ResponseStream.Current);
                }
                
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return ugyintezok;
        }
        public BindableCollection<Privilege> GetPrivileges()
        {
            //new BindableCollection<Privilege>() { new Privilege() { Id = 1, Name = "Admin" }, new Privilege() { Id = 2, Name = "User" } };
            BindableCollection<Privilege> privileges = new BindableCollection<Privilege>();
            try
            {
                var stream = Client().GetPrivileges(new EmptyMessage(), GetCallOption());
                while (stream.ResponseStream.MoveNext().Result)
                {
                    privileges.Add(stream.ResponseStream.Current);
                }
                
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return privileges;
        }
        public async Task<Document> GetDocumentByIdAsync(DocumentInfo info)
        {
            Document document = new Document() { Name = "" };
            List<byte[]> Chunkes = new List<byte[]>();
            Document recivedDocuemnt = new Document();
            var call =  Client().GetDocumentById(info,GetCallOption());
            while (await call.ResponseStream.MoveNext())
            {
                recivedDocuemnt = call.ResponseStream.Current;
                Chunkes.Add(call.ResponseStream.Current.Doc.ToArray());
            }
            recivedDocuemnt.Doc = ByteString.CopyFrom(Chunkes.ToArray().SelectMany(inner => inner).ToArray());
            return recivedDocuemnt;
         }
          
            
  

        public async Task<BindableCollection<Jelleg>> GetJellegekByTelephelyAsync(Telephely selectedTelephely)
        {
 
            BindableCollection<Jelleg> jellegek = new BindableCollection<Jelleg>();
            try
            {
                if (selectedTelephely != null)
                {
                    var stream = Client().GetJellegekByTelephely(selectedTelephely, GetCallOption());
                    while (await stream.ResponseStream.MoveNext())
                    {
                        jellegek.Add(stream.ResponseStream.Current);
                    }
                    
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return jellegek;

        }
        public async Task<BindableCollection<Csoport>> GetCsoportokByTelephelyAsync(Telephely selectedTelephely)
        {
            /*
             *  if (selectedTelephely.Name == "Rákóczi")
                    return new BindableCollection<Csoport>() { new Csoport() { Id = 3, Name = "Munkaügy", Shortname = "M" }, new Csoport() { Id = 1, Name = "Szerződés", Shortname = "SZ" }, new Csoport() { Id = 1, Name = "Konyha", Shortname = "K" } };
                else return new BindableCollection<Csoport>() { new Csoport() { Id = 10, Name = "Munkaügy", Shortname = "M" }, new Csoport() { Id = 1, Name = "Szerződés", Shortname = "SZ" } };
             */
            BindableCollection<Csoport> csoportok = new BindableCollection<Csoport>();
            try
            {
                if (selectedTelephely != null)
                {
                    var stream = Client().GetCsoportokByTelephely(selectedTelephely, GetCallOption());
                    while (await stream.ResponseStream.MoveNext())
                    {
                        csoportok.Add(stream.ResponseStream.Current);
                    }
                  
                }

            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return csoportok;
        }

        public async Task<BindableCollection<Telephely>> GetUserTelephelyeiAsync(UserProxy message)
        {
            BindableCollection<Telephely> telephelyek = new BindableCollection<Telephely>();
            try
            {
                var stream = Client().GetUserTelephelyei(message.GetUser, GetCallOption());
                while (await stream.ResponseStream.MoveNext())
                {
                    telephelyek.Add(stream.ResponseStream.Current);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return telephelyek;
        }

        public async Task<BindableCollection<Partner>> GetPartnerekByTelephelyAsync(Telephely selectedTelephely)
        {
            /*
                Partner partner1 = new Partner() { Id = new Random().Next(1, 4), Name = "Beszállító cica" };
                Partner partner2 = new Partner() { Id = new Random().Next(5, 9), Name = "KutyaPartner" };
                Partner partner3 = new Partner() { Id = new Random().Next(5, 9), Name = "E-on" };
                partner1.Ugyintezok.Add(new PartnerUgyintezo() { Id = 1, Name = "Cili" });
                partner1.Ugyintezok.Add(new PartnerUgyintezo() { Id = 2, Name = "Marci" });
                partner1.Ugyintezok.Add(new PartnerUgyintezo() { Id = 3, Name = "Elemér" });
                partner2.Ugyintezok.Add(new PartnerUgyintezo() { Id = 4, Name = "Bodri" });
                if (selectedTelephely.Name == "Rákóczi") return new BindableCollection<Partner>() { partner1, partner2 };
                else return new BindableCollection<Partner>() { partner3 };
             */
            BindableCollection<Partner> partnerek = new BindableCollection<Partner>();
            try
            {
                if (selectedTelephely != null)
                {
                    var stream = Client().GetPartnerekByTelephely(selectedTelephely, GetCallOption());
                    while (await stream.ResponseStream.MoveNext())
                    {
                        partnerek.Add(stream.ResponseStream.Current);
                    }  
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return partnerek;
        }

    

        public async Task<BindableCollection<RovidIkonyv>> GetShortIktSzamokByTelephelyAsync(Telephely selectedTelephely)
        {
            /*new BindableCollection<RovidIkonyv>() { new RovidIkonyv() { Id=1,Iktatoszam="RövidIktatószám1" }, new RovidIkonyv() { Id = 2, Iktatoszam = "RövidIktatószám2" }, new RovidIkonyv() { Id = 3, Iktatoszam = "RövidIktatószám3" } };
             */
            BindableCollection<RovidIkonyv> rovidIkonyvs = new BindableCollection<RovidIkonyv>();
            try
            {
                var stream = Client().GetShortIktSzamokByTelephely(selectedTelephely, GetCallOption());
                while (await stream.ResponseStream.MoveNext())
                {
                    rovidIkonyvs.Add(stream.ResponseStream.Current);
                }
                
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return rovidIkonyvs;
        }
        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                Answer answer = await Client().LogoutAsync(new EmptyMessage(), GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    success = false;
                    throw new Exception(answer.Message);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }


            return success;
        }
        public async Task<BindableCollection<Ugyintezo>> GetUgyintezokByTelephelyAsync(Telephely valasztottTelephely)
        {
            BindableCollection<Ugyintezo> ugyintezok = new BindableCollection<Ugyintezo>();
            try
            {
                if (valasztottTelephely != null)
                {
                    var stream = Client().GetUgyintezokByTelephely(valasztottTelephely, GetCallOption());
                    while (await stream.ResponseStream.MoveNext())
                    {
                        ugyintezok.Add(stream.ResponseStream.Current);
                    }
                    
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }

            return ugyintezok;
        }
        public async Task<BindableCollection<Telephely>> GetTelephelyekAsync()
        {
          
            BindableCollection<Telephely> telephelyek = new BindableCollection<Telephely>();
            try
            {
                var stream = Client().GetTelephelyek(new EmptyMessage(), GetCallOption());
                while (await stream.ResponseStream.MoveNext())
                {
                    telephelyek.Add(stream.ResponseStream.Current);
                }
                
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return telephelyek;
        }
        public async Task<BindableCollection<UserProxy>> GetAllUserAsync()
        {
            BindableCollection<UserProxy> userProxies = new BindableCollection<UserProxy>();
            try
            {
                
                var stream = Client().GetAllUser(new EmptyMessage(), GetCallOption());
                while (await stream.ResponseStream.MoveNext())
                {
                    userProxies.Add(new UserProxy(stream.ResponseStream.Current));
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return userProxies;
        }
        public async Task<BindableCollection<Ikonyv>> GetIkonyvekAsync(SearchIkonyvData searchData)
        {
            BindableCollection<Ikonyv> ikonyvek = new BindableCollection<Ikonyv>();
            try
            {
                var stream = Client().ListIktatas(searchData, GetCallOption());
                while (await stream.ResponseStream.MoveNext())
                {
                    ikonyvek.Add(stream.ResponseStream.Current);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return ikonyvek;

        }
        public async Task<BindableCollection<DocumentInfo>> GetDocumentInfoByIkonyvIdAsync(int ikonyvId)
        {
            // return new BindableCollection<DocumentInfo>() { new DocumentInfo {Id = 1, Name = "KiscicaIktatás", Size = 3.54, Type = "PDF" } };
            BindableCollection<DocumentInfo> documentInfos = new BindableCollection<DocumentInfo>();
            try
            {
                var stream = Client().GetDocumentInfoByIkonyv(new Ikonyv() { Id = ikonyvId }, GetCallOption());
                while (await stream.ResponseStream.MoveNext())
                {
                    documentInfos.Add(stream.ResponseStream.Current);
                }
                
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return documentInfos;
        }
        #endregion

        #region Additions
        public async Task<bool> AddYearAndActivateAsync()
        {
            bool success = false;
            try
            {
                Answer answer = await Client().AddYearAndActivateAsync(new EmptyMessage(), GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }

            return success;
        }
        public async Task<Telephely> AddTelephelyAsync(string telephelyNeve)
        {
            Telephely telephely = new Telephely() { Id = 0, Name = telephelyNeve };
            try
            {
                telephely = await Client().AddTelephelyAsync(telephely, GetCallOption());
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return telephely;
        }
        public async Task<DocumentInfo> UploadDocumentAsync(Document doc)
        {
            DocumentInfo documentInfo = new DocumentInfo() { Id = 0 };
            try
            {

                using (var call = Client().UploadDocument(GetCallOption()))
                {
                    Document chunkDocument = new Document();
                    chunkDocument.Id = 0;
                    chunkDocument.IkonyvId = doc.IkonyvId;
                    chunkDocument.Name = doc.Name;
                    chunkDocument.Type = doc.Type;
                    byte[] byteToSend = doc.Doc.ToArray();
                    for (long i = 0; i < byteToSend.Length; i += chunkSize)
                    {

                        if (i + chunkSize > doc.Doc.Length)
                        {
                            chunkDocument.Doc = ByteString.CopyFrom(FromToByteArray(byteToSend, i, i + chunkSize - doc.Doc.Length));
                            await call.RequestStream.WriteAsync(chunkDocument);
                        }
                        else
                        {
                            chunkDocument.Doc = ByteString.CopyFrom(FromToByteArray(byteToSend, i, 0));
                            await call.RequestStream.WriteAsync(chunkDocument);
                        }


                    }
                    await call.RequestStream.CompleteAsync();
                    documentInfo = await call.ResponseAsync;
                }

            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return documentInfo;
        }

        private byte[] FromToByteArray(byte[] input, long from,long size ) {
            byte[] output;
            if (size == 0) output = new byte[chunkSize];
            
            else {
                output = new byte[chunkSize-size];
            }
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = input[from + i];
            }
            return output;
        }
        public async Task<Jelleg> AddJellegToTelephelyAsync(Telephely selectedTelephely, string jellegNeve)
        {
            Jelleg jelleg = new Jelleg() { Id = 0 };
            try
            {
                NewTorzsData newJelleg = new NewTorzsData(){ Telephely = selectedTelephely, Name = jellegNeve};
                jelleg = await Client().AddJellegToTelephelyAsync(newJelleg, GetCallOption());
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return jelleg;
        }
        public async Task<PartnerUgyintezo> AddPartnerUgyintezoToPartnerAsync(Partner selectedPartner, string ugyintezoNeve)
        {
            PartnerUgyintezo partnerUgyintezo = new PartnerUgyintezo() { Id = 0 };
            try
            {
                NewTorzsData newUgyintezo = new NewTorzsData(){ Partner = selectedPartner, Name = ugyintezoNeve };
                partnerUgyintezo = await Client().AddPartnerUgyintezoToPartnerAsync(newUgyintezo, GetCallOption());
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return partnerUgyintezo;
        }
        public async Task<Partner> AddPartnerToTelephelyAsync(Telephely selectedTelephely, string partnerNeve)
        {
            Partner partner = new Partner() { Id = 0 };
            try
            {
                NewTorzsData newPartner = new NewTorzsData() { Telephely = selectedTelephely, Name = partnerNeve };
                partner = await Client().AddPartnerToTelephelyAsync(newPartner, GetCallOption());
            }
            catch (RpcException ex) {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);

            }
            return partner;
        }
        public async Task<UserProxy> AddUserAsync(string newUsername, string newFullname, string newPassword, Privilege selectedPrivilege, BindableCollection<Telephely> selectedTelephelyek)
        {
            User user = new User()
            {
                Id = 0,
                Fullname = newFullname,
                Password = newPassword,
                Privilege = selectedPrivilege,
                Username = newUsername
            };

            foreach (var item in selectedTelephelyek)
            {
                user.Telephelyek.Add(item);
            }
            UserProxy proxy = new UserProxy(user);
            try
            {
                proxy = new UserProxy(await Client().AddUserAsync(user, GetCallOption()));
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return proxy;
        }
        public async Task<RovidIkonyv> AddIktatas(Ikonyv newIkonyv)
        {
            RovidIkonyv rovidIkonyv = new RovidIkonyv() { Id = 0 };
            try
            {
                rovidIkonyv = await Client().AddIktatasAsync(newIkonyv, GetCallOption());
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return rovidIkonyv;
        }
        public async Task<Csoport> AddCsoportToTelephelyAsync(Telephely valasztottTelephely, string csoportName, string csoportKod)
        {
            //new Csoport() { Id = new Random().Next(1,200), Name=csoportName , Shortname = csoportKod};
            Csoport csoport = new Csoport() { Id = 0 };
            try
            {
                NewTorzsData newCsoport = new NewTorzsData(){ Telephely = valasztottTelephely, Name = csoportName, Shorname = csoportKod};
                csoport = await Client().AddCsoportToTelephelyAsync(newCsoport, GetCallOption());
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return csoport;
        }
        public async Task<RovidIkonyv> AddIktatasWithValaszAsync(Ikonyv newIkonyv)
        {
            RovidIkonyv rovidIkonyv = new RovidIkonyv() { Id = 0 };
            try
            {
                rovidIkonyv = await Client().AddIktatasWithValaszAsync(newIkonyv);

            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return rovidIkonyv;
        }
        public async Task<Ugyintezo> AddUgyintezoToTelephelyAsync(Telephely valasztottTelephely, string ugyintezoNeve)
        {
            //return new Ugyintezo() {Id = new Random().Next(1,100),Name = ugyintezoNeve };
            Ugyintezo ugyintezo = new Ugyintezo() { Id = 0 };
            try
            {
                NewTorzsData newUgyintezo = new NewTorzsData(){ Telephely = valasztottTelephely, Name = ugyintezoNeve};
                ugyintezo = await Client().AddUgyintezoToTelephelyAsync(newUgyintezo, GetCallOption());
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return ugyintezo;
        }
        #endregion
        #region Modifications
        public async Task<bool> ModifyUserAsync(User user)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().ModifyUserAsync(user, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return success;
        }
        public async Task<bool> ModifyTelephelyAsync(Telephely telephely)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().ModifyTelephelyAsync(telephely, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return success;
        }
        public async Task<bool> ModifyPartnerUgyintezoAsync(PartnerUgyintezo selectedPartnerUgyintezo, string ugyintezoNeve)
        {
            bool success = false;
            try
            {
                selectedPartnerUgyintezo.Name = ugyintezoNeve;
                Answer answer = await Client().ModifyPartnerUgyintezoAsync(selectedPartnerUgyintezo, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }

            return success;
        }
        public async Task<bool> ModifyPartnerAsync(Partner selectedPartner)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().ModifyPartnerAsync(selectedPartner, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }

            return success;
        }
        public async Task<bool> ModifyJellegAsync(Jelleg modifiedJelleg)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().ModifyJellegAsync(modifiedJelleg, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }

            return success;
        }
        public async Task<bool> ModifyCsoportAsync(Csoport modifiedCsoport)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().ModifyCsoportAsync(modifiedCsoport, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }

            return success;
        }
        public async Task<bool> ModifyUgyintezoAsync(Ugyintezo modifiedUgyintezo)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().ModifyUgyintezoAsync(modifiedUgyintezo, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return success;
        }
        public async Task<bool> ModifyIkonyvAsync(Ikonyv ikonyv)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().ModifyIktatasAsync(ikonyv, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }

            return success;
        }
        #endregion
        #region Removes
        public async Task<bool> RemoveDocumentAsync(DocumentInfo selectedDocument)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().RemovedocumentAsync(selectedDocument, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;

        }
        public async Task<bool> RemovePartnerUgyintezoAsync(PartnerUgyintezo selectedUgyintezo)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().RemovePartnerUgyintezoAsync(selectedUgyintezo, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        public async Task<bool> RemovePartnerAsync(Partner selectedPartner)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().RemovePartnerAsync(selectedPartner, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        public async Task<bool> DisableUserAsync(User user)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().DisableUserAsync(user, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        public async Task<bool> RemoveIkonyvByIdAsync(int id)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().RemoveIkonyvAsync(new Ikonyv() { Id = id }, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        public async Task<bool> RemoveJellegAsync(Jelleg selectedJelleg)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().RemoveJellegAsync(selectedJelleg, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        public async Task<bool> RemoveTelephelyAsync(Telephely selectedTelephely)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().RemoveTelephelyAsync(selectedTelephely, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        public async Task<bool> RemoveCsoportAsync(Csoport selectedCsoport)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().RemoveCsoportAsync(selectedCsoport, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        public async Task<bool> RemoveUgyintezoFromTelephelyAsync(Ugyintezo valasztottUgyintezo)
        {
            bool success = false;
            try
            {
                Answer answer = await Client().RemoveUgyintezoFromTelephelyAsync(valasztottUgyintezo, GetCallOption());
                if (answer.Error == false)
                {
                    success = true;
                }
                else
                {
                    throw new Exception(answer.Message);
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        #endregion

































 
    }
} 
