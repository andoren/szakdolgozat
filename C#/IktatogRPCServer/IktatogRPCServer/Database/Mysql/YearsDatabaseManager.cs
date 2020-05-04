﻿using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using IktatogRPCServer.Database.Interfaces;

namespace IktatogRPCServer.Database.Mysql
{
    class YearsDatabaseManager : MysqlDatabaseManager, IManageEv
    {
        public Answer CloseOldYearAndActivateNewOne(Year modifiedObject)
        {
            Log.Debug("YearsDatabaseManager.Update: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "AddYearAndActivate";
            string message = "Hiba az év hozzáadása közben.";
            bool eredmeny = false;
            //IN PARAMETERS 
            Log.Debug("YearsDatabaseManager.Update: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {ModifiedObject}", modifiedObject);
            MySqlParameter yearp = new MySqlParameter()
            {
                ParameterName = "@id_B",
                DbType = System.Data.DbType.Int32,
                Value = modifiedObject.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            command.Parameters.Add(yearp);
            try
            {
                Log.Debug("YearsDatabaseManager.Update: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("YearsDatabaseManager.Update: MysqlCommand végrehajtása");
                    eredmeny = command.ExecuteNonQuery() == 0;
                    if (!eredmeny) message = "Sikeres év hozzáadás.";

                }
                catch (MySqlException ex)
                {
                    Log.Error("YearsDatabaseManager.Update: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("YearsDatabaseManager.Update: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("YearsDatabaseManager.Update: Hiba történt {Message}", ex);

            }
            return new Answer() { Error = eredmeny, Message = message };
        }
        public List<Year> GetEvek()
        {
            Log.Debug("YearsDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<Year> evek = new List<Year>();
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getevek";
            try
            {
                Log.Debug("YearsDatabaseManager.GetAllData: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("YearsDatabaseManager.GetAllData: MysqlCommand végrehajtása");
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
                    Log.Error("YearsDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("YearsDatabaseManager.GetAllData: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Error("YearsDatabaseManager.GetAllData: Hiba történt {Message}", ex);
            }
            return evek;
        }
    }
}
