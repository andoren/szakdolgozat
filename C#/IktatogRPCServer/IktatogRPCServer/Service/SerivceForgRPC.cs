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
         static List<(string,DateTime)> InvalidTokens = new List<(string, DateTime)>();
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

        }
        public override Task<Answer> Logout(EmptyMessage request, ServerCallContext context)
        {
            try
            {
                InvalidTokens.Add((context.RequestHeaders[0].Value.ToString(),DateTime.Now));
                return Task.FromResult<Answer>(new Answer() { Error = false, Message = "Sikeres kijelentkezés." });

            }
            catch (Exception e)
            {
                return Task.FromException<Answer>(e);
            }
        }
        //TODO EZ KELL?
        public override Task<Answer> Register(User request, ServerCallContext context)
        {

            return base.Register(request, context);
        }
        public override Task<User> AddUser(User request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user) && user.Privilege.Name == "admin")
            {
                MysqlDatabaseManager<User> manager = new UserDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user)); ;
            }
            return Task.FromResult(new User());
        }
        public override Task<Answer> DisableUser(User request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<User> manager = new UserDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

       
        
        //TODO
        public override async Task<DocumentInfo> UploadDocument(Document request, ServerCallContext context)
        {
            return await Task.Run(() =>
            {

                return new DocumentInfo() { Id = 2, Name = request.Name, Size = (request.Doc.Length / (double)1024) / 1024, Type = request.Type };
            });

        }
        
        public override Task<RovidIkonyv> AddIktatas(Ikonyv request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ikonyv> manager = new IkonyvDatabaseManager(connectionManager);
                Ikonyv ikonyv = manager.Add(request, user);
                return Task.FromResult(new RovidIkonyv() { Id = ikonyv.Id, Iktatoszam = ikonyv.Iktatoszam });
            }
            return Task.FromResult(new RovidIkonyv());
        }
        
        public override Task<Csoport> AddCsoportToTelephely(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                Csoport csoport = new Csoport() { Name = request.Name, Shortname = request.Shorname };
                MysqlDatabaseManager<Csoport> manager = new CsoportDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request,user)); ;
            }
            return Task.FromResult(new Csoport());
        }
        
        public override Task<RovidIkonyv> AddIktatasWithValasz(Ikonyv request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ikonyv> manager = new IkonyvDatabaseManager(connectionManager);
                Ikonyv ikonyv = manager.Add(request, user);
                return Task.FromResult(new RovidIkonyv() {Id= ikonyv.Id, Iktatoszam = ikonyv.Iktatoszam }) ;
            }
            return Task.FromResult(new RovidIkonyv());
        }
        
        public override Task<Jelleg> AddJellegToTelephely(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Jelleg> manager = new JellegDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user)); ;
            }
            return Task.FromResult(new Jelleg());
        }
        
        public override Task<Partner> AddPartnerToTelephely(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Partner> manager = new PartnerDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user)); ;
            }
            return Task.FromResult(new Partner());
        }
        
        public override Task<PartnerUgyintezo> AddPartnerUgyintezoToPartner(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<PartnerUgyintezo> manager = new PartnerUgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user)); ;
            }
            return Task.FromResult(new PartnerUgyintezo());
        }
        
        public override Task<Telephely> AddTelephely(Telephely request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Telephely> manager = new TelephelyDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user)); ;
            }
            return Task.FromResult(new Telephely());
        }
        
        public override Task<Ugyintezo> AddUgyintezoToTelephely(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ugyintezo> manager = new UgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user)); ;
            }
            return Task.FromResult(new Ugyintezo());
        }

        //TODO Ez kell?
        public override Task ListallIktatas(EmptyMessage request, IServerStreamWriter<Ikonyv> responseStream, ServerCallContext context)
        {

            return base.ListallIktatas(request, responseStream, context);
        }
        public async override Task GetUserTelephelyei(User request, IServerStreamWriter<Telephely> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Telephely> mysqlDatabaseManager = new TelephelyDatabaseManager(connectionManager);

                List<Telephely> telephelyek = mysqlDatabaseManager.GetAllData(request);
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

        //TODO
        public override async Task<Document> GetDocumentById(DocumentInfo request, ServerCallContext context)
        {

            return await Task.Run(() =>
            {
                MysqlDatabaseManager<Document> databaseManager = new DocumentDatabaseManager(connectionManager);
                Document document = databaseManager.GetDataById(request.Id);
                return document;
            });
        }
        public override async Task ListIktatas(SearchIkonyvData request, IServerStreamWriter<Ikonyv> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ikonyv> mysqlDatabaseManager = new IkonyvDatabaseManager(connectionManager);

                List<Ikonyv> ikonyvek = mysqlDatabaseManager.GetAllData(user);
                foreach (var response in ikonyvek)
                {
                    await responseStream.WriteAsync(response);

                }
            }

        }

        public override async Task GetAllUser(EmptyMessage request, IServerStreamWriter<User> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
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
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
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
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
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
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
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
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
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
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Privilege> mysqlDatabaseManager = new PrivilegeDatabaseManager(connectionManager);

                List<Privilege> privileges = mysqlDatabaseManager.GetAllData();
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

            User user;
            if (CheckUserIsValid(context.RequestHeaders,out user))
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
            User user; 
            if (CheckUserIsValid(context.RequestHeaders, out user))
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
        {;
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
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
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Year> mysqlDatabaseManager = new YearsDatabaseManager(connectionManager);

                List<Year> evek = mysqlDatabaseManager.GetAllData();
                foreach (var response in evek)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                await responseStream.WriteAsync(new Year());
            }

        }

        public override Task<Answer> AddYearAndActivate(EmptyMessage request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Year> manager = new YearsDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(new Year() { Id = user.Id }));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }
        public override Task<Answer> ModifyCsoport(Csoport request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Csoport> manager = new CsoportDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> ModifyJelleg(Jelleg request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Jelleg> manager = new JellegDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> ModifyPartner(Partner request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Partner> manager = new PartnerDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> ModifyPartnerUgyintezo(PartnerUgyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<PartnerUgyintezo> manager = new PartnerUgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> ModifyTelephely(Telephely request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Telephely> manager = new TelephelyDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> ModifyUgyintezo(Ugyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ugyintezo> manager = new UgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> ModifyUser(User request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<User> manager = new UserDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> ModifyIktatas(Ikonyv request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ikonyv> manager = new IkonyvDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> DeleteIktatas(DeleteMessage request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                if (user.Privilege.Name == "admin")
                {
                    MysqlDatabaseManager<Ikonyv> manager = new IkonyvDatabaseManager(connectionManager);
                    return Task.FromResult(manager.Delete(request.Id, user));
                }
                else {
                    return Task.FromResult(new Answer() { Error=true,Message="Az iktatás törléséhez admin jogosultság szükséges!"});
                }
               
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> RemoveCsoport(Csoport request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Csoport> manager= new CsoportDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id,user));
            }
            else return Task.FromResult(new Answer() { Error = true , Message = "Hibás felhasználó!"}) ;
            
        }

        public override Task<Answer> RemoveIkonyv(Ikonyv request, ServerCallContext context)
        {
            return base.RemoveIkonyv(request, context);
        }

        public override Task<Answer> RemoveJelleg(Jelleg request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Jelleg> manager = new JellegDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> RemovePartner(Partner request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Partner> manager = new PartnerDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> RemovePartnerUgyintezo(PartnerUgyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<PartnerUgyintezo> manager = new PartnerUgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> RemoveTelephely(Telephely request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Telephely> manager = new TelephelyDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> RemoveUgyintezoFromTelephely(Ugyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ugyintezo> manager = new UgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }

        public override Task<Answer> Removedocument(DocumentInfo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Document> manager = new DocumentDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else return Task.FromResult(new Answer() { Error = true, Message = "Hibás felhasználó!" });
        }
        private bool CheckUserIsValid(Metadata header, out User user) {
            bool success = false;
            try
            {
                AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };
                if (TokenIsUsed(authToken))
                {
                    user = new User();
                    success  = false;
                }

                success = TokenManager.IsValidToken(authToken, out user);
            }
            catch (Exception e) {
                Logger.Logging.LogToScreenAndFile($"Hiba történt a user validálása közben. {e.Message}");
                user = new User();
            }
            
            return success;
        }
        private bool TokenIsUsed(AuthToken token) {
            bool used = false;
            foreach (var item in InvalidTokens)
            {
                if (item.Item2.AddDays(1) > DateTime.Now) {
                    InvalidTokens.Remove(item);
                    continue;
                } 
                if (item.Item1 == token.Token) used = true;
            }
            return used;
        }
    }
}
