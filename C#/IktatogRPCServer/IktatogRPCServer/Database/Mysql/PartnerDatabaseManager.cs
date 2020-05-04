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
    class PartnerDatabaseManager : MysqlDatabaseManager, IManagePartner
    {

        public Partner AddPartner(NewTorzsData newObject, User user)
        {
            Log.Debug("PartnerDatabaseManager.Add: Mysqlcommand előkészítése.");
            Partner partner = new Partner()
            {
                Name = newObject.Name,
            };
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addpartner";
            //IN PARAMETERS
            Log.Debug("PartnerDatabaseManager.Add: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {NewObject}", newObject);
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
            Log.Debug("PartnerDatabaseManager.Add: Kimenő paraméter létrehozása és hozzáadása a paraméter listához.");
            MySqlParameter newPartnerId = new MySqlParameter()
            {
                ParameterName = "@newid_b",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(newPartnerId);
            Log.Debug("PartnerDatabaseManager.Add: MysqlConnection létrehozása és nyitása.");
            MySqlConnection connection = GetConnection();
            command.Connection = connection;
            OpenConnection(connection);
            try
            {
                Log.Debug("PartnerDatabaseManager.Add: MysqlCommand végrehajtása");
                command.ExecuteNonQuery();
                partner.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
                Log.Debug("PartnerDatabaseManager.Add: Kimenő paraméter kiolvasása {Id}", partner.Id);
            }

            catch (Exception e)
            {
                Log.Error("PartnerDatabaseManager.Add: Hiba történt {Message}", e);
                throw e;
            }
            finally { CloseConnection(connection); }



            return partner;
        }

        public Answer DeletePartner(int id, User user)
        {
            Log.Debug("PartnerDatabaseManager.Delete: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "delpartner";
            Log.Debug("PartnerDatabaseManager.Delete: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {Id}, User: {User}", id, user);
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
                Log.Debug("PartnerDatabaseManager.Delete: MysqlConnection létrehozása és nyitása.");
                OpenConnection(connection);
                command.Connection = connection;
                Log.Debug("PartnerDatabaseManager.Delete: MysqlCommand végrehajtása");
                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
            }
            catch (MySqlException ex)
            {
                Log.Error("PartnerDatabaseManager.Delete: Adatbázis hiba. {Message}", ex);
            }
            catch (Exception e)
            {
                Log.Error("PartnerDatabaseManager.Delete: Hiba történt {Message}", e);

            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }

        public List<Partner> GetPartnerek(Telephely filter)
        {
            Log.Debug("PartnerDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<Partner> partnerek = new List<Partner>();
            MySqlCommand command = new MySqlCommand();
            Log.Debug("PartnerDatabaseManager.GetAllData: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {Telephely}", telephely);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getpartners";
            command.Parameters.AddWithValue("@telephely_b", filter.Id);
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
                        Partner partner = new Partner();
                        partner.Id = int.Parse(reader["id"].ToString());
                        partner.Name = reader["name"].ToString();
                        partnerek.Add(partner);
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Error("PartnerDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("PartnerDatabaseManager.GetAllData: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Error("PartnerDatabaseManager.GetAllData: Hiba történt {Message}", ex);
            }


            return partnerek;
        }

        public Answer ModifyPartner(Partner modifiedObject)
        {
            Log.Debug("PartnerDatabaseManager.Update: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "modifypartner";
            string message = "Hiba a partner módosítása közben.";
            bool eredmeny = false;
            //IN PARAMETERS
            Log.Debug("PartnerDatabaseManager.Update: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {ModifiedObject}", modifiedObject);
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
                Log.Debug("PartnerDatabaseManager.Update: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("PartnerDatabaseManager.Update: MysqlCommand végrehajtása");
                    eredmeny = command.ExecuteNonQuery() == 0;
                    if (!eredmeny) message = "Sikeres módosítás.";
                }
                catch (MySqlException ex)
                {
                    Log.Error("PartnerDatabaseManager.Update: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("PartnerDatabaseManager.Update: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("PartnerDatabaseManager.Update: Hiba történt {Message}", ex);

            }

            return new Answer() { Error = eredmeny, Message = message };
        }

    }
}
