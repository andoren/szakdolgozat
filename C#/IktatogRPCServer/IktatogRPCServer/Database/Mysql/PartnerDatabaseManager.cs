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
    class PartnerDatabaseManager : MysqlDatabaseManager<Partner>
    {
        public PartnerDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }

        public override Partner Add(Partner newObjet)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Partner> GetAllData()
        {
            throw new NotImplementedException();
        }

        public override List<Partner> GetAllData(object filter)
        {
            List<Partner> partnerek = new List<Partner>();
            if (filter is Telephely)
            {
                Telephely telephely = filter as Telephely;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getpartners";
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
                            Partner partner = new Partner();
                            partner.Id = int.Parse(reader["id"].ToString());
                            partner.Name = reader["name"].ToString();
                            partnerek.Add(partner);
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

            return partnerek;
        }

        public override Partner GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Partner modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
