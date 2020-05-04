using Iktato;
using IktatogRPCServer.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Services
{
    class UserService:IManageUser
    {
        IManageUser dbManager;
        public UserService(IManageUser dbManager)
        {
            this.dbManager = dbManager;
        }

        public User AddUser(NewTorzsData newObject, User user)
        {
            return dbManager.AddUser(newObject, user);
        }

        public void AddUserToTelephely(User user, Telephely telephely)
        {
            dbManager.AddUserToTelephely(user, telephely);
        }

        public Answer DisableUser(int id, User user)
        {
            return dbManager.DisableUser(id, user);
        }

        public List<User> GetallUser()
        {
            return dbManager.GetallUser();
        }

        public bool IsValidUser(LoginMessage request, out User user)
        {
            return dbManager.IsValidUser(request, out user);
        }

        public void ModifyPassword(User modifiedObject)
        {
            dbManager.ModifyPassword(modifiedObject);
        }

        public Answer ModifyUser(User modifiedObject)
        {
            return dbManager.ModifyUser(modifiedObject);
        }
    }
}
