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
    class SerivceForgRPC : IktatoService.IktatoServiceBase
    {
        readonly TokenSerivce TokenManager = new TokenSerivce();
        readonly ConnectionManager connectionManager = new ConnectionManager();
        public override Task<User> Login(LoginMessage request, ServerCallContext context)
        {
            UserDatabaseManager userManager = new UserDatabaseManager(new Database.ConnectionManager());
            User user;
            if (userManager.IsValidUser(request, out user))
            {

                user.AuthToken = new AuthToken() { Token = TokenManager.GenerateToken(user) };
                return Task.FromResult<User>(user);
            }
            else
            {
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

                return Task.FromResult<Answer>(new Answer() { Error = false, Message = "Sikeres kijelentkezés." });

            }
            catch (Exception e)
            {
                return Task.FromException<Answer>(e);
            }
        }
        public override Task<Answer> Register(User request, ServerCallContext context)
        {

            return base.Register(request, context);
        }
        public override async Task<RovidIkonyv> AddIktatas(Ikonyv request, ServerCallContext context)
        {
            return await Task.Run(() =>
            {

                return new RovidIkonyv() { Id = new Random().Next(1, 400), Iktatoszam = "Added SZám" };

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
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<Ikonyv> mysqlDatabaseManager = new IkonyvDatabaseManager(connectionManager);

                List<Ikonyv> ikonyvek = mysqlDatabaseManager.GetAllData(user);
                foreach (var response in ikonyvek)
                {
                    await responseStream.WriteAsync(response);

                }
            }      

        }
        public override async Task<Document> GetDocumentById(DocumentInfo request, ServerCallContext context)
        {

            return await Task.Run(() =>
            {
                MysqlDatabaseManager<Document> databaseManager = new DocumentDatabaseManager(connectionManager);
                Document document = databaseManager.GetDataById(request.Id);
                return document;
            });
        }
        public override async Task<DocumentInfo> UploadDocument(Document request, ServerCallContext context)
        {
            return await Task.Run(() =>
            {

                return new DocumentInfo() { Id = 2, Name = request.Name, Size = (request.Doc.Length / (double)1024) / 1024, Type = request.Type };
            });

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
        public override async Task GetAllUser(EmptyMessage request, IServerStreamWriter<User> responseStream, ServerCallContext context)
        {
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<User> mysqlDatabaseManager = new UserDatabaseManager(connectionManager);

                List<User> users = mysqlDatabaseManager.GetAllData(user);
                foreach (var response in users)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                await responseStream.WriteAsync(new User());
            }
        }
        public override async Task GetCsoportokByTelephely(Telephely request, IServerStreamWriter<Csoport> responseStream, ServerCallContext context)
        {
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<Csoport> mysqlDatabaseManager = new CsoportDatabaseManager(connectionManager);

                List<Csoport> users = mysqlDatabaseManager.GetAllData(request);
                foreach (var response in users)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                await responseStream.WriteAsync(new Csoport());
            }
        }
        public override Task GetDocumentInfoByIkonyv(Ikonyv request, IServerStreamWriter<DocumentInfo> responseStream, ServerCallContext context)
        {
            return base.GetDocumentInfoByIkonyv(request, responseStream, context);
        }
        public override Task GetIkonyvek(SearchIkonyvData request, IServerStreamWriter<Ikonyv> responseStream, ServerCallContext context)
        {
            return base.GetIkonyvek(request, responseStream, context);
        }
        public override async Task GetJellegekByTelephely(Telephely request, IServerStreamWriter<Jelleg> responseStream, ServerCallContext context)
        {
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<Jelleg> mysqlDatabaseManager = new JellegDatabaseManager(connectionManager);

                List<Jelleg> users = mysqlDatabaseManager.GetAllData(request);
                foreach (var response in users)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                await responseStream.WriteAsync(new Jelleg());
            }
        }
        public override async Task GetPartnerekByTelephely(Telephely request, IServerStreamWriter<Partner> responseStream, ServerCallContext context)
        {
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<Partner> mysqlDatabaseManager = new PartnerDatabaseManager(connectionManager);

                List<Partner> partnerek = mysqlDatabaseManager.GetAllData(request);
                foreach (var response in partnerek)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                await responseStream.WriteAsync(new Partner());
            }
        }
        public override async Task GetPartnerUgyintezoByPartner(Partner request, IServerStreamWriter<PartnerUgyintezo> responseStream, ServerCallContext context)
        {
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<PartnerUgyintezo> mysqlDatabaseManager = new PartnerUgyintezoDatabaseManager(connectionManager);

                List<PartnerUgyintezo> ugyintezok = mysqlDatabaseManager.GetAllData(request);
                foreach (var response in ugyintezok)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                await responseStream.WriteAsync(new PartnerUgyintezo());
            }
        }
        public override async Task GetPrivileges(EmptyMessage request, IServerStreamWriter<Privilege> responseStream, ServerCallContext context)
        {
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<Privilege> mysqlDatabaseManager = new PrivilegeDatabaseManager(connectionManager);

                List<Privilege> privileges = mysqlDatabaseManager.GetAllData(request);
                foreach (var response in privileges)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                await responseStream.WriteAsync(new Privilege());
            }
        }
        public override async Task GetShortIktSzamokByTelephely(Telephely request, IServerStreamWriter<RovidIkonyv> responseStream, ServerCallContext context)
        {
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<RovidIkonyv> mysqlDatabaseManager = new RovidIkonyvDatabaseManager(connectionManager);

                List<RovidIkonyv> privileges = mysqlDatabaseManager.GetAllData(request);
                foreach (var response in privileges)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                await responseStream.WriteAsync(new RovidIkonyv());
            }
        }
        public override async Task GetTelephelyek(EmptyMessage request, IServerStreamWriter<Telephely> responseStream, ServerCallContext context)
        {
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<Telephely> mysqlDatabaseManager = new TelephelyDatabaseManager(connectionManager);

                List<Telephely> telephelyek = mysqlDatabaseManager.GetAllData(user);
                foreach (var response in telephelyek)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                await responseStream.WriteAsync(new Telephely());
            }
        }
        public override async Task GetUgyintezokByTelephely(Telephely request, IServerStreamWriter<Ugyintezo> responseStream, ServerCallContext context)
        {
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<Ugyintezo> mysqlDatabaseManager = new UgyintezoDatabaseManager(connectionManager);

                List<Ugyintezo> telephelyek = mysqlDatabaseManager.GetAllData(request);
                foreach (var response in telephelyek)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                await responseStream.WriteAsync(new Ugyintezo());
            }
        }
        public override async Task GetYears(EmptyMessage request, IServerStreamWriter<Year> responseStream, ServerCallContext context)
        {
            Metadata header = context.RequestHeaders;
            User user;
            AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };

            if (TokenManager.IsValidToken(authToken, out user))
            {
                MysqlDatabaseManager<Year> mysqlDatabaseManager = new YearsDatabaseManager(connectionManager);

                List<Year> evek = mysqlDatabaseManager.GetAllData();
                foreach (var response in evek)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else {
              await responseStream.WriteAsync(new Year());
            }
           
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
        public override Task<Answer> Removedocument(DocumentInfo request, ServerCallContext context)
        {
            return Task.Run(() => { return new Answer() { Error = false, Message = "A törlés sikeres volt." }; });
        }
    }
}
