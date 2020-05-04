using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class JellegService:IManageJelleg
    {
        IManageJelleg dbManager;
        public JellegService(IManageJelleg dbManager)
        {
            this.dbManager = dbManager;
        }
    }
}
