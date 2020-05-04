using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using Serilog;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IktatogRPCServer.Database.Interfaces;

namespace IktatogRPCServer.Database.Mysql
{
    class TelephelyDatabaseManager : MysqlDatabaseManager, IManageTelephely
    {


        public Telephely AddTelephely(NewTorzsData newObject, User user)
        {
            Log.Debug("TelephelyDatabaseManager.Add: Mysqlcommand előkészítése.");
            Telephely telephely = new Telephely()
            {
                Name = newObject.Name,

            };
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addtelephely";
            //IN PARAMETERS
            Log.Debug("TelephelyDatabaseManager.Add: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {NewObject}", newObject);
            MySqlParameter namep = new MySqlParameter()
            {
                ParameterName = "@name_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Telephely.Name,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter userp = new MySqlParameter()
            {
                ParameterName = "@created_by_b",
                DbType = System.Data.DbType.Int32,
                Value = user.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            command.Parameters.Add(namep);
            command.Parameters.Add(userp);
            //OUT PARAMETER
            Log.Debug("TelephelyDatabaseManager.Add: Kimenő paraméter létrehozása és hozzáadása a paraméter listához.");
            MySqlParameter newPartnerId = new MySqlParameter()
            {
                ParameterName = "@newid_b",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(newPartnerId);
            try
            {
                Log.Debug("TelephelyDatabaseManager.Add: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("TelephelyDatabaseManager.Add: MysqlCommand végrehajtása");
                    command.ExecuteNonQuery();
                    telephely.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
                    Log.Debug("TelephelyDatabaseManager.Add: Kimenő paraméter kiolvasása {Id}", telephely.Id);
                }
                catch (MySqlException ex)
                {
                    Log.Error("TelephelyDatabaseManager.Add: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("TelephelyDatabaseManager.Add: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("TelephelyDatabaseManager.Add: Hiba történt {Message}", ex);

            }

            return telephely;
        }

        public Answer DeleteTelephely(int id, User user)
        {
            Log.Debug("TelephelyDatabaseManager.Delete: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "deltelephely";
            Log.Debug("TelephelyDatabaseManager.Delete: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {Id}, User: {User}", id, user);
            MySqlParameter idp = new MySqlParameter()
            {
                ParameterName = "@id_b",
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
                Log.Debug("TelephelyDatabaseManager.Delete: MysqlConnection létrehozása és nyitása.");
                OpenConnection(connection);
                command.Connection = connection;
                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
                Log.Debug("TelephelyDatabaseManager.Delete: MysqlCommand végrehajtása");
            }
            catch (MySqlException ex)
            {
                Log.Error("TelephelyDatabaseManager.Delete: Adatbázis hiba. {Message}", ex);
            }
            catch (Exception e)
            {
                Log.Error("TelephelyDatabaseManager.Delete: Hiba történt {Message}", e);

            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }

        public List<Telephely> GetTelephelyek(User filter)
        {
            Log.Debug("TelephelyDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<Telephely> telephelyek = new List<Telephely>();
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "gettelephelyek";
            Log.Debug("TelephelyDatabaseManager.GetAllData: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. User.Id: {Id}", filter.Id);
            command.Parameters.AddWithValue("@user_b", filter.Id);
            try
            {
                Log.Debug("TelephelyDatabaseManager.GetAllData: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("TelephelyDatabaseManager.GetAllData: MysqlCommand végrehajtása");
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Telephely telephely = new Telephely();
                        telephely.Id = int.Parse(reader["id"].ToString());
                        telephely.Name = reader["name"].ToString();
                        telephelyek.Add(telephely);
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Error("TelephelyDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("TelephelyDatabaseManager.GetAllData: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Error("TelephelyDatabaseManager.GetAllData: Hiba történt {Message}", ex);
            }

            return telephelyek;
        }

        public List<Telephely> GetTelephelyek()
        {
            Log.Debug("TelephelyDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<Telephely> telephelyek = new List<Telephely>();
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getalltelephely";
            try
            {
                Log.Debug("TelephelyDatabaseManager.GetAllData: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("TelephelyDatabaseManager.GetAllData: MysqlCommand végrehajtása");
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Telephely telephely = new Telephely();
                        telephely.Id = int.Parse(reader["id"].ToString());
                        telephely.Name = reader["name"].ToString();
                        telephelyek.Add(telephely);
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Error("TelephelyDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("TelephelyDatabaseManager.GetAllData: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Error("TelephelyDatabaseManager.GetAllData: Hiba történt {Message}", ex);
            }

            return telephelyek;
        }

        public Answer ModifyTelephely(Telephely modifiedObject)
        {
            Log.Debug("TelephelyDatabaseManager.Update: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "modifytelephely";
            string message = "Hiba a partner módosítása közben.";
            bool eredmeny = false;
            //IN PARAMETERS
            Log.Debug("TelephelyDatabaseManager.Update: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {ModifiedObject}", modifiedObject);
            MySqlParameter namep = new MySqlParameter()
            {
                ParameterName = "@name_b",
                DbType = System.Data.DbType.String,
                Value = modifiedObject.Name,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter idp = new MySqlParameter()
            {
                ParameterName = "@id_b",
                DbType = System.Data.DbType.Int32,
                Value = modifiedObject.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            command.Parameters.Add(namep);
            command.Parameters.Add(idp);
            try
            {
                Log.Debug("TelephelyDatabaseManager.Update: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("TelephelyDatabaseManager.Update: MysqlCommand végrehajtása");
                    eredmeny = command.ExecuteNonQuery() == 0;
                    if (!eredmeny) message = "Sikeres módosítás.";
                }
                catch (MySqlException ex)
                {
                    Log.Error("TelephelyDatabaseManager.Update: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("TelephelyDatabaseManager.Update: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("TelephelyDatabaseManager.Update: Hiba történt {Message}", ex);

            }

            return new Answer() { Error = eredmeny, Message = message };
        }
    }
}
