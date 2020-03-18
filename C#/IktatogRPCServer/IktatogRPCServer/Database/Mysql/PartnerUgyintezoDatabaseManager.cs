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
    class PartnerUgyintezoDatabaseManager : MysqlDatabaseManager<PartnerUgyintezo>
    {
        public PartnerUgyintezoDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }


        public override PartnerUgyintezo Add(NewTorzsData newObject, User user)
        {//in name_b varchar(250),in created_by_b int,in partner_b int, out newid_b int
            PartnerUgyintezo ugyintezo = new PartnerUgyintezo()
            {
                Name = newObject.Name,
                
            };

            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addpartnerugyintezo";
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
            MySqlParameter newPartnerId = new MySqlParameter()
            {
                ParameterName = "@newid_b",
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
                    ugyintezo.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
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

            return ugyintezo;
        }

        public override PartnerUgyintezo Add(PartnerUgyintezo newObject, User user)
        {
            throw new NotImplementedException();
        }

        public override Answer Delete(int id, User user)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "delpartnerugyintezo";
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

        public override List<PartnerUgyintezo> GetAllData()
        {
            throw new NotImplementedException();
        }

        public override List<PartnerUgyintezo> GetAllData(object filter)
        {
            List<PartnerUgyintezo> ugyintezok = new List<PartnerUgyintezo>();
            if (filter is Partner)
            {
                Partner partner = filter as Partner;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getpartnerugyintezok";
                command.Parameters.AddWithValue("@partner_b", partner.Id);
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
                            PartnerUgyintezo ugyintezo = new PartnerUgyintezo();
                            ugyintezo.Id = int.Parse(reader["id"].ToString());
                            ugyintezo.Name = reader["name"].ToString();
                            ugyintezok.Add(ugyintezo);
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

            return ugyintezok;
        }

        public override PartnerUgyintezo GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public override Answer Update(PartnerUgyintezo modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
