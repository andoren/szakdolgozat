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
    


        public override Ikonyv Add(NewTorzsData newObject, User user)
        {
            throw new NotImplementedException();
        }

        public override Ikonyv Add(Ikonyv newObject, User user)
        {
            if (newObject.ValaszId == -1) {
                return AddRootIkonyv(newObject, user);
            }else
            {
                return AddSubIkonyv(newObject, user);
            }
         
        }

        private Ikonyv AddRootIkonyv(Ikonyv newObject, User user)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addRootIkonyv";
            //IN PARAMETERS
            MySqlParameter targyp = new MySqlParameter()
            {
                ParameterName = "@targy_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Targy,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter hivszamp = new MySqlParameter()
            {
                ParameterName = "@hivszam_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Hivszam,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter ugyintezop = new MySqlParameter()
            {
                ParameterName = "@ugyintezo_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Ugyintezo.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter partnerp = new MySqlParameter()
            {
                ParameterName = "@partner_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Partner.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter partnerugyintp = new MySqlParameter()
            {
                ParameterName = "@partnerugyint_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Partner.Ugyintezok.Count > 0?newObject.Partner.Ugyintezok[0].Id:-1,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter userp = new MySqlParameter()
            {
                ParameterName = "@created_by_b",
                DbType = System.Data.DbType.Int32,
                Value = user.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter telephelyp = new MySqlParameter()
            {
                ParameterName = "@telephely_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Telephely.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter csoportp = new MySqlParameter()
            {
                ParameterName = "@csoport_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Csoport.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter jellegp = new MySqlParameter()
            {
                ParameterName = "@jelleg_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Jelleg.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter iranyp = new MySqlParameter()
            {
                ParameterName = "@irany_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Irany,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter erkezettp = new MySqlParameter()
            {
                ParameterName = "@erkezett_b",
                DbType = System.Data.DbType.DateTime,
                Value = DateTime.Parse(newObject.Erkezett) ,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter hatidop = new MySqlParameter()
            {
                ParameterName = "@hatido_b",
                DbType = System.Data.DbType.DateTime,
                Value =  DateTime.Parse(newObject.HatIdo),
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter szovegp = new MySqlParameter()
            {
                ParameterName = "@szoveg_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Szoveg,
                Direction = System.Data.ParameterDirection.Input
            };
            //OUT parameters
            MySqlParameter newidp = new MySqlParameter()
            {
                ParameterName = "@newid",
                DbType = System.Data.DbType.Int32,  
                Direction = System.Data.ParameterDirection.Output
            };
            MySqlParameter iktaszmp = new MySqlParameter()
            {
                ParameterName = "@iktszam",
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(targyp);
            command.Parameters.Add(hivszamp);
            command.Parameters.Add(ugyintezop);
            command.Parameters.Add(partnerp);
            command.Parameters.Add(partnerugyintp);
            command.Parameters.Add(userp);
            command.Parameters.Add(telephelyp);
            command.Parameters.Add(csoportp);
            command.Parameters.Add(jellegp);
            command.Parameters.Add(iranyp);
            command.Parameters.Add(erkezettp);
            command.Parameters.Add(hatidop);
            command.Parameters.Add(szovegp);
            command.Parameters.Add(newidp);
            command.Parameters.Add(iktaszmp);
            try
            {
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    command.ExecuteNonQuery();
                    newObject.Id = int.Parse(command.Parameters["@newid"].Value.ToString());
                    newObject.Iktatoszam = command.Parameters["@iktszam"].Value.ToString();
                }
                catch (MySqlException ex)
                {
                    Logging.LogToScreenAndFile("Error code: " + ex.Code + " Error message: " + ex.Message);
                }
                catch (Exception e)
                {
                    Logging.LogToScreenAndFile(e.Message);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Logging.LogToScreenAndFile(ex.Message);

            }

            return newObject;
        }
        private Ikonyv AddSubIkonyv(Ikonyv newObject, User user) {
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addsubikonyv";
            //IN PARAMETERS
            MySqlParameter targyp = new MySqlParameter()
            {
                ParameterName = "@targy_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Targy,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter hivszamp = new MySqlParameter()
            {
                ParameterName = "@hivszam_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Hivszam,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter ugyintezop = new MySqlParameter()
            {
                ParameterName = "@ugyintezo_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Ugyintezo.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter partnerp = new MySqlParameter()
            {
                ParameterName = "@partner_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Partner.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter partnerugyintp = new MySqlParameter()
            {
                ParameterName = "@partnerugyint_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Partner.Ugyintezok.Count > 0 ? newObject.Partner.Ugyintezok[0].Id : -1,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter userp = new MySqlParameter()
            {
                ParameterName = "@created_by_b",
                DbType = System.Data.DbType.Int32,
                Value = user.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter telephelyp = new MySqlParameter()
            {
                ParameterName = "@telephely_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Telephely.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter csoportp = new MySqlParameter()
            {
                ParameterName = "@csoport_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Csoport.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter jellegp = new MySqlParameter()
            {
                ParameterName = "@jelleg_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Jelleg.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter iranyp = new MySqlParameter()
            {
                ParameterName = "@irany_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.Irany,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter erkezettp = new MySqlParameter()
            {
                ParameterName = "@erkezett_b",
                DbType = System.Data.DbType.DateTime,
                Value = DateTime.Parse(newObject.Erkezett),
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter hatidop = new MySqlParameter()
            {
                ParameterName = "@hatido_b",
                DbType = System.Data.DbType.DateTime,
                Value = DateTime.Parse(newObject.HatIdo),
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter szovegp = new MySqlParameter()
            {
                ParameterName = "@szoveg_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Szoveg,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter parentidp = new MySqlParameter()
            {
                ParameterName = "@parentid_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.ValaszId,
                Direction = System.Data.ParameterDirection.Input
            };
            //OUT parameters
            MySqlParameter newidp = new MySqlParameter()
            {
                ParameterName = "@newid",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Output
            };
            MySqlParameter iktaszmp = new MySqlParameter()
            {
                ParameterName = "@myIktSzam",
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(targyp);
            command.Parameters.Add(hivszamp);
            command.Parameters.Add(ugyintezop);
            command.Parameters.Add(partnerp);
            command.Parameters.Add(partnerugyintp);
            command.Parameters.Add(userp);
            command.Parameters.Add(telephelyp);
            command.Parameters.Add(csoportp);
            command.Parameters.Add(jellegp);
            command.Parameters.Add(iranyp);
            command.Parameters.Add(erkezettp);
            command.Parameters.Add(hatidop);
            command.Parameters.Add(szovegp);
            command.Parameters.Add(newidp);
            command.Parameters.Add(iktaszmp);
            command.Parameters.Add(parentidp);
            try
            {
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    command.ExecuteNonQuery();
                    newObject.Id = int.Parse(command.Parameters["@newid"].Value.ToString());
                    newObject.Iktatoszam = command.Parameters["@myIktSzam"].Value.ToString();
                }
                catch (MySqlException ex)
                {
                    Logging.LogToScreenAndFile("Error code: " + ex.Code + " Error message: " + ex.Message);
                }
                catch (Exception e)
                {
                    Logging.LogToScreenAndFile(e.Message);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Logging.LogToScreenAndFile(ex.Message);

            }

            return newObject;
        }

        public override Answer Delete(int id, User user)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "delikonyv";
            MySqlParameter idp = new MySqlParameter()
            {
                ParameterName = "@id_b",
                DbType = System.Data.DbType.Int32,
                Value = id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter deleterp = new MySqlParameter()
            {
                ParameterName = "@deleter_b",
                DbType = System.Data.DbType.Int32,
                Value = user.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            command.Parameters.Add(idp);
            command.Parameters.Add(deleterp);
            bool eredmeny = true;
            string message = "Sikeres törlés!";
            try
            {
                OpenConnection(connection);
                command.Connection = connection;
                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
            }
            catch (MySqlException ex)
            {
                Logging.LogToScreenAndFile("Error code: " + ex.Code + " Error Message: " + ex.Message);
            }
            catch (Exception e)
            {
                Logging.LogToScreenAndFile(e.Message);
            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
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
                            ikonyv.Telephely = new Telephely() { Id = int.Parse(reader["telephely"].ToString()) };
                            ikonyv.Partner = new Partner()
                            {
                                Id = int.Parse(reader["partnerid"].ToString()),
                                Name = reader["partnername"].ToString()
                            };
                            int oszlopszam = reader.GetOrdinal("partnerugyintezoid");
                            if (!reader.IsDBNull(oszlopszam))ikonyv.Partner.Ugyintezok.Add(new PartnerUgyintezo { Id = int.Parse(reader["partnerugyintezoid"].ToString()), Name = reader["partnerugyintezoname"].ToString() });
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

        public override Answer Update(Ikonyv modifiedObject)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "modifyikonyv";
            string message = "Hiba a partner módosítása közben.";
            bool eredmeny = false;
            //IN PARAMETERS   
            MySqlParameter idp = new MySqlParameter()
            {
                ParameterName = "@id_b",
                DbType = System.Data.DbType.Int32,
                Value = modifiedObject.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter targyp = new MySqlParameter()
            {
                ParameterName = "@targy_b",
                DbType = System.Data.DbType.String,
                Value = modifiedObject.Targy,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter hivszamp = new MySqlParameter()
            {
                ParameterName = "@hivszam_b",
                DbType = System.Data.DbType.String,
                Value = modifiedObject.Hivszam,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter ugyintezop = new MySqlParameter()
            {
                ParameterName = "@ugyintezo_b",
                DbType = System.Data.DbType.Int32,
                Value = modifiedObject.Ugyintezo.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter partnerp = new MySqlParameter()
            {
                ParameterName = "@partner_b",
                DbType = System.Data.DbType.Int32,
                Value = modifiedObject.Partner.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter partnerugyintp = new MySqlParameter()
            {
                ParameterName = "@partnerugyintezo_b",
                DbType = System.Data.DbType.Int32,
                Value = modifiedObject.Partner.Ugyintezok.Count > 0 ? modifiedObject.Partner.Ugyintezok[0].Id : -1,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter jellegp = new MySqlParameter()
            {
                ParameterName = "@jelleg_b",
                DbType = System.Data.DbType.Int32,
                Value = modifiedObject.Jelleg.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter erkezettp = new MySqlParameter()
            {
                ParameterName = "@erkezett_b",
                DbType = System.Data.DbType.DateTime,
                Value = DateTime.Parse(modifiedObject.Erkezett),
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter hatidop = new MySqlParameter()
            {
                ParameterName = "@hatido_b",
                DbType = System.Data.DbType.DateTime,
                Value = DateTime.Parse(modifiedObject.HatIdo),
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter szovegp = new MySqlParameter()
            {
                ParameterName = "@szoveg_b",
                DbType = System.Data.DbType.String,
                Value = modifiedObject.Szoveg,
                Direction = System.Data.ParameterDirection.Input
            };
            command.Parameters.Add(idp);
            command.Parameters.Add(jellegp);
            command.Parameters.Add(targyp);
            command.Parameters.Add(hivszamp);
            command.Parameters.Add(ugyintezop);
            command.Parameters.Add(partnerp);
            command.Parameters.Add(partnerugyintp);
            command.Parameters.Add(erkezettp);
            command.Parameters.Add(hatidop);
            command.Parameters.Add(szovegp);
            try
            {
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    eredmeny = command.ExecuteNonQuery() == 0;
                    if (!eredmeny) message = "Sikeres módosítás.";
                }
                catch (MySqlException ex)
                {
                    Logging.LogToScreenAndFile("Error code: " + ex.Code + " Error message: " + ex.Message);
                }
                catch (Exception e)
                {
                    Logging.LogToScreenAndFile(e.Message);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Logging.LogToScreenAndFile(ex.Message);

            }

            return new Answer() { Error = eredmeny, Message = message };
        }
    }
}
