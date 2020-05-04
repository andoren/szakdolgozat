﻿using Iktato;
using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class IkonyvService:IManageIkonyv
    {
        IManageIkonyv dbManager;
        public IkonyvService(IManageIkonyv dbManager)
        {
            this.dbManager = dbManager;
        }

        public RovidIkonyv AddRootIkonyv(Ikonyv newObject, User user)
        {
            return dbManager.AddRootIkonyv(newObject, user);
        }

        public RovidIkonyv AddSubIkonyv(Ikonyv newObject, User user)
        {
            return dbManager.AddSubIkonyv(newObject, user);
        }

        public Answer DeleteIkonyv(int id, User user)
        {
            return dbManager.DeleteIkonyv(id, user);
        }

        public List<Ikonyv> GetIkonyvek(SearchIkonyvData filter)
        {
            return dbManager.GetIkonyvek(filter);
        }

        public List<RovidIkonyv> GetRovidIkonyvek(Telephely filter)
        {
            return dbManager.GetRovidIkonyvek(filter);
        }

        public Answer ModifyIkonyv(Ikonyv modifiedObject)
        {
            return dbManager.ModifyIkonyv(modifiedObject);
        }
    }
}
