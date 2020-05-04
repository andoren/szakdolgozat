using Iktato;
using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class EvService:IManageEv
    {
        IManageEv dbManager;
        public EvService(IManageEv dbManager)
        {
            this.dbManager = dbManager;
        }

        public Answer CloseOldYearAndActivateNewOne(Year modifiedObject)
        {
            return dbManager.CloseOldYearAndActivateNewOne(modifiedObject);
        }

        public List<Year> GetEvek()
        {
            return dbManager.GetEvek();
        }
    }
}
