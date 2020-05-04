using Iktato;
using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class PartnerService:IManagePartner
    {
        IManagePartner dbManager;
        public PartnerService(IManagePartner dbManager)
        {
            this.dbManager = dbManager;
        }

        public Partner AddPartner(NewTorzsData newObject, User user)
        {
            return dbManager.AddPartner(newObject, user);
        }

        public Answer DeletePartner(int id, User user)
        {
            return dbManager.DeletePartner(id, user);
        }

        public List<Partner> GetPartnerek(Telephely filter)
        {
            return dbManager.GetPartnerek(filter);
        }

        public Answer ModifyPartner(Partner modifiedObject)
        {
            return dbManager.ModifyPartner(modifiedObject);
        }
    }
}
