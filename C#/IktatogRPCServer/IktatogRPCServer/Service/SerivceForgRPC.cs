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
                user.Authtoken = new AuthToken() { Token = TokenManager.GenerateToken(user) };
                return Task.FromResult<User>(user);
            }
            else {
                return Task.FromResult<User>(new User());
            }

            //if (context.RequestHeaders.Select(x=>x.Value == "Kiscica") != null) {
            //    return Task.FromResult<User>(new User() { Id = 1, Username = "Misi", Fullname = "Pekár Mihály",, Privilege = new Privilege() { Id = 1, Name = "Admin" } });
            //} 

        }
        public override Task<Answer> Logout(AuthToken request, ServerCallContext context)
        {
            return base.Logout(request, context);
        }
        public override Task<Answer> Register(AuthToken request, ServerCallContext context)
        {
          
            return base.Register(request, context);
        }
    }
}
