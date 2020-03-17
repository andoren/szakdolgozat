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
    class PrivilegeDatabaseManager : MysqlDatabaseManager<Privilege>
    {
        public PrivilegeDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }

        public override Privilege Add(Privilege newObjet)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Privilege> GetAllData()
        {
            List<Privilege> privileges = new List<Privilege>();
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getprivileges";
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
                        Privilege privilege = new Privilege();
                        privilege.Id = int.Parse(reader["id"].ToString());
                        privilege.Name = reader["privilege"].ToString();
                        privileges.Add(privilege);
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


            return privileges;
        }

        public override List<Privilege> GetAllData(object filter)
        {
            throw new NotImplementedException();
        }

        public override Privilege GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Privilege modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
