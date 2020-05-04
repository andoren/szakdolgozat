using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class PartnerUgyintezoService:IManagePartnerUgyintezo
    {
        IManagePartnerUgyintezo dbManager;
        public PartnerUgyintezoService(IManagePartnerUgyintezo dbManager)
        {
            this.dbManager = dbManager;
        }
    }
}
