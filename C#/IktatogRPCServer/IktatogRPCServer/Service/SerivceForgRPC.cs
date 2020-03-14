using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Iktato;
using IktatogRPCServer.Database.Mysql;

namespace IktatogRPCServer.Service
{
    class SerivceForgRPC:IktatoService.IktatoServiceBase
    {
        readonly TokenSerivce TokenManager = new TokenSerivce();
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
        public override Task<Answer> AddIktatas(Ikonyv request, ServerCallContext context)
        {
            return base.AddIktatas(request, context);
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
    }
}
