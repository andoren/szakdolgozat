using Iktato;
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

        public Ugyintezo AddUgyintezo(NewTorzsData newObject, User user)
        {
            return dbManager.AddUgyintezo(newObject, user);
        }

        public Answer DeleteUgyintezo(int id, User user)
        {
            return dbManager.DeleteUgyintezo(id, user);
        }

        public List<Ugyintezo> GetUgyintezok(Telephely filter)
        {
            return dbManager.GetUgyintezok(filter);
        }

        public Answer ModifyUgyintezo(Ugyintezo modifiedObject)
        {
            return dbManager.ModifyUgyintezo(modifiedObject);
        }
    }
}
