using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class IkonyvService:IManageIkonyv
    {
        IManageIkonyv dbManager;
        public IkonyvService(IManageIkonyv dbManager)
        {
            this.dbManager = dbManager;
        }
    }
}
