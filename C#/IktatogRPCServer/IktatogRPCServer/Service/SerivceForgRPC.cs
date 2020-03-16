using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Iktato;
using IktatogRPCServer.Database;
using IktatogRPCServer.Database.Mysql;
using IktatogRPCServer.Database.Mysql.Abstract;

namespace IktatogRPCServer.Service
{
    class SerivceForgRPC:IktatoService.IktatoServiceBase
    {
        readonly TokenSerivce TokenManager = new TokenSerivce();
        readonly ConnectionManager connectionManager = new ConnectionManager();
        public override Task<User> Login(LoginMessage request, ServerCallContext context)
        {
            UserDatabaseManager userManager = new UserDatabaseManager(new Database.ConnectionManager());
            User user;
            if (userManager.IsValidUser(request, out user)) {

                user.AuthToken = new AuthToken() { Token = TokenManager.GenerateToken(user) };                 
                return Task.FromResult<User>(user);
            }
            else {
                Status s = new Status(StatusCode.Unauthenticated, "Hibás felhasználónév vagy jelszó!");
                return Task.FromException<User>(new RpcException(s));
            }

            //if (context.RequestHeaders.Select(x => x.Value == "kiscica") != null)
            //{
            //    return Task.FromResult<User>(new User() { Id = 1, Username = "Misi", Fullname = "Pekár Mihály",, Privilege = new Privilege() { Id = 1, Name = "Admin" } });
            //}
            
        }
        public override Task<Answer> Logout(EmptyMessage request, ServerCallContext context)
        {
            try
            {
              
                 return Task.FromResult<Answer>(new Answer() { Error=false, Message = "Sikeres kijelentkezés."});
               
            }
            catch (Exception e) {
                return Task.FromException<Answer>(e);
            }

        }
        public override Task<Answer> Register(User request, ServerCallContext context)
        {

            return base.Register(request, context);
        }
        public override async Task<RovidIkonyv> AddIktatas(Ikonyv request, ServerCallContext context)
        {
            return await Task.Run(()=> {
               
                return new RovidIkonyv() { Id = new Random().Next(1,400),Iktatoszam="Added SZám"} ;
                
            });
        }
       
        public override Task ListallIktatas(EmptyMessage request, IServerStreamWriter<Ikonyv> responseStream, ServerCallContext context)
        {
            return base.ListallIktatas(request, responseStream, context);
        }
        public override Task<Answer> ModifyIktatas(Ikonyv request, ServerCallContext context)
        {
            return base.ModifyIktatas(request, context);
        }
        public override Task<Answer> DeleteIktatas(DeleteMessage request, ServerCallContext context)
        {
            return base.DeleteIktatas(request, context);
        }
        public override async Task ListIktatas(SearchIkonyvData request, IServerStreamWriter<Ikonyv> responseStream, ServerCallContext context)
        {
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
            Ikonyv konyv2 = new Ikonyv()
            {
                CreatedBy = new User { Id = 1, Username = "misi" },
                Csoport = new Csoport() { Id = 5, Name = "Kiscica", Shortname = "KC" },
                Erkezett = "2020.02.15",
                HatIdo = "2020.02.20",
                Id = 232,
                HasDoc = true,
                Hivszam = "1234/412Hivszám",
                Iktatoszam = "B-R/KC/M/2020",
                Jelleg = new Jelleg() { Id = 1, Name = "Levél" },
                Irany = 0,
                Partner = new Partner() { Id = 1, Name = "MacskaKonzervGyártó2" },
                Ugyintezo = new Ugyintezo() { Id = 1, Name = "Brachna Anita" },
                Szoveg = "Éhesek a cicám valam2222it jó volna baszni ennek a dolognak mert ez már nem állapot roar!",
                Targy = "Cica éhes2",
                Telephely = new Telephely() { Id = 1, Name = "Rákóczi" }

            };
            konyv2.Partner.Ugyintezok.Add(new PartnerUgyintezo() { Id = 1, Name = "FluffyBoy2" });
            Ikonyv konyv3 = new Ikonyv()
            {
                CreatedBy = new User { Id = 1, Username = "misi" },
                Csoport = new Csoport() { Id = 5, Name = "Kiscica2", Shortname = "KC2" },
                Erkezett = "2020.02.15",
                HatIdo = "2020.02.20",
                Id = 232,
                Hivszam = "1234/412Hivszám",
                Iktatoszam = "Kiscicaikató",
                Jelleg = new Jelleg() { Id = 1, Name = "Levél" },
                Irany = 0,
                Partner = new Partner() { Id = 1, Name = "Beszállító Kiscica" },
                Ugyintezo = new Ugyintezo() { Id = 1, Name = "Pekár Mihály" },
                Szoveg = "Éhesek a cicám valam2222it jó volna baszni ennek a dolognak mert ez már nem állapot roar!",
                Targy = "Ez egy kiktatás",
                Telephely = new Telephely() { Id = 1, Name = "Vajda" }

            };
            konyv2.Partner.Ugyintezok.Add(new PartnerUgyintezo() { Id = 1, Name = "FluffyBoy3" });
            Ikonyv konyv0 = new Ikonyv()
            {
                CreatedBy = new User { Id = 1, Username = "misi" },
                Csoport = new Csoport() { Id = 5, Name = "Kutya", Shortname = "WUFF" },
                Erkezett = "2020.02.15",
                HatIdo = "2020.02.20",
                Id = 233,
                Hivszam = "1234/412Hivszám",
                Iktatoszam = "Első Iktatás",
                Jelleg = new Jelleg() { Id = 1, Name = "Levél" },
                Irany = 0,
                Partner = new Partner() { Id = 1, Name = "Meow" },
                Ugyintezo = new Ugyintezo() { Id = 1, Name = "Kis Cica" },
                Szoveg = "Éhesek a cicám valam2222it jó volna baszni ennek a dolognak mert ez már nem állapot roar!",
                Targy = "Ez egy kiktatás",
                Telephely = new Telephely() { Id = 1, Name = "Vajda" }

            };
            konyv0.Partner.Ugyintezok.Add(new PartnerUgyintezo() { Id = 1, Name = "FluffyBoy0" });
            List<Ikonyv> ikonyvek = new List<Ikonyv>() {konyv0, konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2, 
                konyv, konyv2, konyv, konyv2, konyv, konyv2, konyv, konyv2, konyv, konyv2,
                konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,
            konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,
                konyv, konyv2, konyv, konyv2, konyv, konyv2, konyv, konyv2, konyv, konyv2,
                konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,
            konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,
                konyv, konyv2, konyv, konyv2, konyv, konyv2, konyv, konyv2, konyv, konyv2,
                konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,
            konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv,konyv2,konyv3};
            foreach (var response in ikonyvek)
            {
                await responseStream.WriteAsync(response);
               
            }
        }
        public override async Task<Document> GetDocumentById(DocumentInfo request, ServerCallContext context)
        {
            
            return await Task.Run(()=> {
                MysqlDatabaseManager<Document> databaseManager = new DocumentDatabaseManager(connectionManager);
                Document document = databaseManager.GetDataById(request.Id);
                return document; 
            });
        }
        public override async Task<DocumentInfo> UploadDocument(Document request, ServerCallContext context)
        {
            return await Task.Run(() => {
                
                return new DocumentInfo() { Id = 2, Name = request.Name, Size = (request.Doc.Length/(double)1024)/1024, Type = request.Type };
            });
          
        }
        public override Task<Answer> Removedocument(DocumentInfo request, ServerCallContext context)
        {
            return Task.Run(() => { return new Answer() { Error=false, Message="A törlés sikeres volt." }; });
        }
        public override Task<Csoport> AddCsoportToTelephely(NewTorzsData request, ServerCallContext context)
        {
            return base.AddCsoportToTelephely(request, context);
        }
        public override Task<RovidIkonyv> AddIktatasWithValasz(Ikonyv request, ServerCallContext context)
        {
            return base.AddIktatasWithValasz(request, context);
        }
        public override Task<Jelleg> AddJellegToTelephely(NewTorzsData request, ServerCallContext context)
        {
            return base.AddJellegToTelephely(request, context);
        }
        public override Task<Partner> AddPartnerToTelephely(NewTorzsData request, ServerCallContext context)
        {
            return base.AddPartnerToTelephely(request, context);
        }
        public override Task<PartnerUgyintezo> AddPartnerUgyintezoToPartner(NewTorzsData request, ServerCallContext context)
        {
            return base.AddPartnerUgyintezoToPartner(request, context);
        }
        public override Task<Telephely> AddTelephely(Telephely request, ServerCallContext context)
        {
            return base.AddTelephely(request, context);
        }
        public override Task<Ugyintezo> AddUgyintezoToTelephely(NewTorzsData request, ServerCallContext context)
        {
            return base.AddUgyintezoToTelephely(request, context);
        }
        public override Task<User> AddUser(User request, ServerCallContext context)
        {
            return base.AddUser(request, context);
        }
        public override Task<Answer> DisableUser(User request, ServerCallContext context)
        {
            return base.DisableUser(request, context);
        }
        public override Task GetAllUser(EmptyMessage request, IServerStreamWriter<User> responseStream, ServerCallContext context)
        {
            return base.GetAllUser(request, responseStream, context);
        }
        public override Task GetCsoportokByTelephely(Telephely request, IServerStreamWriter<Csoport> responseStream, ServerCallContext context)
        {
            return base.GetCsoportokByTelephely(request, responseStream, context);
        }
        public override Task GetDocumentInfoByIkonyv(Ikonyv request, IServerStreamWriter<DocumentInfo> responseStream, ServerCallContext context)
        {
            return base.GetDocumentInfoByIkonyv(request, responseStream, context);
        }
        public override Task GetIkonyvek(SearchIkonyvData request, IServerStreamWriter<Ikonyv> responseStream, ServerCallContext context)
        {
            return base.GetIkonyvek(request, responseStream, context);
        }
        public override Task GetJellegekByTelephely(Telephely request, IServerStreamWriter<Jelleg> responseStream, ServerCallContext context)
        {
            return base.GetJellegekByTelephely(request, responseStream, context);
        }
        public override Task GetPartnerekByTelephely(Telephely request, IServerStreamWriter<Partner> responseStream, ServerCallContext context)
        {
            return base.GetPartnerekByTelephely(request, responseStream, context);
        }
        public override Task GetPartnerUgyintezoByPartner(Partner request, IServerStreamWriter<PartnerUgyintezo> responseStream, ServerCallContext context)
        {
            return base.GetPartnerUgyintezoByPartner(request, responseStream, context);
        }
        public override Task GetPrivileges(EmptyMessage request, IServerStreamWriter<Privilege> responseStream, ServerCallContext context)
        {
            return base.GetPrivileges(request, responseStream, context);
        }
        public override Task GetShortIktSzamokByTelephely(Telephely request, IServerStreamWriter<RovidIkonyv> responseStream, ServerCallContext context)
        {
            return base.GetShortIktSzamokByTelephely(request, responseStream, context);
        }
        public override Task GetTelephelyek(EmptyMessage request, IServerStreamWriter<Telephely> responseStream, ServerCallContext context)
        {
            return base.GetTelephelyek(request, responseStream, context);
        }
        public override Task GetUgyintezokByTelephely(Telephely request, IServerStreamWriter<Ugyintezo> responseStream, ServerCallContext context)
        {
            return base.GetUgyintezokByTelephely(request, responseStream, context);
        }
        public override Task GetYears(EmptyMessage request, IServerStreamWriter<Year> responseStream, ServerCallContext context)
        {
            return base.GetYears(request, responseStream, context);
        }
        public override Task<Answer> ModifyCsoport(Csoport request, ServerCallContext context)
        {
            return base.ModifyCsoport(request, context);
        }
        public override Task<Answer> ModifyJelleg(Jelleg request, ServerCallContext context)
        {
            return base.ModifyJelleg(request, context);
        }
        public override Task<Answer> ModifyPartner(Partner request, ServerCallContext context)
        {
            return base.ModifyPartner(request, context);
        }
        public override Task<Answer> ModifyPartnerUgyintezo(PartnerUgyintezo request, ServerCallContext context)
        {
            return base.ModifyPartnerUgyintezo(request, context);
        }
        public override Task<Answer> ModifyTelephely(Telephely request, ServerCallContext context)
        {
            return base.ModifyTelephely(request, context);
        }
        public override Task<Answer> ModifyUgyintezo(Ugyintezo request, ServerCallContext context)
        {
            return base.ModifyUgyintezo(request, context);
        }
        public override Task<Answer> ModifyUser(User request, ServerCallContext context)
        {
            return base.ModifyUser(request, context);
        }
        public override Task<Answer> RemoveCsoport(Csoport request, ServerCallContext context)
        {
            return base.RemoveCsoport(request, context);
        }
        public override Task<Answer> RemoveIkonyv(Ikonyv request, ServerCallContext context)
        {
            return base.RemoveIkonyv(request, context);
        }
        public override Task<Answer> RemoveJelleg(Jelleg request, ServerCallContext context)
        {
            return base.RemoveJelleg(request, context);
        }
        public override Task<Answer> RemovePartner(Partner request, ServerCallContext context)
        {
            return base.RemovePartner(request, context);
        }
        public override Task<Answer> RemovePartnerUgyintezo(PartnerUgyintezo request, ServerCallContext context)
        {
            return base.RemovePartnerUgyintezo(request, context);
        }
        public override Task<Answer> RemoveTelephely(Telephely request, ServerCallContext context)
        {
            return base.RemoveTelephely(request, context);
        }
        public override Task<Answer> RemoveUgyintezoFromTelephely(Ugyintezo request, ServerCallContext context)
        {
            return base.RemoveUgyintezoFromTelephely(request, context);
        }
    }
}
