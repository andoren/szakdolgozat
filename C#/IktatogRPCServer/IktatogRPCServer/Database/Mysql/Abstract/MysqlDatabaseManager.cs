using IktatogRPCServer.Database.Abstract;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace IktatogRPCServer.Database.Mysql.Abstract
{
    public abstract class MysqlDatabaseManager<T> : DatabaseManager<T> where T: new ()
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
                Log.Warning("Hiba az adatbázis kapcsolat bontása közben. {Message}",e);

            }
            catch (Exception ex)
            {
                Log.Warning("Hiba az adatbázis kapcsolat bontása közben. {Message}", ex);

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
                Log.Error("Hiba az adatbázis kapcsolat nyitása közben. {Message}", e);

            }
            catch (Exception ex)
            {
                Log.Error("Hiba az adatbázis kapcsolat nyitása közben. {Message}", ex);

            }
        }
    }   
}
