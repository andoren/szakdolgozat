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
    class CsoportDatabaseManager : MysqlDatabaseManager<Csoport>
    {
        public CsoportDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }

        public override Csoport Add(Csoport newObject, User user)
        {
            throw new NotImplementedException();
        }

        public override Csoport Add(NewTorzsData newObject, User user)
        {
            Csoport csoport = new Csoport()
            {
                Name = newObject.Name,
                Shortname = newObject.Shorname
            };
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addgroup";
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
            MySqlParameter shortnamep = new MySqlParameter()
            {
                ParameterName = "@shortname_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Shorname,
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
            MySqlParameter newCsoportId = new MySqlParameter()
            {
                ParameterName = "@newid_b",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };

            command.Parameters.Add(newCsoportId);
            try
            {
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    command.ExecuteNonQuery();
                    csoport.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
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

            return csoport;
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Csoport> GetAllData()
        {
            return new List<Csoport>();
        }

        public override List<Csoport> GetAllData(object filter)
        {
            List<Csoport> csoportok = new List<Csoport>();
            if (filter is Telephely) {
                Telephely telephely = filter as Telephely;                
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getGroup";
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
                            Csoport csoport = new Csoport();
                            csoport.Id = int.Parse(reader["id"].ToString());
                            csoport.Name = reader["name"].ToString();
                            csoport.Shortname = reader["shortname"].ToString();
                            csoportok.Add(csoport);
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
                    finally { 
                        CloseConnection(connection);
                    }
                }
                catch (Exception ex)
                {
                    Logging.LogToScreenAndFile(ex.Message);
                }
                
            }
            return csoportok;
        }

        public override Csoport GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Csoport modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
