using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace IktatogRPCServer.Database.Mysql
{
    class PrivilegeDatabaseManager : MysqlDatabaseManager<Privilege>
    {
        public PrivilegeDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }

 

        public override Privilege Add(NewTorzsData newObject, User user)
        {
            throw new NotImplementedException();
        }

        public override Answer Delete(int id, User user)
        {
            throw new NotImplementedException();
        }

        public override List<Privilege> GetAllData(object filter)
        {
            Log.Debug("PrivilegeDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<Privilege> privileges = new List<Privilege>();
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getprivileges";
            try
            {
                Log.Debug("PrivilegeDatabaseManager.GetAllData: MysqlConnection létrehozása és nyitása.");
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    Log.Debug("PrivilegeDatabaseManager.GetAllData: MysqlCommand végrehajtása");
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Privilege privilege = new Privilege();
                        privilege.Id = int.Parse(reader["id"].ToString());
                        privilege.Name = reader["privilege"].ToString();
                        privileges.Add(privilege);
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Error("PrivilegeDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("PrivilegeDatabaseManager.GetAllData: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Error("PrivilegeDatabaseManager.GetAllData: Hiba történt {Message}", ex);
            }


            return privileges;
        }


        public override Answer Update(Privilege modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
