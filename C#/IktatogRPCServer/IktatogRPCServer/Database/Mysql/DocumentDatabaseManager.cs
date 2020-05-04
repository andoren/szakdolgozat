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
using IktatogRPCServer.Database.Interfaces;
using IktatogRPCServer.Helpers;

namespace IktatogRPCServer.Database.Mysql
{
    class DocumentDatabaseManager : MysqlDatabaseManager, IManageDocument
    {
        private UploadedFileHandler _fileHandler = new UploadedFileHandler();
        public UploadedFileHandler FileHandler
        {
            get
            {
                return _fileHandler;
            }
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
            Log.Debug("DocumentDatabaseManager.GetDocumentInfosByIkonyv: Bemenő paraméterek beolvasása és hozzáadása a paraméter listához. Id: {Id}", ikonyv.Id);
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

        public Document AddDocument(Document newObject, User user)
        {
            {
                Log.Debug("DocumentDatabaseManager.Add: Mysqlcommand előkészítése.");
                MySqlCommand command = new MySqlCommand();
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                Log.Debug("DocumentDatabaseManager.Add: Új fájlnév generálása.");
                string path = FileHandler.GetUniqueFileName();
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
        }

        public Answer DeleteDocument(int id, User user)
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

        public Document GetDocumentByInfo(DocumentInfo documentInfo)
        {
            Log.Debug("DocumentDatabaseManager.GetDocumentFromInfo: Adatok beolvasása.");
            Document document = new Document();
            document.Name = documentInfo.Name;
            document.Type = documentInfo.Type;
            document.Path = documentInfo.Path;
            document.Doc = ByteString.CopyFrom(FileHandler.GetBytesFromFile(document.Path));
            return document;
        }
    }
}
