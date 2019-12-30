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
        public override Task<User> Login(LoginMessage request, ServerCallContext context)
        {
            return Task.FromResult<User>(new User() { Id = 1, Username ="Misi", Fullname= "Pekár Mihály", Privilege = new Privilege() { Id = 1, Name = "Admin"} }) ;
        }
        public override Task<Answer> Logout(User request, ServerCallContext context)
        {
            return base.Logout(request, context);
        }
        public override Task<Answer> Register(User request, ServerCallContext context)
        {
          
            return base.Register(request, context);
        }
    }
}
