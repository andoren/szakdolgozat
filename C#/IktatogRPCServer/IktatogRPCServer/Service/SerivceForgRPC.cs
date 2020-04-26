using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using Iktato;
using IktatogRPCServer.Database;
using IktatogRPCServer.Database.Mysql;
using IktatogRPCServer.Database.Mysql.Abstract;
using MySql.Data.MySqlClient;
using Serilog;
namespace IktatogRPCServer.Service
{
    class SerivceForgRPC : IktatoService.IktatoServiceBase
    {
        public SerivceForgRPC() : base()
        {

        }

        private readonly TokenSerivce TokenManager = new TokenSerivce();
        private readonly ConnectionManager connectionManager = new ConnectionManager();
        private static List<(string, DateTime)> InvalidTokens = new List<(string, DateTime)>();
        private int chunkSize = 64 * 1024;
        public override Task<User> Login(LoginMessage request, ServerCallContext context)
        {
            UserDatabaseManager userManager = new UserDatabaseManager(new Database.ConnectionManager());
            User user;
            if (userManager.IsValidUser(request, out user))
            {
                Log.Debug("Login: Sikeres user validálás. {Username}", user);
                Log.Debug("Login: Token generálás megkezdése a következő adatokkal: Felhasználónév: {Username}, Privilege: {Name}, Id: {Id}", user, user.Privilege, user);
                user.AuthToken = new AuthToken() { Token = TokenManager.GenerateToken(user) };
                Log.Debug("Login: A token sikeresen legenerálva.");
                return Task.FromResult<User>(user);
            }
            else
            {
                Log.Debug("Login: Sikertelen user validálás");
                Status s = new Status(StatusCode.Unauthenticated, "Hibás felhasználónév vagy jelszó!");
                return Task.FromException<User>(new RpcException(s));
            }

        }
        public override Task<Answer> Logout(EmptyMessage request, ServerCallContext context)
        {
            try
            {
                InvalidTokens.Add((context.RequestHeaders[0].Value.ToString(), DateTime.Now));
                return Task.FromResult<Answer>(new Answer() { Error = false, Message = "Sikeres kijelentkezés." });

            }
            catch (Exception e)
            {
                return Task.FromException<Answer>(e);
            }
        }
        public override Task<User> AddUser(User request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user) && user.Privilege.Name == "admin")
            {
                MysqlDatabaseManager<User> manager = new UserDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(new NewTorzsData() { User = request }, user)); ;
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> DisableUser(User request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<User> manager = new UserDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override async Task<DocumentInfo> UploadDocument(IAsyncStreamReader<Document> requestStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                List<byte[]> Chunkes = new List<byte[]>();
                Document recivedDocuemnt = new Document();
                while (await requestStream.MoveNext())
                {
                    recivedDocuemnt = requestStream.Current;
                    Chunkes.Add(requestStream.Current.Doc.ToArray());
                }
                MysqlDatabaseManager<Document> manager = new DocumentDatabaseManager(connectionManager);

                recivedDocuemnt.Doc = ByteString.CopyFrom(Chunkes.ToArray().SelectMany(inner => inner).ToArray());
                Document document = manager.Add(recivedDocuemnt, user);
                return new DocumentInfo()
                {
                    Id = document.Id,
                    Name = document.Name,
                    Path = document.Path,
                    Size = ((document.Doc.Length / (double)1024) / 1024),
                    Type = document.Type
                };
            }
            else
            {
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
            }

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
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Csoport> AddCsoportToTelephely(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                Csoport csoport = new Csoport() { Name = request.Name, Shortname = request.Shorname };
                MysqlDatabaseManager<Csoport> manager = new CsoportDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user));
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<RovidIkonyv> AddIktatasWithValasz(Ikonyv request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ikonyv> manager = new IkonyvDatabaseManager(connectionManager);
                Ikonyv ikonyv = manager.Add(request, user);
                return Task.FromResult(new RovidIkonyv() { Id = ikonyv.Id, Iktatoszam = ikonyv.Iktatoszam });
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Jelleg> AddJellegToTelephely(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Jelleg> manager = new JellegDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user)); ;
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Partner> AddPartnerToTelephely(NewTorzsData request, ServerCallContext context)
        {

            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Partner> manager = new PartnerDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user)); ;
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));


        }
        public override Task<PartnerUgyintezo> AddPartnerUgyintezoToPartner(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<PartnerUgyintezo> manager = new PartnerUgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user)); ;
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Telephely> AddTelephely(Telephely request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Telephely> manager = new TelephelyDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(new NewTorzsData() { Telephely = request }, user)); ;
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Ugyintezo> AddUgyintezoToTelephely(NewTorzsData request, ServerCallContext context)
        {

            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ugyintezo> manager = new UgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Add(request, user)); ;
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));


        }
        public override Task<Answer> AddYearAndActivate(EmptyMessage request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Year> manager = new YearsDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(new Year() { Id = user.Id }));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
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
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
            }
        }
        public override async Task GetDocumentById(DocumentInfo request, IServerStreamWriter<Document> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                DocumentDatabaseManager databaseManager = new DocumentDatabaseManager(connectionManager);
                Document document = databaseManager.GetDataById(request);
                Document chunkDocument = new Document();
                chunkDocument.Id = 0;
                chunkDocument.IkonyvId = document.IkonyvId;
                chunkDocument.Name = document.Name;
                chunkDocument.Type = document.Type;
                byte[] byteToSend = document.Doc.ToArray();
                for (long i = 0; i < byteToSend.Length; i += chunkSize)
                {

                    if (i + chunkSize > document.Doc.Length)
                    {
                        chunkDocument.Doc = ByteString.CopyFrom(FromToByteArray(byteToSend, i, i + chunkSize - document.Doc.Length));
                        await responseStream.WriteAsync(chunkDocument);
                    }
                    else
                    {
                        chunkDocument.Doc = ByteString.CopyFrom(FromToByteArray(byteToSend, i, 0));
                        await responseStream.WriteAsync(chunkDocument);
                    }
                }
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        private byte[] FromToByteArray(byte[] input, long from, long size)
        {
            byte[] output;
            if (size == 0) output = new byte[chunkSize];

            else
            {
                output = new byte[chunkSize - size];
            }
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = input[from + i];
            }
            return output;
        }
        public override async Task ListIktatas(SearchIkonyvData request, IServerStreamWriter<Ikonyv> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ikonyv> mysqlDatabaseManager = new IkonyvDatabaseManager(connectionManager);
                request.User = user;
                List<Ikonyv> ikonyvek = mysqlDatabaseManager.GetAllData(request);
                foreach (var response in ikonyvek)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));

        }
        public override async Task GetAllTelephely(EmptyMessage request, IServerStreamWriter<Telephely> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Telephely> mysqlDatabaseManager = new TelephelyDatabaseManager(connectionManager);

                List<Telephely> telephelyek = mysqlDatabaseManager.GetAllData(new Object());
                foreach (var response in telephelyek)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
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
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
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
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
            }
        }
        public override async Task GetDocumentInfoByIkonyv(Ikonyv request, IServerStreamWriter<DocumentInfo> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                DocumentDatabaseManager mysqlDatabaseManager = new DocumentDatabaseManager(connectionManager);

                List<DocumentInfo> infos = mysqlDatabaseManager.GetDocumentInfosByIkonyv(request);
                foreach (var response in infos)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
            }
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
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
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
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
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
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
            }
        }
        public override async Task GetPrivileges(EmptyMessage request, IServerStreamWriter<Privilege> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Privilege> mysqlDatabaseManager = new PrivilegeDatabaseManager(connectionManager);

                List<Privilege> privileges = mysqlDatabaseManager.GetAllData(new object());
                foreach (var response in privileges)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
            }
        }
        public override async Task GetShortIktSzamokByTelephely(Telephely request, IServerStreamWriter<RovidIkonyv> responseStream, ServerCallContext context)
        {

            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
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
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
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
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
            }
        }
        public override async Task GetUgyintezokByTelephely(Telephely request, IServerStreamWriter<Ugyintezo> responseStream, ServerCallContext context)
        {
            ;
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
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
            }
        }
        public override async Task GetYears(EmptyMessage request, IServerStreamWriter<Year> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Year> mysqlDatabaseManager = new YearsDatabaseManager(connectionManager);

                List<Year> evek = mysqlDatabaseManager.GetAllData(new object());
                foreach (var response in evek)
                {
                    await responseStream.WriteAsync(response);

                }
            }
            else
            {
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
            }

        }
        public override Task<Answer> ModifyCsoport(Csoport request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Csoport> manager = new CsoportDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> ModifyJelleg(Jelleg request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Jelleg> manager = new JellegDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> ModifyPartner(Partner request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Partner> manager = new PartnerDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> ModifyPartnerUgyintezo(PartnerUgyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<PartnerUgyintezo> manager = new PartnerUgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> ModifyTelephely(Telephely request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Telephely> manager = new TelephelyDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> ModifyUgyintezo(Ugyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ugyintezo> manager = new UgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> ModifyUser(User request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<User> manager = new UserDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> ModifyIktatas(Ikonyv request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ikonyv> manager = new IkonyvDatabaseManager(connectionManager);
                return Task.FromResult(manager.Update(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
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
                else
                {
                    return Task.FromResult(new Answer() { Error = true, Message = "Az iktatás törléséhez admin jogosultság szükséges!" });
                }

            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> RemoveCsoport(Csoport request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Csoport> manager = new CsoportDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));

        }

        public override Task<Answer> RemoveIkonyv(Ikonyv request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user) && user.Privilege.Name == "admin")
            {
                MysqlDatabaseManager<Ikonyv> manager = new IkonyvDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> RemoveJelleg(Jelleg request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Jelleg> manager = new JellegDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> RemovePartner(Partner request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Partner> manager = new PartnerDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> RemovePartnerUgyintezo(PartnerUgyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<PartnerUgyintezo> manager = new PartnerUgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> RemoveTelephely(Telephely request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Telephely> manager = new TelephelyDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> RemoveUgyintezoFromTelephely(Ugyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Ugyintezo> manager = new UgyintezoDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }

        public override Task<Answer> Removedocument(DocumentInfo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                MysqlDatabaseManager<Document> manager = new DocumentDatabaseManager(connectionManager);
                return Task.FromResult(manager.Delete(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        private bool CheckUserIsValid(Metadata header, out User user)
        {
            bool success = false;
            try
            {
                Log.Debug("CheckUserIsValid: Token kiolvasása a headerből.");
                AuthToken authToken = new AuthToken() { Token = header[0].Value.ToString() };
                Log.Debug("CheckUserIsValid: Sikeres kiolvasás");    
                Log.Debug("CheckUserIsValid: Token validálás megkezdése.");
                success = TokenManager.IsValidToken(authToken, out user);
                Log.Debug("CheckUserIsValid: A token: {Success}", success);
            }

            catch (Exception e)
            {
                Log.Warning("Hiba történt a user validálása közben. {Message}", e);
                user = new User();
            }

            return success;
        }

    }
}
