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
    class TelephelyDatabaseManager : MysqlDatabaseManager<Telephely>
    {
        public TelephelyDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }

        public override Telephely Add(Telephely newObject, User user)
        {
            Telephely telephely = new Telephely()
            {
                Name = newObject.Name,

            };

            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addtelephely";
            //IN PARAMETERS

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
                    telephely.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
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

            return telephely;
        }

        public override Telephely Add(NewTorzsData newObject, User user)
        {
            throw new NotImplementedException();
        }

        public override Answer Delete(int id, User user)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "deltelephely";
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

        public override List<Telephely> GetAllData()
        {
            throw new NotImplementedException();
        }

        public override List<Telephely> GetAllData(object filter)
        {
            List<Telephely> telephelyek = new List<Telephely>();
            if (filter is User)
            {
                User user = filter as User;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "gettelephelyek";
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
                            Telephely telephely = new Telephely();
                            telephely.Id = int.Parse(reader["id"].ToString());
                            telephely.Name = reader["name"].ToString();
                            telephelyek.Add(telephely);
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

            return telephelyek;
        }

        public override Telephely GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public override Answer Update(Telephely modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
