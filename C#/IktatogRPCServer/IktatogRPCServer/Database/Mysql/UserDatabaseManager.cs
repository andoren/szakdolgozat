using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Mysql
{
    internal class UserDatabaseManager : MysqlDatabaseManager<User>
    {
        public UserDatabaseManager(ConnectionManager connection) : base(connection)
        {

        }

        public override bool Add(User newObjet)
        {
            throw new NotImplementedException();
        }


        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<User> GetAllData()
        {
            throw new NotImplementedException();
        }
        public override User GetDataById(int id)
        {
            throw new NotImplementedException();
        }
        public override bool Update(User modifiedObject)
        {
            throw new NotImplementedException();
        }
        public bool IsValidUser(LoginMessage request, out User user)
        {
            user = new User();
            if (request.Username == "misi")
            {
                user.Fullname = "Pekár Mihály";
                user.Id = 1;
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
