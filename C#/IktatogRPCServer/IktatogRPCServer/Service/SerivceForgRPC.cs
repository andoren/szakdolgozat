using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Iktato;
namespace IktatogRPCServer.Service
{
    class SerivceForgRPC:IktatoService.IktatoServiceBase
    {
        readonly TokenSerivce TokenManager = new TokenSerivce();
        public override Task<User> Login(LoginMessage request, ServerCallContext context)
        {
            UserDatabaseManager userManager = new UserDatabaseManager();
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
        public override Task ListIktatas(FromTo request, IServerStreamWriter<Ikonyv> responseStream, ServerCallContext context)
        {
            return base.ListIktatas(request, responseStream, context);
        }
    }
}
