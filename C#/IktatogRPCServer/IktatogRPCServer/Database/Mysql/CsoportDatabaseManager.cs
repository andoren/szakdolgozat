using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using IktatogRPCServer.Database.Interfaces;

namespace IktatogRPCServer.Database.Mysql
{
    class CsoportDatabaseManager : MysqlDatabaseManager, IManageCsoport
    {

        public Csoport AddCsoport(NewTorzsData data, User user)
        {
            Csoport csoport = new Csoport()
            {
                Name = data.Name,
                Shortname = data.Shorname
            };
            Log.Debug("CsoportDatabaseManager.Add: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addgroup";
            //IN PARAMETERS
            Log.Debug("CsoportDatabaseManager.Add: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {Csoport}", data);
            MySqlParameter telephelyp = new MySqlParameter()
            {
                ParameterName = "@telephely_b",
                DbType = System.Data.DbType.Int32,
                Value = data.Telephely.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter namep = new MySqlParameter()
            {
                ParameterName = "@name_b",
                DbType = System.Data.DbType.String,
                Value = data.Name,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter shortnamep = new MySqlParameter()
            {
                ParameterName = "@shortname_b",
                DbType = System.Data.DbType.String,
                Value = data.Shorname,
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
            command.Parameters.Add(shortnamep);
            command.Parameters.Add(userp);
            //OUTPARAMETER
            Log.Debug("CsoportDatabaseManager.Add: Kimenő paraméter létrehozása és hozzáadása a paraméter listához.");
            MySqlParameter newCsoportId = new MySqlParameter()
            {
                ParameterName = "@newid_b",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            command.Parameters.Add(newCsoportId);
            try
            {
                Log.Debug("CsoportDatabaseManager.Add: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("CsoportDatabaseManager.Add: MysqlCommand végrehajtása");
                    command.ExecuteNonQuery();
                    csoport.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
                    Log.Debug("CsoportDatabaseManager.Add: Kimenő paraméter kiolvasása {Id}", csoport);
                }
                catch (MySqlException ex)
                {
                    Log.Error("CsoportDatabaseManager.Add: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("CsoportDatabaseManager.Add: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("CsoportDatabaseManager.Add: Hiba történt {Message}", ex);

            }

            return csoport;
        }

        public Answer DeleteCsoport(int id, User user)
        {
            Log.Debug("CsoportDatabaseManager.Delete: MysqlConnection létrehozása és nyitása.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            OpenConnection(connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "delgroup";
            Log.Debug("CsoportDatabaseManager.Delete: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához.Id {Id} User: {User}", id, user);
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
                Log.Debug("CsoportDatabaseManager.Delete: MysqlCommand végrehajtása");
                command.Connection = connection;
                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
            }
            catch (MySqlException ex)
            {
                Log.Error("CsoportDatabaseManager.Delete: Adatbázis hiba. {Message}", ex);
            }
            catch (Exception e)
            {
                Log.Error("CsoportDatabaseManager.Delete: Hiba történt {Message}", e);

            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }

        public List<Csoport> GetCsoportok(Telephely filter)
        {
            List<Csoport> csoportok = new List<Csoport>();
            Log.Debug("CsoportDatabaseManager.GetAllData: MysqlConnection létrehozása és nyitása.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.Connection = connection;
            OpenConnection(connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getGroup";
            Log.Debug("CsoportDatabaseManager.GetAllData: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához.Id {Id}", filter.Id);
            command.Parameters.AddWithValue("@telephely_b", filter.Id);
            try
            {


                try
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Log.Debug("CsoportDatabaseManager.Delete: MysqlCommand végrehajtása");
                        Csoport csoport = new Csoport();
                        csoport.Id = int.Parse(reader["id"].ToString());
                        csoport.Name = reader["name"].ToString();
                        csoport.Shortname = reader["shortname"].ToString();
                        csoportok.Add(csoport);
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Error("CsoportDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("CsoportDatabaseManager.GetAllData: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Error("CsoportDatabaseManager.GetAllData: Hiba történt {Message}", ex);
            }


            return csoportok;
        }

        public Answer ModifyCsoport(Csoport modifiedObject)
        {
            Log.Debug("CsoportDatabaseManager.Update: MysqlConnection létrehozása és nyitása.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "modifygroup";
            MySqlConnection connection = GetConnection();
            command.Connection = connection;
            OpenConnection(connection);
            string message = "Hiba a partner módosítása közben.";
            bool eredmeny = false;
            //IN PARAMETERS
            Log.Debug("CsoportDatabaseManager.Update: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Csoport: {Csoport}", modifiedObject);
            MySqlParameter namep = new MySqlParameter()
            {
                ParameterName = "@name_b",
                DbType = System.Data.DbType.String,
                Value = modifiedObject.Name,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter snamep = new MySqlParameter()
            {
                ParameterName = "@sname_b",
                DbType = System.Data.DbType.String,
                Value = modifiedObject.Shortname,
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
            command.Parameters.Add(snamep);
            command.Parameters.Add(idp);
            try
            {

                try
                {
                    Log.Debug("CsoportDatabaseManager.Update: MysqlCommand végrehajtása");
                    eredmeny = command.ExecuteNonQuery() == 0;
                    if (!eredmeny) message = "Sikeres módosítás.";
                }
                catch (MySqlException ex)
                {
                    Log.Error("CsoportDatabaseManager.Update: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("CsoportDatabaseManager.Update: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("CsoportDatabaseManager.Update: Hiba történt {Message}", ex);

            }

            return new Answer() { Error = eredmeny, Message = message };
        }

    }
}
