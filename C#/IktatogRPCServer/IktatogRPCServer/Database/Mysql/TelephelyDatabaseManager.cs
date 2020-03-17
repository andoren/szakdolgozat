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

        public override Telephely Add(Telephely newObjet)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
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

        public override bool Update(Telephely modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
