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
    class PartnerUgyintezoDatabaseManager : MysqlDatabaseManager, IManagePartnerUgyintezo
    {

        public PartnerUgyintezo AddPartnerUgyintezo(NewTorzsData newObject, User user)
        {
            Log.Debug("PartnerUgyintezoDatabaseManager.Add: Mysqlcommand előkészítése.");
            PartnerUgyintezo ugyintezo = new PartnerUgyintezo()
            {
                Name = newObject.Name,

            };

            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addpartnerugyintezo";
            Log.Debug("PartnerUgyintezoDatabaseManager.Add: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {NewObject}", newObject);
            //IN PARAMETERS
            MySqlParameter partnerp = new MySqlParameter()
            {
                ParameterName = "@partner_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Partner.Id,
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
            command.Parameters.Add(partnerp);
            command.Parameters.Add(namep);
            command.Parameters.Add(userp);
            //OUTPARAMETER
            Log.Debug("PartnerUgyintezoDatabaseManager.Add: Kimenő paraméter létrehozása és hozzáadása a paraméter listához.");
            MySqlParameter newPartnerId = new MySqlParameter()
            {
                ParameterName = "@newid_b",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(newPartnerId);
            try
            {
                Log.Debug("PartnerUgyintezoDatabaseManager.Add: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("PartnerUgyintezoDatabaseManager.Add: MysqlCommand végrehajtása");
                    command.ExecuteNonQuery();
                    ugyintezo.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
                    Log.Debug("PartnerUgyintezoDatabaseManager.Add: Kimenő paraméter kiolvasása {Id}", ugyintezo.Id);
                }
                catch (MySqlException ex)
                {
                    Log.Error("PartnerUgyintezoDatabaseManager.Add: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("PartnerUgyintezoDatabaseManager.Add: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("PartnerUgyintezoDatabaseManager.Add: Hiba történt {Message}", ex);

            }

            return ugyintezo;
        }

        public Answer DeletePartnerUgyintezo(int id, User user)
        {
            Log.Debug("PartnerUgyintezoDatabaseManager.Delete: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "delpartnerugyintezo";
            Log.Debug("PartnerUgyintezoDatabaseManager.Delete: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {Id}, User: {User}", id, user);
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
            Log.Debug("PartnerUgyintezoDatabaseManager.Delete: MysqlConnection létrehozása és nyitása.");
            MySqlConnection connection = GetConnection();
            OpenConnection(connection);
            command.Connection = connection;
            string message = "Sikeres törlés!";
            try
            {
                Log.Debug("PartnerUgyintezoDatabaseManager.Delete: MysqlCommand végrehajtása");
                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
            }
            catch (MySqlException ex)
            {
                Log.Error("PartnerUgyintezoDatabaseManager.Delete: Adatbázis hiba. {Message}", ex);
            }
            catch (Exception e)
            {
                Log.Error("PartnerUgyintezoDatabaseManager.Delete: Hiba történt {Message}", e);
                throw e;
            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }

        public List<PartnerUgyintezo> GetPartnerUgyintezok(Partner filter)
        {
            Log.Debug("PartnerUgyintezoDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<PartnerUgyintezo> ugyintezok = new List<PartnerUgyintezo>();


            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getpartnerugyintezok";
            Log.Debug("PartnerUgyintezoDatabaseManager.GetAllData: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {Partner}", filter);

            command.Parameters.AddWithValue("@partner_b", filter.Id);
            try
            {
                Log.Debug("PartnerUgyintezoDatabaseManager.GetAllData: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("PartnerUgyintezoDatabaseManager.GetAllData: MysqlCommand végrehajtása");
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PartnerUgyintezo ugyintezo = new PartnerUgyintezo();
                        ugyintezo.Id = int.Parse(reader["id"].ToString());
                        ugyintezo.Name = reader["name"].ToString();
                        ugyintezok.Add(ugyintezo);
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Error("PartnerUgyintezoDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("PartnerUgyintezoDatabaseManager.GetAllData: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Error("PartnerUgyintezoDatabaseManager.GetAllData: Hiba történt {Message}", ex);
            }


            return ugyintezok;
        }

        public Answer ModifyPartnerUgyintezo(PartnerUgyintezo modifiedObject)
        {
            Log.Debug("PartnerUgyintezoDatabaseManager.Update: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "modifypartnerugyintezo";
            string message = "Hiba a partner módosítása közben.";
            bool eredmeny = false;
            //IN PARAMETERS
            Log.Debug("PartnerUgyintezoDatabaseManager.Update: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {ModifiedObject}", modifiedObject);
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
                Log.Debug("PartnerUgyintezoDatabaseManager.Update: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("PartnerUgyintezoDatabaseManager.Update: MysqlCommand végrehajtása");
                    eredmeny = command.ExecuteNonQuery() == 0;
                    if (!eredmeny) message = "Sikeres módosítás.";
                }
                catch (MySqlException ex)
                {
                    Log.Error("PartnerUgyintezoDatabaseManager.Update: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("PartnerUgyintezoDatabaseManager.Update: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("PartnerUgyintezoDatabaseManager.Update: Hiba történt {Message}", ex);

            }

            return new Answer() { Error = eredmeny, Message = message };
        }
    }
}
