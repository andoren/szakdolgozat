using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using Serilog;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IktatogRPCServer.Helpers;

namespace IktatogRPCServer.Database.Mysql
{
    internal class UserDatabaseManager : MysqlDatabaseManager<User>
    {
        public UserDatabaseManager(ConnectionManager connection) : base(connection)
        {

        }


        private void AddUserToTelephely(User user, Telephely telephely)
        {
            Log.Debug("UserDatabaseManager.AddUserToTelephely: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addusertotelephely";
            Log.Debug("UserDatabaseManager.AddUserToTelephely: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. User: {User}, Telephely: {Telephely}", user,telephely);
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
                Log.Debug("UserDatabaseManager.AddUserToTelephely: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {

                    Log.Debug("UserDatabaseManager.AddUserToTelephely: MysqlCommand végrehajtása");
                    command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Log.Error("UserDatabaseManager.AddUserToTelephely: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("UserDatabaseManager.AddUserToTelephely: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("UserDatabaseManager.AddUserToTelephely: Hiba történt {Message}", ex);

            }
        }
        public override Answer Delete(int id, User user)
        {
            Log.Debug("UserDatabaseManager.Delete: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "deluser";
            Log.Debug("UserDatabaseManager.Delete: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {Id}, User: {User}", id, user);
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
                Log.Debug("UserDatabaseManager.Delete: MysqlConnection létrehozása és nyitása.");
                OpenConnection(connection);
                Log.Debug("UserDatabaseManager.Delete: MysqlCommand végrehajtása");
                command.Connection = connection;
                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
            }
            catch (MySqlException ex)
            {
                Log.Error("UserDatabaseManager.Delete: Adatbázis hiba. {Message}", ex);
            }
            catch (Exception e)
            {
                Log.Error("UserDatabaseManager.Delete: Hiba történt {Message}", e);

            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }
  

        public override Answer Update(User modifiedObject)
        {
            Log.Debug("UserDatabaseManager.Update: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "modifyuser";
            string message = "Hiba a felhasználó módosítása közben.";
            bool eredmeny = false;
            //IN PARAMETERS
            Log.Debug("UserDatabaseManager.Update: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {ModifiedObject}", modifiedObject);
            MySqlParameter usernamep = new MySqlParameter()
            {
                ParameterName = "@username_b",
                DbType = System.Data.DbType.String,
                Value = modifiedObject.Username,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter idp = new MySqlParameter()
            {
                ParameterName = "@id_b",
                DbType = System.Data.DbType.Int32,
                Value = modifiedObject.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter fullnamep = new MySqlParameter()
            {
                ParameterName = "@fullname_b",
                DbType = System.Data.DbType.String,
                Value = modifiedObject.Fullname,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter privilegep = new MySqlParameter()
            {
                ParameterName = "@privilege_b",
                DbType = System.Data.DbType.Int32,
                Value = modifiedObject.Privilege.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter telephelyekp = new MySqlParameter()
            {
                ParameterName = "@telephelyek_b",
                DbType = System.Data.DbType.String,
                Value = TelephelyekIdToString(modifiedObject.Telephelyek),
                Direction = System.Data.ParameterDirection.Input
            };
            command.Parameters.Add(usernamep);
            command.Parameters.Add(idp);
            command.Parameters.Add(fullnamep);
            command.Parameters.Add(privilegep);
            command.Parameters.Add(telephelyekp);
            try
            {

                Log.Debug("UserDatabaseManager.Update: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("UserDatabaseManager.Update: MysqlCommand végrehajtása");
                    eredmeny = command.ExecuteNonQuery() == 0;
                    if (!eredmeny) message = "Sikeres módosítás.";
                }
                catch (MySqlException ex)
                {
                    Log.Error("UserDatabaseManager.Update: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("UserDatabaseManager.Update: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("UserDatabaseManager.Update: Hiba történt {Message}", ex);

            }
            if (!string.IsNullOrWhiteSpace(modifiedObject.Password)) {
                ModifyPassword(modifiedObject);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }
        private void ModifyPassword(User user) {
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "modifyuserpassword";
            //IN PARAMETERS
            Log.Debug("UserDatabaseManager.ModifyPassword: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {ModifiedObject}", user);
            MySqlParameter usernamep = new MySqlParameter()
            {
                ParameterName = "@password_b",
                DbType = System.Data.DbType.String,
                Value = user.Password,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter idp = new MySqlParameter()
            {
                ParameterName = "@user_id_b",
                DbType = System.Data.DbType.Int32,
                Value = user.Id,
                Direction = System.Data.ParameterDirection.Input
            };

            command.Parameters.Add(usernamep);
            command.Parameters.Add(idp);   
            try
            {

                Log.Debug("UserDatabaseManager.ModifyPassword: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("UserDatabaseManager.ModifyPassword: MysqlCommand végrehajtása");
                    command.ExecuteNonQuery();
                    
                }
                catch (MySqlException ex)
                {
                    Log.Error("UserDatabaseManager.ModifyPassword: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("UserDatabaseManager.ModifyPassword: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("UserDatabaseManager.ModifyPassword: Hiba történt {Message}", ex);

            }
        }
        /// <summary>
        /// A bevitt telephelyek id-jét kiszedi és egy mysql-ben olvashato stringgé módosítája
        /// </summary>
        /// <param name="telephelyek">A felhasználó telephelyei</param>
        /// <returns></returns>
        private string TelephelyekIdToString(IEnumerable<Telephely> telephelyek)
        {
            string data = "";
            for (int i = 0; i < telephelyek.Count(); i++)
            {
                data += telephelyek.ElementAt(i).Id;
                if (i < telephelyek.Count() - 1) data += ",";
            }
            return data;
        }
        public bool IsValidUser(LoginMessage request, out User user)
        {
            Log.Debug("UserDatabaseManager.IsValidUser: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "UserLogin";
            Log.Debug("UserDatabaseManager.IsValidUser: MysqlConnection létrehozása és nyitása.");
            MySqlConnection connection = GetConnection();
            command.Connection = connection;
            OpenConnection(connection);
            bool eredmeny = false;
            user = new User();
            try
            {
                Log.Debug("UserDatabaseManager.IsValidUser: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {Username}", user.Username);
                MySqlParameter usernamep = new MySqlParameter()
                {
                    ParameterName = "@username_b",
                    DbType = System.Data.DbType.String,
                    Value = request.Username,
                    Direction = System.Data.ParameterDirection.Input
                };
                MySqlParameter passwordp = new MySqlParameter()
                {
                    ParameterName = "@password_b",
                    DbType = System.Data.DbType.String,
                    Value = request.Password,
                    Direction = System.Data.ParameterDirection.Input
                };
                command.Parameters.Add(usernamep);
                command.Parameters.Add(passwordp);
                Log.Debug("UserDatabaseManager.IsValidUser: MysqlCommand végrehajtása");
                MySqlDataReader reader = command.ExecuteReader();
                eredmeny = reader.HasRows;
                if (eredmeny)
                {
                    Log.Debug("UserDatabaseManager.IsValidUser: Sikeres bejelentkezés.");
                    reader.Read();
                    user.Id = int.Parse(reader["id"].ToString());
                    user.Username = reader["username"].ToString();
                    user.Fullname = reader["FullName"].ToString();
                    user.Privilege = new Privilege() { Id = int.Parse(reader["privilegeid"].ToString()), Name = reader["privilegename"].ToString() };

                }

            }
            catch (MySqlException ex)
            {
                Log.Error("UserDatabaseManager.IsValidUser: Adatbázis hiba. {Message}", ex);
            }
            catch (Exception e)
            {
                Log.Error("UserDatabaseManager.IsValidUser: Hiba történt {Message}", e);

            }
            finally
            {
                CloseConnection(connection);

            }
            return eredmeny;
        }

        public override List<User> GetAllData(object filter)
        {
            Log.Debug("PartnerDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<User> userek = new List<User>();
            if (filter is User)
            {
                User user = filter as User;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getusers";
                try
                {
                    Log.Debug("PartnerDatabaseManager.GetAllData: MysqlConnection létrehozása és nyitása.");
                    MySqlConnection connection = GetConnection();
                    command.Connection = connection;
                    OpenConnection(connection);
                    try
                    {
                        Log.Debug("PartnerDatabaseManager.GetAllData: MysqlCommand végrehajtása");
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            User newuser = new User();
                            newuser.Id = int.Parse(reader["id"].ToString());
                            newuser.Username = reader["username"].ToString();
                            newuser.Fullname = reader["fullname"].ToString();
                            newuser.Privilege = new Privilege() { Id = int.Parse(reader["privilegeid"].ToString()), Name = reader["privilege"].ToString() };
                            userek.Add(newuser);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Log.Error("UserDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                    }
                    catch (Exception e)
                    {
                        Log.Error("UserDatabaseManager.GetAllData: Hiba történt {Message}", e);

                    }
                    finally
                    {
                        CloseConnection(connection);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("UserDatabaseManager.GetAllData: Hiba történt {Message}", ex);
                }
            }

            return userek;
        }
   
        public override User Add(NewTorzsData newObject, User user)
        {
            Log.Debug("UserDatabaseManager.Add: Mysqlcommand előkészítése.");
            User newUser = new User()
            {
                Username = newObject.User.Username,
                Fullname = newObject.User.Fullname,
                Privilege = newObject.User.Privilege,

            };
            foreach (var telephely in newObject.User.Telephelyek)
            {
                newUser.Telephelyek.Add(telephely);
            }
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "adduser";
            //IN PARAMETERS 
            Log.Debug("UserDatabaseManager.Add: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {NewObject}", newObject);
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
                Value = newObject.User.Password,
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
            //OUT PARAMETER
            Log.Debug("UserDatabaseManager.Add: Kimenő paraméter létrehozása és hozzáadása a paraméter listához.");
            MySqlParameter newPartnerId = new MySqlParameter()
            {
                ParameterName = "newid_b",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            command.Parameters.Add(newPartnerId);
            try
            {
                Log.Debug("UserDatabaseManager.Add: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("UserDatabaseManager.Add: MysqlCommand végrehajtása");
                    command.ExecuteNonQuery();
                    newUser.Id = int.Parse(command.Parameters["newid_b"].Value.ToString());
                    Log.Debug("UserDatabaseManager.Add: Kimenő paraméter kiolvasása {Id}", newUser.Id);
                    Log.Debug("UserDatabaseManager.Add: A felhasználó telephelyhez való csatolása.");
                    foreach (var telephely in newUser.Telephelyek)
                    {
                        AddUserToTelephely(newUser, telephely);
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Error("UserDatabaseManager.Add: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("UserDatabaseManager.Add: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("UserDatabaseManager.Add: Hiba történt {Message}", ex);

            }
            Log.Debug("Új felhasználó hozzáadva: {Username}. {Username} felhasználó által", newUser, user);
            return newUser;
        }
    }
}
