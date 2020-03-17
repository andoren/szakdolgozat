using IktatogRPCServer.Database.Abstract;
using IktatogRPCServer.Logger;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Mysql.Abstract
{
    public abstract class MysqlDatabaseManager<T> : DatabaseManager<T>
    {
        public MysqlDatabaseManager(ConnectionManager connection) : base(connection)
        {

        }

        public override void CloseConnection(object connection)
        {
            try
            {
                (connection as MySqlConnection).Close();

            }
            catch (MySqlException e)
            {
                Logging.LogToScreenAndFile(e.Message);

            }
            catch (Exception ex)
            {
                Logging.LogToScreenAndFile(ex.Message);

            }
        }


        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionManager.ConnectionString);
        }

        public override void OpenConnection(object connection)
        {
            try
            {
                (connection as MySqlConnection).Open();

            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Logging.LogToScreenAndFile("Nem tudok a mysql szerverhez kapcsolódni.");
                        Logging.LogToScreenAndFile(e.Message);
                        break;

                    case 1045:
                        Logging.LogToScreenAndFile("Hibás felhasználónév vagy jelszó");
                        break;
                }

            }
            catch (Exception ex)
            {
                Logging.LogToScreenAndFile(ex.Message);

            }
        }
    }   
}
