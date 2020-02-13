using Iktato;
using System;
using System.Threading;

namespace IktatogRPCServer.Service
{
    internal class UserDatabaseManager
    {
        public UserDatabaseManager()
        {

        }

        internal bool IsValidUser(LoginMessage request, out User user)
        {
            Thread.Sleep(100);
            user = new User();
            if (request.Username == "misi")
            {
                user.Fullname = "Pekár Mihály";
                user.Id = 1;
                user.Username = request.Username;
                user.Privilege = new Privilege() { Id = 1, Name = "Admin" };
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}