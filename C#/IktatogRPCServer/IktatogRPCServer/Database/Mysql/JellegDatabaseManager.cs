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
    class JellegDatabaseManager : MysqlDatabaseManager, IManageJelleg
    {


        public Jelleg AddJelleg(NewTorzsData newObject, User user)
        {
            Log.Debug("JellegDatabaseManager.Add: Mysqlcommand előkészítése.");
            Jelleg jelleg = new Jelleg()
            {
                Name = newObject.Name,
            };
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "AddKind";
            //IN PARAMETERS
            Log.Debug("JellegDatabaseManager.Add: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {NewObject}", newObject);
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
            MySqlParameter newJellegId = new MySqlParameter()
            {
                ParameterName = "@newid_b",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };
            Log.Debug("JellegDatabaseManager.Add: Kimenő paraméter létrehozása és hozzáadása a paraméter listához.");
            command.Parameters.Add(newJellegId);
            try
            {
                Log.Debug("JellegDatabaseManager.Add: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("JellegDatabaseManager.Add: MysqlCommand végrehajtása");
                    command.ExecuteNonQuery();
                    jelleg.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
                    Log.Debug("JellegDatabaseManager.Add: Kimenő paraméter kiolvasása {Id}", jelleg.Id);
                }
                catch (MySqlException ex)
                {
                    Log.Error("JellegDatabaseManager.Add: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("JellegDatabaseManager.Add: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("JellegDatabaseManager.Add: Hiba történt {Message}", ex);

            }

            return jelleg;
        }

        public Answer DeleteJelleg(int id, User user)
        {
            Log.Debug("JellegDatabaseManager.Delete: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "delkind";
            Log.Debug("JellegDatabaseManager.Delete: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {Id}, User: {User}", id, user);
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
                Log.Debug("JellegDatabaseManager.Delete: MysqlConnection létrehozása és nyitása.");
                OpenConnection(connection);
                command.Connection = connection;
                Log.Debug("JellegDatabaseManager.Delete: MysqlCommand végrehajtása");
                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
            }
            catch (MySqlException ex)
            {
                Log.Error("JellegDatabaseManager.Delete: Adatbázis hiba. {Message}", ex);
            }
            catch (Exception e)
            {
                Log.Error("JellegDatabaseManager.Delete: Hiba történt {Message}", e);

            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }

        public List<Jelleg> GetJellegek(Telephely filter)
        {
            Log.Debug("JellegDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<Jelleg> jellegek = new List<Jelleg>();

            Telephely telephely = filter as Telephely;
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getjellegek";
            Log.Debug("JellegDatabaseManager.GetAllData: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {Telephely}", telephely);
            command.Parameters.AddWithValue("@telephely_b", telephely.Id);
            try
            {
                Log.Debug("JellegDatabaseManager.GetAllData: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("JellegDatabaseManager.GetAllData: MysqlCommand végrehajtása");
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Jelleg jelleg = new Jelleg();
                        jelleg.Id = int.Parse(reader["id"].ToString());
                        jelleg.Name = reader["name"].ToString();
                        jellegek.Add(jelleg);
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Error("JellegDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("JellegDatabaseManager.GetAllData: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Error("JellegDatabaseManager.GetAllData: Hiba történt {Message}", ex);
            }

            return jellegek;
        }

        public Answer ModifyJelleg(Jelleg modifiedObject)
        {
            Log.Debug("JellegDatabaseManager.Update: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "modifykind";
            string message = "Hiba a partner módosítása közben.";
            bool eredmeny = false;
            //IN PARAMETERS
            Log.Debug("JellegDatabaseManager.Update: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {ModifiedObject}", modifiedObject);
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
                Log.Debug("JellegDatabaseManager.Update: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("JellegDatabaseManager.Update: MysqlCommand végrehajtása");
                    eredmeny = command.ExecuteNonQuery() == 0;
                    if (!eredmeny) message = "Sikeres módosítás.";
                }
                catch (MySqlException ex)
                {
                    Log.Error("JellegDatabaseManager.Update: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("JellegDatabaseManager.Update: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("JellegDatabaseManager.Update: Hiba történt {Message}", ex);

            }

            return new Answer() { Error = eredmeny, Message = message };
        }

    }
}
