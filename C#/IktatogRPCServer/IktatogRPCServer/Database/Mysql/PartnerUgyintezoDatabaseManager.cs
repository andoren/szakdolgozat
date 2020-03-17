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
    class PartnerUgyintezoDatabaseManager : MysqlDatabaseManager<PartnerUgyintezo>
    {
        public PartnerUgyintezoDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }

        public override PartnerUgyintezo Add(PartnerUgyintezo newObjet)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<PartnerUgyintezo> GetAllData()
        {
            throw new NotImplementedException();
        }

        public override List<PartnerUgyintezo> GetAllData(object filter)
        {
            List<PartnerUgyintezo> ugyintezok = new List<PartnerUgyintezo>();
            if (filter is Partner)
            {
                Partner partner = filter as Partner;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getpartnerugyintezok";
                command.Parameters.AddWithValue("@partner_b", partner.Id);
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
                            PartnerUgyintezo ugyintezo = new PartnerUgyintezo();
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

        public override PartnerUgyintezo GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Update(PartnerUgyintezo modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
