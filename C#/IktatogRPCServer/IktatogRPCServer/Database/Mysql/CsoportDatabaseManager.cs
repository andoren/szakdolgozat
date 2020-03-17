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

        public override Csoport Add(Csoport newObjet)
        {
            throw new NotImplementedException();
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
