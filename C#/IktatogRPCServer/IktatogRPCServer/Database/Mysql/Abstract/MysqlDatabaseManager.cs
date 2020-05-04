﻿
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using IktatogRPCServer.Database.Interfaces;

namespace IktatogRPCServer.Database.Mysql.Abstract
{
    public abstract class MysqlDatabaseManager: IDatabaseConnectionManager<MySqlConnection>
    {
        public ConnectionManager ConnectionManager { get {
                return new ConnectionManager();
            }
        }

        public void CloseConnection(MySqlConnection connection)
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
            return new MySqlConnection(ConnectionManager.ConnectionString);
        }

        public void OpenConnection(MySqlConnection connection)
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
