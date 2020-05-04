using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class TelephelyService:IManageTelephely
    {
        IManageTelephely dbManager;
        public TelephelyService(IManageTelephely dbManager)
        {
            this.dbManager = dbManager;
        }
    }
}
