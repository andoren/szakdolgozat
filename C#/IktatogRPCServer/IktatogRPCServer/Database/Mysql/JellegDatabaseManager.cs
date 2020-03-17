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
    class JellegDatabaseManager : MysqlDatabaseManager<Jelleg>
    {
        public JellegDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }


        public override Jelleg Add(NewTorzsData newObject, User user)
        {
            Jelleg jelleg = new Jelleg()
            {
                Name = newObject.Name,
            };
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "AddKind";
            //IN PARAMETERS
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

            command.Parameters.Add(newJellegId);
            try
            {
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    command.ExecuteNonQuery();
                    jelleg.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
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

            return jelleg;
        }

        public override Jelleg Add(Jelleg newObject, User user)
        {
            return new Jelleg() { Id = 0, Name = "Not Implemented On ServerSide!" };
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Jelleg> GetAllData()
        {
            throw new NotImplementedException();
        }

        public override List<Jelleg> GetAllData(object filter)
        {
            List<Jelleg> jellegek = new List<Jelleg>();
            if (filter is Telephely) {
                Telephely telephely = filter as Telephely;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getjellegek";
                command.Parameters.AddWithValue("@telephely_b",telephely.Id);
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
                            Jelleg jelleg = new Jelleg();
                            jelleg.Id = int.Parse(reader["id"].ToString());
                            jelleg.Name = reader["name"].ToString();
                            jellegek.Add(jelleg);
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
          
            return jellegek;
        }

        public override Jelleg GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Jelleg modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
