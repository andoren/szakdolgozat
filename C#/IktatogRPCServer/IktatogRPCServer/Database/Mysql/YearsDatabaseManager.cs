﻿using Iktato;
using IktatogRPCServer.Database.Abstract;
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
    class YearsDatabaseManager : MysqlDatabaseManager<Year>
    {
        public YearsDatabaseManager(ConnectionManager connectionManager) : base(connectionManager)
        {
        }

        public override Year Add(Year newObjet)
        {
            throw new NotImplementedException();
        }



        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Year> GetAllData()
        {
            List<Year> evek = new List<Year>();
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getevek";
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
                        Year ev = new Year();
                        ev.Id = int.Parse(reader["id"].ToString());
                        ev.Year_ = int.Parse(reader["year"].ToString());
                        ev.Active = int.Parse(reader["active"].ToString()) == 1 ? true : false;
                        evek.Add(ev);
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
            return evek;
        }

        public override List<Year> GetAllData(object filter)
        {
            throw new NotImplementedException();
        }

        public override Year GetDataById(int id)
        {
            throw new NotImplementedException();
        }

 

        public override bool Update(Year modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
