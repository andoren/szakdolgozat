using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using MySql.Data.MySqlClient;
using Serilog;
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

  
        public override Answer Delete(int id, User user)
        {
            throw new NotImplementedException();
        }



        public override List<RovidIkonyv> GetAllData(object filter)
        {
            Log.Debug("RovidIkonyvDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<RovidIkonyv> rovidikoynvek = new List<RovidIkonyv>();
            if (filter is Telephely)
            {
                Telephely telephely = filter as Telephely;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getshortikonyv";
                Log.Debug("RovidIkonyvDatabaseManager.GetAllData: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. {Telephely}", telephely);
                command.Parameters.AddWithValue("@telephely_b", telephely.Id);
                try
                {
                    Log.Debug("RovidIkonyvDatabaseManager.GetAllData: MysqlConnection létrehozása és nyitása.");
                    MySqlConnection connection = GetConnection();
                    command.Connection = connection;
                    OpenConnection(connection);
                    try
                    {
                        Log.Debug("RovidIkonyvDatabaseManager.GetAllData: MysqlCommand végrehajtása");
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
                        Log.Error("RovidIkonyvDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                    }
                    catch (Exception e)
                    {
                        Log.Error("RovidIkonyvDatabaseManager.GetAllData: Hiba történt {Message}", e);

                    }
                    finally
                    {
                        CloseConnection(connection);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("RovidIkonyvDatabaseManager.GetAllData: Hiba történt {Message}", ex);
                }
            }

            return rovidikoynvek;
        }
        public override Answer Update(RovidIkonyv modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
