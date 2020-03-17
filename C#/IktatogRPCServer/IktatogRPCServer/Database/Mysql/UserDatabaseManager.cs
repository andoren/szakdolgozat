using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using IktatogRPCServer.Logger;
using MySql.Data.MySqlClient;
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

        public override User Add(User newObjet)
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
                user.Username = request.Username;
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

        public override List<User> GetAllData(object filter)
        {
            List<User> userek = new List<User>();
            if (filter is User)
            {
                User user = filter as User;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getusers";
                command.Parameters.AddWithValue("@user_b", user.Id);
                try
                {
                    MySqlConnection connection = GetConnection();
                    command.Connection = connection;
                    OpenConnection(connection);
                    try
                    {
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            User newuser = new User();
                            newuser.Id = int.Parse(reader["id"].ToString());
                            newuser.Username = reader["username"].ToString();
                            newuser.Fullname = reader["fullname"].ToString();
                            userek.Add(newuser);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Logging.LogToScreenAndFile("Error code: " + ex.Code + " Error message: " + ex.Message);
                    }
                    catch (Exception e)
                    {
                        Logging.LogToScreenAndFile(e.Message);
                    }
                    finally
                    {
                        CloseConnection(connection);
                    }
                }
                catch (Exception ex)
                {
                    Logging.LogToScreenAndFile(ex.Message);
                }
            }

            return userek;
        }
    }
}
