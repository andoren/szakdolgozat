using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using Serilog;
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
                Log.Debug("IkonyvDatabaseManager.Add: AddRootIkonyv meghívva.");
                return AddRootIkonyv(newObject, user);
            }else
            {
                Log.Debug("IkonyvDatabaseManager.Add: AddSubIkonyv meghívva.");
                return AddSubIkonyv(newObject, user);
            }
         
        }

        private Ikonyv AddRootIkonyv(Ikonyv newObject, User user)
        {
            Log.Debug("IkonyvDatabaseManager.AddRootIkonyv: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addRootIkonyv";
            MySqlConnection connection = GetConnection();
            command.Connection = connection;
            OpenConnection(connection);
            Log.Debug("IkonyvDatabaseManager.AddRootIkonyv: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {NewObject}, User: {User}", newObject, user);
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
            Log.Debug("IkonyvDatabaseManager.AddRootIkonyv: Kimenő paraméter beálltása ");
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
    
                try
                {
                    Log.Debug("IkonyvDatabaseManager.AddRootIkonyv: Command végrehajtása");
                    command.ExecuteNonQuery();
                    newObject.Id = int.Parse(command.Parameters["@newid"].Value.ToString());
                    newObject.Iktatoszam = command.Parameters["@iktszam"].Value.ToString();
                    Log.Debug("IkonyvDatabaseManager.AddRootIkonyv: Kimenő paraméterek: {NewObject} ",newObject);
                }
                catch (MySqlException ex)
                {
                    Log.Error("IkonyvDatabaseManager.AddRootIkonyv: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("IkonyvDatabaseManager.AddRootIkonyv: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("IkonyvDatabaseManager.AddRootIkonyv: Hiba történt {Message}", ex);

            }

            return newObject;
        }
        private Ikonyv AddSubIkonyv(Ikonyv newObject, User user) {
            Log.Debug("IkonyvDatabaseManager.AddSubIkonyv: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "addsubikonyv";
            MySqlConnection connection = GetConnection();
            command.Connection = connection;
            OpenConnection(connection);
            //IN PARAMETERS
            Log.Debug("IkonyvDatabaseManager.AddSubIkonyv: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {NewObject}, User: {User}", newObject, user);
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
            Log.Debug("IkonyvDatabaseManager.AddSubIkonyv: Kimenő paraméter beálltása ");
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
     
                try
                {
                    Log.Debug("IkonyvDatabaseManager.AddSubIkonyv: Command végrehajtása");
                    command.ExecuteNonQuery();
                    newObject.Id = int.Parse(command.Parameters["@newid"].Value.ToString());
                    newObject.Iktatoszam = command.Parameters["@myIktSzam"].Value.ToString();
                    Log.Debug("IkonyvDatabaseManager.AddSubIkonyv: Kimenő paraméterek: {NewObject} ", newObject);
                }
                catch (MySqlException ex)
                {
                    Log.Error("IkonyvDatabaseManager.AddSubIkonyv: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("IkonyvDatabaseManager.AddSubIkonyv: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("IkonyvDatabaseManager.AddSubIkonyv: Hiba történt {Message}", ex);

            }

            return newObject;
        }

        public override Answer Delete(int id, User user)
        {
            Log.Debug("IkonyvDatabaseManager.Delete: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "delikonyv";
            OpenConnection(connection);
            command.Connection = connection;
            Log.Debug("IkonyvDatabaseManager.Delete: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {Id}, User: {User}", id, user);
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
                Log.Debug("IkonyvDatabaseManager.Delete: Command végrehajtása");
                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
            }
            catch (MySqlException ex)
            {
                Log.Error("IkonyvDatabaseManager.Delete: Adatbázis hiba. {Message}", ex);
            }
            catch (Exception e)
            {
                Log.Error("IkonyvDatabaseManager.Delete: Hiba történt {Message}", e);

            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }


        public override List<Ikonyv> GetAllData(object filter)
        {
            Log.Debug("IkonyvDatabaseManager.GetAllData: Mysqlcommand előkészítése.");
            List<Ikonyv> ikonyvek = new List<Ikonyv>();
            if (filter is SearchIkonyvData) {
                SearchIkonyvData data = filter as SearchIkonyvData;
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getikonyvek";
                Log.Debug("IkonyvDatabaseManager.GetAllData: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Adat: {Data}", data);
                command.Parameters.AddWithValue("@user_id_b", data.User.Id);
                command.Parameters.AddWithValue("@year_id_b", data.Year.Id);
                command.Parameters.AddWithValue("@irany_b", data.Irany); 
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
               
                    try
                    {

                        Log.Debug("IkonyvDatabaseManager.GetAllData: Command végrehajtása");
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
                            ikonyv.HasDoc = int.Parse(reader["doccount"].ToString()) > 0;
                            ikonyvek.Add(ikonyv);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Log.Error("IkonyvDatabaseManager.GetAllData: Adatbázis hiba. {Message}", ex);
                    }
                    catch (Exception e)
                    {
                        Log.Error("IkonyvDatabaseManager.GetAllData: Hiba történt {Message}", e);

                    }
                    finally
                    {
                        CloseConnection(connection);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("IkonyvDatabaseManager.GetAllData: Hiba történt {Message}", ex);
                }
            }
            return ikonyvek;
        }


        public override Answer Update(Ikonyv modifiedObject)
        {
            Log.Debug("IkonyvDatabaseManager.Update: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "modifyikonyv";
            string message = "Hiba a iktatás módosítása közben.";
            bool eredmeny = false;
            MySqlConnection connection = GetConnection();
            command.Connection = connection;
            OpenConnection(connection);
            //IN PARAMETERS   
            Log.Debug("IkonyvDatabaseManager.Update: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Ikonyv: {ModifiedObject}", modifiedObject);
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

                try
                {
                    Log.Debug("IkonyvDatabaseManager.Update: Command végrehajtása");
                    eredmeny = command.ExecuteNonQuery() == 0;
                    if (!eredmeny) message = "Sikeres módosítás.";
                }
                catch (MySqlException ex)
                {
                    Log.Error("IkonyvDatabaseManager.Update: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("IkonyvDatabaseManager.Update: Hiba történt {Message}", e);

                }
                finally { CloseConnection(connection); }
            }
            catch (Exception ex)
            {
                Log.Error("IkonyvDatabaseManager.Update: Hiba történt {Message}", ex);

            }

            return new Answer() { Error = eredmeny, Message = message };
        }
    }
}
