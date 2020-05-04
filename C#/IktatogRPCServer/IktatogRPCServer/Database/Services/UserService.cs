using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class UserService:IManageUser
    {
        IManageUser dbManager;
        public UserService(IManageUser dbManager)
        {
            this.dbManager = dbManager;
        }
    }
}
