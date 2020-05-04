using Iktato;
using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class CsoportService:IManageCsoport
    {
        IManageCsoport dbManager;
        public CsoportService(IManageCsoport dbManager)
        {
            this.dbManager = dbManager;
        }

        public Csoport AddCsoport(NewTorzsData data, User user)
        {
            return dbManager.AddCsoport(data, user);
        }

        public Answer DeleteCsoport(int id, User user)
        {
            return dbManager.DeleteCsoport(id, user);
        }

        public List<Csoport> GetCsoportok(Telephely filter)
        {
            return dbManager.GetCsoportok(filter);
        }

        public Answer ModifyCsoport(Csoport modifiedObject)
        {
            return dbManager.ModifyCsoport(modifiedObject);
        }
    }
}
