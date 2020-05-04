using Iktato;
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

        public Telephely AddTelephely(NewTorzsData newObject, User user)
        {
            return dbManager.AddTelephely(newObject, user);
        }

        public Answer DeleteTelephely(int id, User user)
        {
            return dbManager.DeleteTelephely(id, user);
        }

        public List<Telephely> GetTelephelyek(User filter)
        {
            return dbManager.GetTelephelyek(filter);
        }

        public List<Telephely> GetTelephelyek()
        {
            return dbManager.GetTelephelyek();
        }

        public Answer ModifyTelephely(Telephely modifiedObject)
        {
            return dbManager.ModifyTelephely(modifiedObject);
        }
    }
}
