using Iktato;
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

        public PartnerUgyintezo AddPartnerUgyintezo(NewTorzsData newObject, User user)
        {
            return dbManager.AddPartnerUgyintezo(newObject, user);
        }

        public Answer DeletePartnerUgyintezo(int id, User user)
        {
            return dbManager.DeletePartnerUgyintezo(id, user);
        }

        public List<PartnerUgyintezo> GetPartnerUgyintezok(Partner filter)
        {
            return dbManager.GetPartnerUgyintezok(filter);
        }

        public Answer ModifyPartnerUgyintezo(PartnerUgyintezo modifiedObject)
        {
            return dbManager.ModifyPartnerUgyintezo(modifiedObject);
        }
    }
}
