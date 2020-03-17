using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using IktatogRPCServer.Logger;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Mysql
{
    class UgyintezoDatabaseManager : MysqlDatabaseManager<Ugyintezo>
    {
        public UgyintezoDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }

        public override Ugyintezo Add(Ugyintezo newObjet)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Ugyintezo> GetAllData()
        {
            throw new NotImplementedException();
        }

        public override List<Ugyintezo> GetAllData(object filter)
        {
            List<Ugyintezo> ugyintezok = new List<Ugyintezo>();
            if (filter is Telephely)
            {
                Thread.Sleep(1000);
                Telephely telephely = filter as Telephely;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getugyintezok";
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
                            Ugyintezo ugyintezo = new Ugyintezo();
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

        public override Ugyintezo GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Ugyintezo modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
