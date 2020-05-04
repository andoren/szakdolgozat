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
using IktatogRPCServer.Database.Services;
using IktatogRPCServer.Helpers;
using Serilog;
namespace IktatogRPCServer.Service
{
    class SerivceForgRPC : IktatoService.IktatoServiceBase
    {
        public SerivceForgRPC() : base()
        {

        }
        private UploadedFileHandler fileHandler = new UploadedFileHandler();
        private readonly TokenSerivce TokenManager = new TokenSerivce();
        private readonly ConnectionManager connectionManager = new ConnectionManager();
        private static List<(string, DateTime)> InvalidTokens = new List<(string, DateTime)>();

        public override Task<User> Login(LoginMessage request, ServerCallContext context)
        {
            UserService service = new UserService(new UserDatabaseManager());
            User user;
            if (service.IsValidUser(request, out user))
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
                UserService service = new UserService(new UserDatabaseManager());
                return Task.FromResult(service.AddUser(new NewTorzsData() { User = request }, user)); ;
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> DisableUser(User request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                UserService service = new UserService(new UserDatabaseManager());
                return Task.FromResult(service.DisableUser(request.Id, user));
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
                DocumentService service = new DocumentService(new DocumentDatabaseManager());
                recivedDocuemnt.Doc = ByteString.CopyFrom(Chunkes.ToArray().SelectMany(inner => inner).ToArray());
                Document document = service.AddDocument(recivedDocuemnt, user);
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
                IkonyvService service = new IkonyvService(new IkonyvDatabaseManager());
                Log.Debug("IkonyvDatabaseManager.Add: AddRootIkonyv meghívva.");
                return Task.FromResult (service.AddRootIkonyv(request, user));  
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Csoport> AddCsoportToTelephely(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                CsoportService service = new CsoportService(new CsoportDatabaseManager());
                return Task.FromResult(service.AddCsoport(request, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<RovidIkonyv> AddIktatasWithValasz(Ikonyv request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                IkonyvService service = new IkonyvService(new IkonyvDatabaseManager());
                return Task.FromResult(service.AddSubIkonyv(request,user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Jelleg> AddJellegToTelephely(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                JellegService service = new JellegService(new JellegDatabaseManager());
                return Task.FromResult(service.AddJelleg(request, user)); ;
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Partner> AddPartnerToTelephely(NewTorzsData request, ServerCallContext context)
        {

            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                PartnerService service = new PartnerService(new PartnerDatabaseManager());
                return Task.FromResult(service.AddPartner(request, user)); 
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));


        }
        public override Task<PartnerUgyintezo> AddPartnerUgyintezoToPartner(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                PartnerUgyintezoService service = new PartnerUgyintezoService(new PartnerUgyintezoDatabaseManager());
                return Task.FromResult(service.AddPartnerUgyintezo(request, user)); 
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Telephely> AddTelephely(Telephely request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                TelephelyService service = new TelephelyService(new TelephelyDatabaseManager());
                return Task.FromResult(service.AddTelephely(new NewTorzsData() { Telephely = request }, user)); 
            }
            throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Ugyintezo> AddUgyintezoToTelephely(NewTorzsData request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                UgyintezoService service = new UgyintezoService(new UgyintezoDatabaseManager());
                return Task.FromResult(service.AddUgyintezo(request, user)); 
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> AddYearAndActivate(EmptyMessage request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                EvService service = new EvService(new YearsDatabaseManager());
                return Task.FromResult(service.CloseOldYearAndActivateNewOne(new Year() { Id = user.Id }));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public async override Task GetUserTelephelyei(User request, IServerStreamWriter<Telephely> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                TelephelyService service = new TelephelyService(new TelephelyDatabaseManager());
                List<Telephely> telephelyek = service.GetTelephelyek(request);
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
                DocumentService service = new DocumentService(new DocumentDatabaseManager());
                Document document = service.GetDocumentByInfo(request);
                Document chunkDocument = new Document();
                chunkDocument.Id = 0;
                chunkDocument.IkonyvId = document.IkonyvId;
                chunkDocument.Name = document.Name;
                chunkDocument.Type = document.Type;
                byte[] byteToSend = document.Doc.ToArray();
                for (long i = 0; i < byteToSend.Length; i += fileHandler.chunkSize)
                {

                    if (i + fileHandler.chunkSize > document.Doc.Length)
                    {
                        chunkDocument.Doc = ByteString.CopyFrom(fileHandler.FromToByteArray(byteToSend, i, i + fileHandler.chunkSize - document.Doc.Length));
                       await responseStream.WriteAsync(chunkDocument);
                    }
                    else
                    {
                        chunkDocument.Doc = ByteString.CopyFrom(fileHandler.FromToByteArray(byteToSend, i, 0));
                         await responseStream.WriteAsync(chunkDocument);
                    }
                }
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override async Task ListIktatas(SearchIkonyvData request, IServerStreamWriter<Ikonyv> responseStream, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                IkonyvService service = new IkonyvService(new IkonyvDatabaseManager());
                request.User = user;
                List<Ikonyv> ikonyvek = service.GetIkonyvek(request);
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
                TelephelyService service = new TelephelyService(new TelephelyDatabaseManager());
                List<Telephely> telephelyek = service.GetTelephelyek();
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
            if (CheckUserIsValid(context.RequestHeaders, out user) && user.Privilege.Name == "admin")
            {
                UserService service = new UserService(new UserDatabaseManager());
                List<User> users = service.GetallUser();
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
                CsoportService service = new CsoportService(new CsoportDatabaseManager());

                List<Csoport> users = service.GetCsoportok(request);
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
                DocumentService service = new DocumentService(new DocumentDatabaseManager());

                List<DocumentInfo> infos = service.GetDocumentInfosByIkonyv(request);
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
                JellegService service = new JellegService(new JellegDatabaseManager());
                List<Jelleg> jellegek = service.GetJellegek(request);
                foreach (var response in jellegek)
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
                PartnerService service = new PartnerService(new PartnerDatabaseManager());
                List<Partner> partnerek = service.GetPartnerek(request);
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
                PartnerUgyintezoService service = new PartnerUgyintezoService(new PartnerUgyintezoDatabaseManager());

                List<PartnerUgyintezo> ugyintezok = service.GetPartnerUgyintezok(request);
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
                PrivilegeService service = new PrivilegeService(new PrivilegeDatabaseManager());
                List<Privilege> privileges = service.GetPrivileges();
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
                IkonyvService service = new IkonyvService(new IkonyvDatabaseManager());
                List<RovidIkonyv> rovidikonyvek = service.GetRovidIkonyvek(request);
                foreach (var response in rovidikonyvek)
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
                TelephelyService service = new TelephelyService(new TelephelyDatabaseManager());
                List<Telephely> telephelyek = service.GetTelephelyek(user);
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
                UgyintezoService service = new UgyintezoService(new UgyintezoDatabaseManager());
                List<Ugyintezo> telephelyek = service.GetUgyintezok(request);
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
                EvService service = new EvService(new YearsDatabaseManager());
                List<Year> evek = service.GetEvek();
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
                CsoportService service = new CsoportService(new CsoportDatabaseManager());
                
                return Task.FromResult(service.ModifyCsoport(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> ModifyJelleg(Jelleg request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                JellegService service = new JellegService(new JellegDatabaseManager());
                return Task.FromResult(service.ModifyJelleg(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> ModifyPartner(Partner request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                PartnerService service = new PartnerService(new PartnerDatabaseManager());
                return Task.FromResult(service.ModifyPartner(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> ModifyPartnerUgyintezo(PartnerUgyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                PartnerUgyintezoService service = new PartnerUgyintezoService(new PartnerUgyintezoDatabaseManager());
                return Task.FromResult(service.ModifyPartnerUgyintezo(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> ModifyTelephely(Telephely request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                TelephelyService service = new TelephelyService(new TelephelyDatabaseManager());
                return Task.FromResult(service.ModifyTelephely(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> ModifyUgyintezo(Ugyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                UgyintezoService service = new UgyintezoService(new UgyintezoDatabaseManager());
                return Task.FromResult(service.ModifyUgyintezo(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> ModifyUser(User request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                UserService service = new UserService(new UserDatabaseManager());
                return Task.FromResult(service.ModifyUser(request));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> ModifyIktatas(Ikonyv request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                IkonyvService service = new IkonyvService(new IkonyvDatabaseManager());
                return Task.FromResult(service.ModifyIkonyv(request));
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
                    IkonyvService service = new IkonyvService(new IkonyvDatabaseManager());
                    return Task.FromResult(service.DeleteIkonyv(request.Id, user));
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
                CsoportService service = new CsoportService(new CsoportDatabaseManager());
                return Task.FromResult(service.DeleteCsoport(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));

        }
        public override Task<Answer> RemoveIkonyv(Ikonyv request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user) && user.Privilege.Name == "admin")
            {
                IkonyvService service = new IkonyvService(new IkonyvDatabaseManager());
                return Task.FromResult(service.DeleteIkonyv(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> RemoveJelleg(Jelleg request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                JellegService service = new JellegService(new JellegDatabaseManager());
                return Task.FromResult(service.DeleteJelleg(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> RemovePartner(Partner request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                PartnerService service = new PartnerService(new PartnerDatabaseManager());
                return Task.FromResult(service.DeletePartner(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> RemovePartnerUgyintezo(PartnerUgyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                PartnerUgyintezoService service = new PartnerUgyintezoService(new PartnerUgyintezoDatabaseManager());
                return Task.FromResult(service.DeletePartnerUgyintezo(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> RemoveTelephely(Telephely request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                TelephelyService service = new TelephelyService(new TelephelyDatabaseManager());
                return Task.FromResult(service.DeleteTelephely(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> RemoveUgyintezoFromTelephely(Ugyintezo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                UgyintezoService service = new UgyintezoService(new UgyintezoDatabaseManager());
                return Task.FromResult(service.DeleteUgyintezo(request.Id, user));
            }
            else throw new RpcException(new Status(StatusCode.PermissionDenied, "Hibás felhasználó vagy lejárt időkorlát! Kérem jelentkezzen be újra!"));
        }
        public override Task<Answer> Removedocument(DocumentInfo request, ServerCallContext context)
        {
            User user;
            if (CheckUserIsValid(context.RequestHeaders, out user))
            {
                DocumentService service = new DocumentService(new DocumentDatabaseManager());

                return Task.FromResult(service.DeleteDocument(request.Id, user));
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
