using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class UgyintezoService:IManageUgyintezo
    {
        IManageUgyintezo dbManager;
        public UgyintezoService(IManageUgyintezo dbManager)
        {
            this.dbManager = dbManager;
        }
    }
}
