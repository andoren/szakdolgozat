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
    class IkonyvDatabaseManager : MysqlDatabaseManager<Ikonyv>
    {
        public IkonyvDatabaseManager(ConnectionManager connection) : base(connection)
        {
        }
    
        public override Ikonyv Add(Ikonyv newObjet)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Ikonyv> GetAllData()
        {
            return new List<Ikonyv>();
        }

        public override List<Ikonyv> GetAllData(object filter)
        {
            List<Ikonyv> ikonyvek = new List<Ikonyv>();
            if (filter is User) {
                User user = filter as User;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getikonyvek";
                command.Parameters.AddWithValue("@user_id_b", user.Id);
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
                            Ikonyv ikonyv = new Ikonyv();
                            ikonyv.Id = int.Parse(reader["ikonyvid"].ToString());
                            ikonyv.Iktatoszam = reader["iktatoszam"].ToString();
                            ikonyv.Targy = reader["targy"].ToString();
                            ikonyv.Irany = int.Parse(reader["irany"].ToString());
                            ikonyv.Hivszam = reader["hivszam"].ToString();
                            ikonyv.Erkezett = reader["erkezett"].ToString();
                            ikonyv.HatIdo = reader["hatido"].ToString();
                            ikonyv.Szoveg = reader["szoveg"].ToString();
                            ikonyv.Partner = new Partner()
                            {
                                Id = int.Parse(reader["partnerid"].ToString()),
                                Name = reader["partnername"].ToString()
                            };
                            ikonyv.Partner.Ugyintezok.Add(new PartnerUgyintezo { Id = int.Parse(reader["partnerugyintezoid"].ToString()), Name = reader["partnerugyintezoname"].ToString() });
                            ikonyv.Ugyintezo = new Ugyintezo() { Id = int.Parse(reader["ugyintezoid"].ToString()), Name = reader["ugyintezoname"].ToString() };
                            ikonyv.Csoport = new Csoport() { Id = int.Parse(reader["csoportid"].ToString()), Name = reader["csoportname"].ToString(), Shortname = reader["csoportshortname"].ToString(), };
                            ikonyv.Jelleg = new Jelleg() { Id = int.Parse(reader["jellegid"].ToString()), Name = reader["jellegname"].ToString() };
                            ikonyvek.Add(ikonyv);
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
            return ikonyvek;
        }

        public override Ikonyv GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Ikonyv modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
