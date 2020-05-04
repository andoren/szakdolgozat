using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Interfaces
{
    interface IManageUser
    {
        List<User> GetallUser();
        Answer DisableUser(int id, User user);
        Answer ModifyUser(User modifiedObject);
        bool IsValidUser(LoginMessage request, out User user);
        User AddUser(NewTorzsData newObject, User user);
        void ModifyPassword(User modifiedObject);
        void AddUserToTelephely(User user, Telephely telephely);
    }
}
