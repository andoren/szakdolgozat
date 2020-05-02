using Google.Protobuf;
using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using Serilog;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Mysql
{
    class DocumentDatabaseManager : MysqlDatabaseManager<Document>
    {
        public DocumentDatabaseManager(ConnectionManager connection) : base(connection)
        {

        }

        public override Answer Delete(int id, User user)
        {
            Log.Debug("DocumentDatabaseManager.Delete: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "deletedoc";
            command.Connection = connection;
            OpenConnection(connection);
            Log.Debug("DocumentDatabaseManager.Delete: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {Id}, User: {USer}", id, user);
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

                Log.Debug("DocumentDatabaseManager.Delete: MysqlConnection létrehozása és nyitása.");

                eredmeny = command.ExecuteNonQuery() == 0;
                if (eredmeny) message = "Hiba a törlés közben.";
                
            }
            catch (MySqlException ex)
            {
                Log.Error("DocumentDatabaseManager.Delete: Adatbázis hiba. {Message}", ex);
            }
            catch (Exception e)
            {
                Log.Error("DocumentDatabaseManager.Delete: Hiba történt {Message}", e);

            }
            finally
            {
                CloseConnection(connection);
            }
            return new Answer() { Error = eredmeny, Message = message };
        }

        public Document GetDataById(DocumentInfo documentInfo)
        {
            Document document = GetDocumentFromInfo(documentInfo);
            return document;
        }

        private Document GetDocumentFromInfo(DocumentInfo documentInfo)
        {
            Log.Debug("DocumentDatabaseManager.GetDocumentFromInfo: Adatok beolvasása.");
            Document document = new Document();
            document.Name = documentInfo.Name;
            document.Type = documentInfo.Type;
            document.Path = documentInfo.Path;
            document.Doc = ByteString.CopyFrom(GetBytesFromFile(document.Path));
            return document;
        }
        private byte[] GetBytesFromFile(string FileName)
        {
            byte[] ba1 = new byte[1];
            FileStream fs;
            BinaryReader br;
            try
            {
                Log.Debug("DocumentDatabaseManager.GetBytesFromFile: Filestream megnyitás az elérési úttal: {FileName}",FileName);
                fs = new FileStream(FileName, FileMode.Open);
                long lFileSize = fs.Length;
                br = new BinaryReader(fs);
                Log.Debug("DocumentDatabaseManager.GetBytesFromFile: Adatok beolvasása.");
                ba1 = br.ReadBytes((Int32)lFileSize);

                br.Close();
                fs.Close();
            }
            catch (UnauthorizedAccessException)
            {
                Log.Debug("DocumentDatabaseManager.GetBytesFromFile: Nincs engedélyem a fájlhoz. Átmásolva temp könyvtárba.");
                string temppath = Path.GetTempPath();
                string fullpath = temppath + $"{FileName}";
                if (File.Exists(fullpath))
                {
                    Log.Debug("DocumentDatabaseManager.GetBytesFromFile: A fájl már létezett.");
                    File.Delete(fullpath);
                    File.Copy(FileName, fullpath);
                }
                else File.Copy(FileName, fullpath);
                fs = new FileStream(fullpath, FileMode.Open);
                long lFileSize = fs.Length;
                Log.Debug("DocumentDatabaseManager.GetBytesFromFile: A fájl beolvasása.");
                br = new BinaryReader(fs);
                ba1 = br.ReadBytes((Int32)lFileSize);

                br.Close();
                fs.Close();

            }
            catch (Exception e)
            {
                Log.Error("DocumentDatabaseManager.GetBytesFromFile: Hiba a dokumentum kiolvasása közben a szerverről {Message}", e);
            }
            return (ba1);
        }
        public override Answer Update(Document modifiedObject)
        {
            throw new NotImplementedException();
        }

        public List<DocumentInfo> GetDocumentInfosByIkonyv(Ikonyv ikonyv)
        {
            List<DocumentInfo> documentInfos = new List<DocumentInfo>();
            Log.Debug("DocumentDatabaseManager.GetDocumentInfosByIkonyv: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.Connection = connection;
            OpenConnection(connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "getdocsbyikonyvid";
            Log.Debug("DocumentDatabaseManager.GetDocumentInfosByIkonyv: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {Id}", ikonyv.Id );
            command.Parameters.AddWithValue("@ikonyvid_b", ikonyv.Id);
            try
            {

                try
                {
                    Log.Debug("DocumentDatabaseManager.GetDocumentInfosByIkonyv: command végrehajtása és adatok olvasása");
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DocumentInfo info = new DocumentInfo();
                        info.Id = int.Parse(reader["id"].ToString());
                        info.Name = reader["name"].ToString();
                        info.Type = reader["ext"].ToString();
                        info.Path = reader["path"].ToString();
                        info.Size = double.Parse(reader["size"].ToString());
                        documentInfos.Add(info);
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Error("DocumentDatabaseManager.GetDocumentInfosByIkonyv: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("DocumentDatabaseManager.GetDocumentInfosByIkonyv: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Log.Error("DocumentDatabaseManager.GetDocumentInfosByIkonyv: Hiba történt {Message}", ex);
            }


            return documentInfos;
        }
        public override List<Document> GetAllData(object filter)
        {
            throw new NotImplementedException();
        }

        public override Document Add(NewTorzsData newObject, User user)
        {
            throw new NotImplementedException();
        }

        public override Document Add(Document newObject, User user)
        {
            Log.Debug("DocumentDatabaseManager.Add: Mysqlcommand előkészítése.");
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.Connection = connection;
            OpenConnection(connection);
            Log.Debug("DocumentDatabaseManager.Add: Új fájlnév generálása.");
            string path = GetUniqueFileName();
            Log.Debug("DocumentDatabaseManager.Add: Az új fálj neve és elérési utja: {Path}", path);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "adddoc";
            //IN PARAMETERS
            Log.Debug("DocumentDatabaseManager.Add: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Document: {Name}, User: {User}", newObject.Name, user);
            MySqlParameter namep = new MySqlParameter()
            {
                ParameterName = "@name_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Name,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter typep = new MySqlParameter()
            {
                ParameterName = "@ext_b",
                DbType = System.Data.DbType.String,
                Value = newObject.Type,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter path_b = new MySqlParameter()
            {
                ParameterName = "@path_b",
                DbType = System.Data.DbType.String,
                Value = path,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter userp = new MySqlParameter()
            {
                ParameterName = "@created_by_b",
                DbType = System.Data.DbType.Int32,
                Value = user.Id,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter iktp = new MySqlParameter()
            {
                ParameterName = "@ikt_id_b",
                DbType = System.Data.DbType.Int32,
                Value = newObject.IkonyvId,
                Direction = System.Data.ParameterDirection.Input
            };
            MySqlParameter sizep = new MySqlParameter()
            {
                ParameterName = "@size_b",
                DbType = System.Data.DbType.Double,
                Value = (newObject.Doc.Length / (double)1024) / 1024,
                Direction = System.Data.ParameterDirection.Input
            };
            command.Parameters.Add(namep);
            command.Parameters.Add(typep);
            command.Parameters.Add(path_b);
            command.Parameters.Add(userp);
            command.Parameters.Add(iktp);
            command.Parameters.Add(sizep);
            //OUTPARAMETER
            Log.Debug("DocumentDatabaseManager.Add: Kimenő paraméter létrehozása és hozzáadása a paraméter listához.");
            MySqlParameter newid = new MySqlParameter()
            {
                ParameterName = "@newid_b",
                DbType = System.Data.DbType.Int32,

                Direction = System.Data.ParameterDirection.Output
            };

            command.Parameters.Add(newid);
            try
            {

                try
                {
                    command.ExecuteNonQuery();
                    newObject.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
                    Log.Debug("DocumentDatabaseManager.Add: Kimenő paraméter kiolvasása {Id}", newObject.Id);
                    newObject.Path = path;
                }
                catch (MySqlException ex)
                {
                    Log.Error("DocumentDatabaseManager.Add: Adatbázis hiba. {Message}", ex);
                }
                catch (Exception e)
                {
                    Log.Error("DocumentDatabaseManager.Add: Hiba történt {Message}", e);

                }
                finally
                {
                    CloseConnection(connection);
                }
                Log.Debug("DocumentDatabaseManager.Add: Fájl írása a lemezre");
                byte[] bytes = newObject.Doc.ToByteArray();
                string temppath = Path.GetTempPath();
                string fullpath = path;
                if (File.Exists(fullpath))
                {
                    Log.Warning("DocumentDatabaseManager.Add: Régi fájl felülírva az ujra {Path}", path);
                    File.Delete(fullpath);
                    File.WriteAllBytes(fullpath, bytes);
                }
                else File.WriteAllBytes(fullpath, bytes);

            }

            catch (Exception ex)
            {
                Log.Error("DocumentDatabaseManager.Add: Hiba történt {Message}", ex);

            }

            return newObject;
        }

        private string GetUniqueFileName()
        {
            string path = "";
            path += Directory.GetCurrentDirectory() + "\\Upload\\";
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            path += DateTime.Now.ToString("yyyyMMddHHmmssffff"); 
            path += Path.GetRandomFileName().Replace(".", "");
            return path;
        }
    }
}
