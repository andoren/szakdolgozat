using Iktato;
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

        public Jelleg AddJelleg(NewTorzsData newObject, User user)
        {
            return dbManager.AddJelleg(newObject, user);
        }

        public Answer DeleteJelleg(int id, User user)
        {
            return dbManager.DeleteJelleg(id, user);
        }

        public List<Jelleg> GetJellegek(Telephely filter)
        {
            return dbManager.GetJellegek(filter);
        }

        public Answer ModifyJelleg(Jelleg modifiedObject)
        {
            return dbManager.ModifyJelleg(modifiedObject);
        }
    }
}
