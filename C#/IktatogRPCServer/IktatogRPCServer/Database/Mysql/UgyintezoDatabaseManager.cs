using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using Serilog;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IktatogRPCServer.Database.Interfaces;

namespace IktatogRPCServer.Database.Mysql
{
    class UgyintezoDatabaseManager : MysqlDatabaseManager, IManageUgyintezo
    {

        public Ugyintezo AddUgyintezo(NewTorzsData newObject, User user)
        {
            Log.Debug("UgyintezoDatabaseManager.Add: Mysqlcommand előkészítése.");
            Ugyintezo ugyintezo = new Ugyintezo()
            {
                Name = newObject.Name,

            };

            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addugyintezo";
            //IN PARAMETERS
            Log.Debug("UgyintezoDatabaseManager.Add: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {NewObject}", newObject);
            MySqlParameter telephelyp = new MySqlParameter()
            {
                ParameterName = "@telephely_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Telephely.Id,
                Direction = System.Data.ParameterDirection.Input
            };

            MySqlParameter namep = new MySqlParameter()
            {
                ParameterName = "@name_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Name,
                Direction = System.Data.ParameterDirection.Input
            };

            MySqlParameter userp = new MySqlParameter()
            {
                ParameterName = "@created_by_b",
                DbType = System.Data.DbType.Int32,
                Value = user.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            command.Parameters.Add(telephelyp);
            command.Parameters.Add(namep);
            command.Parameters.Add(userp);
            //OUTPARAMETER
            Log.Debug("UgyintezoDatabaseManager.Add: Kimenő paraméter létrehozása és hozzáadása a paraméter listához.");
            MySqlParameter newPartnerId = new MySqlParameter()
            {
                ParameterName = "@newid_b",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(newPartnerId);
            Log.Debug("UgyintezoDatabaseManager.Add: MysqlConnection létrehozása és nyitása.");
            MySqlConnection connection = GetConnection();
            command.Connection = connection;
            OpenConnection(connection);
            try
            {
                Log.Debug("UgyintezoDatabaseManager.Add: MysqlCommand végrehajtása");
                command.ExecuteNonQuery();
                ugyintezo.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
                Log.Debug("UgyintezoDatabaseManager.Add: Kimenő paraméter kiolvasása {Id}", ugyintezo.Id);
            }

            catch (Exception e)
            {
                Log.Error("UgyintezoDatabaseManager.Add: Hiba történt {Message}", e);
                throw e;
            }
            finally { CloseConnection(connection); }


            return ugyintezo;
        }

        public Answer DeleteUgyintezo(int id, User user)
        {
            Log.Debug("UgyintezoDatabaseManager.Delete: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "delugyintezo";
            Log.Debug("UgyintezoDatabaseManager.Delete: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {Id}, User: {User}", id, user);
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
                Log.Debug("UgyintezoDatabaseManager.Delete: MysqlConnection létrehozása és nyitása.");
                OpenConnection(connection);
                command.Connection = connection;
                Log.Debug("UgyintezoDatabaseManager.Delete: MysqlCommand végrehajtása");
                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
            }
            catch (MySqlException ex)
            {
                Log.Error("UgyintezoDatabaseManager.Delete: Adatbázis hiba. {Message}", ex);
            }
            catch (Exception e)
            {
                Log.Error("UgyintezoDatabaseManager.Delete: Hiba történt {Message}", e);

            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }


        public List<Ugyintezo> GetUgyintezok(Telephely filter)
        {
            Log.Debug("PartnerDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<Ugyintezo> ugyintezok = new List<Ugyintezo>();

            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getugyintezok";
            Log.Debug("UgyintezoDatabaseManager.GetAllData: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {Telephely}", telephely);
            command.Parameters.AddWithValue("@telephely_b", filter.Id);
            try
            {
                Log.Debug("UgyintezoDatabaseManager.GetAllData: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("UgyintezoDatabaseManager.GetAllData: MysqlCommand végrehajtása");
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Ugyintezo ugyintezo = new Ugyintezo();
                        ugyintezo.Id = int.Parse(reader["id"].ToString());
                        ugyintezo.Name = reader["name"].ToString();
                        ugyintezok.Add(ugyintezo);
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Error("UgyintezoDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("UgyintezoDatabaseManager.GetAllData: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Error("UgyintezoDatabaseManager.GetAllData: Hiba történt {Message}", ex);
            }


            return ugyintezok;
        }

        public Answer ModifyUgyintezo(Ugyintezo modifiedObject)
        {
            Log.Debug("UgyintezoDatabaseManager.Update: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "modifyugyintezo";
            string message = "Hiba a partner módosítása közben.";
            bool eredmeny = false;
            //IN PARAMETERS
            Log.Debug("UgyintezoDatabaseManager.Update: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {ModifiedObject}", modifiedObject);
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
                Log.Debug("UgyintezoDatabaseManager.Update: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("UgyintezoDatabaseManager.Update: MysqlCommand végrehajtása");
                    eredmeny = command.ExecuteNonQuery() == 0;
                    if (!eredmeny) message = "Sikeres módosítás.";
                }
                catch (MySqlException ex)
                {
                    Log.Error("UgyintezoDatabaseManager.Update: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("UgyintezoDatabaseManager.Update: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("UgyintezoDatabaseManager.Update: Hiba történt {Message}", ex);

            }

            return new Answer() { Error = eredmeny, Message = message };
        }

    }
}
