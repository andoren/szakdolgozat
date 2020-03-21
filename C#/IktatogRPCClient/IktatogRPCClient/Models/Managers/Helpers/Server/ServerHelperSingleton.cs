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
using Google.Protobuf;

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
    

        //TODO THE WHOLE HELPER! :(
        private UserHelperSingleton userHelper;
        private CallOptions calloptions;
        private IktatoService.IktatoServiceClient client;
        public static ServerHelperSingleton GetInstance()
        {

            return serverHelper;

        }

        public Channel GetChannel()
        {
            string hostname = ConfigurationManager.AppSettings["Hostname"];
            string hostport = ConfigurationManager.AppSettings["Port"];
            string csatinfo = $"{hostname}:{hostport}";
            var servercert = File.ReadAllText("cert/server.crt");
            SslCredentials creds = new SslCredentials(servercert);
            Channel channel = new Channel(csatinfo, creds);
            return channel;
        }

 

        public void InitializeConnection()
        {

            client = new IktatoService.IktatoServiceClient(GetChannel());
            userHelper = UserHelperSingleton.GetInstance();
            calloptions = new CallOptions().WithHeaders(new Metadata() { new Metadata.Entry("Authorization", userHelper.Token.Token) });
        }

        #endregion

        #region Getters
        public BindableCollection<Telephely> GetAllTelephely()
        {
            BindableCollection<Telephely> telephelyek = new BindableCollection<Telephely>();

            try
            {
                var stream = client.GetAllTelephely(new EmptyMessage(), calloptions);
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
                var stream = client.GetYears(new EmptyMessage(), calloptions);
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
                var stream = client.GetPartnerUgyintezoByPartner(selectedPartner, calloptions);
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
                var stream = client.GetPrivileges(new EmptyMessage(), calloptions);
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
            try
            {
                document = await client.GetDocumentByIdAsync(info, calloptions);
            }
            catch (RpcException ex)
            {
                InformationBox.ShowError(ex);
            }
            catch (Exception e)
            {
                InformationBox.ShowError(e);
            }
            return document;
        }
        public async Task<BindableCollection<Jelleg>> GetJellegekByTelephelyAsync(Telephely selectedTelephely)
        {
            /*
             if (selectedTelephely.Name == "Rákóczi") return new BindableCollection<Jelleg>() { new Jelleg() { Id = new Random().Next(1, 20), Name = "Fax" } };
                else return new BindableCollection<Jelleg>() { new Jelleg() { Id = 1, Name = "Levél" } };
             */
            BindableCollection<Jelleg> jellegek = new BindableCollection<Jelleg>();
            try
            {
                if (selectedTelephely != null)
                {
                    var stream = client.GetJellegekByTelephely(selectedTelephely, calloptions);
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
                    var stream = client.GetCsoportokByTelephely(selectedTelephely, calloptions);
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
                var stream = client.GetUserTelephelyei(message.GetUser, calloptions);
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
                    var stream = client.GetPartnerekByTelephely(selectedTelephely, calloptions);
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
                var stream = client.GetShortIktSzamokByTelephely(selectedTelephely, calloptions);
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
                Answer answer = await client.LogoutAsync(new EmptyMessage(), calloptions);
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
                    var stream = client.GetUgyintezokByTelephely(valasztottTelephely, calloptions);
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
                var stream = client.GetTelephelyek(new EmptyMessage(), calloptions);
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
                
                var stream = client.GetAllUser(new EmptyMessage(), calloptions);
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
                var stream = client.ListIktatas(searchData, calloptions);
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
                var stream = client.GetDocumentInfoByIkonyv(new Ikonyv() { Id = ikonyvId }, calloptions);
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
                Answer answer = await client.AddYearAndActivateAsync(new EmptyMessage(), calloptions);
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
                telephely = await client.AddTelephelyAsync(telephely, calloptions);
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
                
               
                
                
              
                using (var call = client.UploadDocument(calloptions))
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
                jelleg = await client.AddJellegToTelephelyAsync(newJelleg, calloptions);
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
                partnerUgyintezo = await client.AddPartnerUgyintezoToPartnerAsync(newUgyintezo, calloptions);
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
                partner = await client.AddPartnerToTelephelyAsync(newPartner, calloptions);
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
                proxy = new UserProxy(await client.AddUserAsync(user, calloptions));
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
                rovidIkonyv = await client.AddIktatasAsync(newIkonyv, calloptions);
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
                csoport = await client.AddCsoportToTelephelyAsync(newCsoport, calloptions);
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
                rovidIkonyv = await client.AddIktatasWithValaszAsync(newIkonyv);

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
                ugyintezo = await client.AddUgyintezoToTelephelyAsync(newUgyintezo, calloptions);
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
                Answer answer = await client.ModifyUserAsync(user, calloptions);
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
                Answer answer = await client.ModifyTelephelyAsync(telephely, calloptions);
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
                Answer answer = await client.ModifyPartnerUgyintezoAsync(selectedPartnerUgyintezo, calloptions);
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
                Answer answer = await client.ModifyPartnerAsync(selectedPartner, calloptions);
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
                Answer answer = await client.ModifyJellegAsync(modifiedJelleg, calloptions);
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
                Answer answer = await client.ModifyCsoportAsync(modifiedCsoport, calloptions);
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
                Answer answer = await client.ModifyUgyintezoAsync(modifiedUgyintezo, calloptions);
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
                Answer answer = await client.ModifyIktatasAsync(ikonyv, calloptions);
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
                Answer answer = await client.RemovedocumentAsync(selectedDocument, calloptions);
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
                Answer answer = await client.RemovePartnerUgyintezoAsync(selectedUgyintezo, calloptions);
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
                Answer answer = await client.RemovePartnerAsync(selectedPartner, calloptions);
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
                Answer answer = await client.DisableUserAsync(user, calloptions);
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
                Answer answer = await client.RemoveIkonyvAsync(new Ikonyv() { Id = id }, calloptions);
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
                Answer answer = await client.RemoveJellegAsync(selectedJelleg, calloptions);
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
                Answer answer = await client.RemoveTelephelyAsync(selectedTelephely, calloptions);
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
                Answer answer = await client.RemoveCsoportAsync(selectedCsoport, calloptions);
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
                Answer answer = await client.RemoveUgyintezoFromTelephelyAsync(valasztottUgyintezo, calloptions);
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
