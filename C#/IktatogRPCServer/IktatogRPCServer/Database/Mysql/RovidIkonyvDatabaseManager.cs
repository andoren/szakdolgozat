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
    class RovidIkonyvDatabaseManager : MysqlDatabaseManager<RovidIkonyv>
    {
        public RovidIkonyvDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }

  

        public override RovidIkonyv Add(NewTorzsData newObject, User user)
        {
            throw new NotImplementedException();
        }

        public override RovidIkonyv Add(RovidIkonyv newObject, User user)
        {
            throw new NotImplementedException();
        }

        public override Answer Delete(int id, User user)
        {
            throw new NotImplementedException();
        }

        public override List<RovidIkonyv> GetAllData()
        {
            throw new NotImplementedException();
        }

        public override List<RovidIkonyv> GetAllData(object filter)
        {
            List<RovidIkonyv> rovidikoynvek = new List<RovidIkonyv>();
            if (filter is Telephely)
            {
                Telephely telephely = filter as Telephely;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getshortikonyv";
                command.Parameters.AddWithValue("@telephely_b", telephely.Id);
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
                            RovidIkonyv rovidikonyv = new RovidIkonyv();
                            rovidikonyv.Id = int.Parse(reader["ikonyvid"].ToString());
                            rovidikonyv.Iktatoszam = reader["iktatoszam"].ToString();
                            rovidikoynvek.Add(rovidikonyv);
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

            return rovidikoynvek;
        }

        public override RovidIkonyv GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public override Answer Update(RovidIkonyv modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
