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

        public override User Add(User newObject, User user)
        {
            User newUser = new User()
            {
                Username = newObject.Username,
                Fullname = newObject.Fullname,
                Privilege = newObject.Privilege,
                
            };
            foreach (var telephely in newObject.Telephelyek)
            {
                newUser.Telephelyek.Add(telephely);
            }
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "adduser";
            //IN PARAMETERS in username_b varchar(45),in password_b varchar(250), in fullname_b varchar(120), in privilege_b tinyint
            MySqlParameter usernamep = new MySqlParameter()
            {
                ParameterName = "@username_b",
                DbType = System.Data.DbType.String,
                Value = newUser.Username,
                Direction = System.Data.ParameterDirection.Input
            };

            MySqlParameter passwordp = new MySqlParameter()
            {
                ParameterName = "@password_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Password,
                Direction = System.Data.ParameterDirection.Input
            };

            MySqlParameter fullnamep = new MySqlParameter()
            {
                ParameterName = "@fullname_b",
                DbType = System.Data.DbType.String,
                Value = newUser.Fullname,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter privilege_b = new MySqlParameter()
            {
                ParameterName = "@privilege_b",
                DbType = System.Data.DbType.Int32,
                Value = newUser.Privilege.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            command.Parameters.Add(usernamep);
            command.Parameters.Add(passwordp);
            command.Parameters.Add(fullnamep);
            command.Parameters.Add(privilege_b);
            //OUTPARAMETER
            MySqlParameter newPartnerId = new MySqlParameter()
            {
                ParameterName = "newid_b",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            command.Parameters.Add(newPartnerId);
            try
            {
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    command.ExecuteNonQuery();
                    newUser.Id = int.Parse(command.Parameters["newid_b"].Value.ToString());
                    foreach (var telephely in newUser.Telephelyek)
                    {
                        AddUserToTelephely(newUser, telephely);
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
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Logging.LogToScreenAndFile(ex.Message);

            }
     
            return newUser;
        }
        private void AddUserToTelephely(User user,Telephely telephely) {
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addusertotelephely";
            //IN PARAMETERS 
            MySqlParameter useridp = new MySqlParameter()
            {
                ParameterName = "@user_id_b",
                DbType = System.Data.DbType.Int32,
                Value = user.Id,
                Direction = System.Data.ParameterDirection.Input
            };

            MySqlParameter telephelyp = new MySqlParameter()
            {
                ParameterName = "@telephely_b",
                DbType = System.Data.DbType.Int32,
                Value = telephely.Id,
                Direction = System.Data.ParameterDirection.Input
            };


            command.Parameters.Add(useridp);
            command.Parameters.Add(telephelyp);

            try
            {
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    command.ExecuteNonQuery();                   
                }
                catch (MySqlException ex)
                {
                    Logging.LogToScreenAndFile("Error code: " + ex.Code + " Error message: " + ex.Message);
                }
                catch (Exception e)
                {
                    Logging.LogToScreenAndFile(e.Message);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Logging.LogToScreenAndFile(ex.Message);

            }
        }

        public override Answer Delete(int id, User user)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "deluser";
            MySqlParameter idp = new MySqlParameter()
            {
                ParameterName = "@user_id_b",
                DbType = System.Data.DbType.Int32,
                Value = id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter deleterp = new MySqlParameter()
            {
                ParameterName = "@deleter_b",
                DbType = System.Data.DbType.Int32,
                Value = user.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            command.Parameters.Add(idp);
            command.Parameters.Add(deleterp);
            bool eredmeny = true;
            string message = "Sikeres törlés!";
            try
            {
                OpenConnection(connection);
                command.Connection = connection;
                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
            }
            catch (MySqlException ex)
            {
                Logging.LogToScreenAndFile("Error code: " + ex.Code + " Error Message: " + ex.Message);
            }
            catch (Exception e)
            {
                Logging.LogToScreenAndFile(e.Message);
            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }

        public override List<User> GetAllData()
        {
            throw new NotImplementedException();
        }
        public override User GetDataById(int id)
        {
            throw new NotImplementedException();
        }
        public override Answer Update(User modifiedObject)
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

        public override User Add(NewTorzsData newObject, User user)
        {
            throw new NotImplementedException();
        }
    }
}
