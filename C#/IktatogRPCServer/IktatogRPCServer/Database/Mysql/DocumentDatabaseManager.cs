using Google.Protobuf;
using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
using IktatogRPCServer.Logger;
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
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = GetConnection();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "deletedoc";
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

        public override List<Document> GetAllData()
        {
            throw new NotImplementedException();
        }

        public override Document GetDataById(int id)
        {
            throw new NotImplementedException();
        }
        public  Document GetDataById(DocumentInfo documentInfo)
        {
            Document document = GetDocumentFromInfo(documentInfo);          
            return document;
        }
        /// <summary>
        /// Beállítja a nevet és a tipusát a fájlnak és vissza tér a fájl elérési útvonalával.
        /// </summary>
        /// <param name="document">A küldeni kívánt dokumentum</param>
        /// <param name="id">Dokumentum id-je</param>
        /// <returns></returns>
        private Document GetDocumentFromInfo(DocumentInfo documentInfo) {
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

				fs = new FileStream(FileName, FileMode.Open);
				long lFileSize = fs.Length;
				br = new BinaryReader(fs);
				ba1 = br.ReadBytes((Int32)lFileSize);

				br.Close();
				fs.Close();
			}
			catch (UnauthorizedAccessException)
			{
				string temppath = Path.GetTempPath();
				string fullpath = temppath + $"{FileName}";
				if (File.Exists(fullpath))
				{
					File.Delete(fullpath);
					File.Copy(FileName, fullpath);
				}
				else File.Copy(FileName, fullpath);
				fs = new FileStream(fullpath, FileMode.Open);
				long lFileSize = fs.Length;

				br = new BinaryReader(fs);
				ba1 = br.ReadBytes((Int32)lFileSize);

				br.Close();
				fs.Close();

			}
			catch (Exception e)
			{
                Logging.LogToScreenAndFile(e.Message);
			}
			return (ba1);
		}
		public override Answer Update(Document modifiedObject)
        {
            throw new NotImplementedException();
        }

        public List<DocumentInfo> GetDocumentInfosByIkonyv(Ikonyv ikonyv) {
            List<DocumentInfo> documentInfos = new List<DocumentInfo>();
     

                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "getdocsbyikonyvid";
                command.Parameters.AddWithValue("@ikonyvid_b", ikonyv.Id);
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
            MySqlCommand command = new MySqlCommand();

            string path = GetUniqueFileName();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "adddoc";
            //IN PARAMETERS
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
            MySqlParameter newid = new MySqlParameter()
            {
                ParameterName = "@newid_b",
                DbType = System.Data.DbType.Int32,

                Direction = System.Data.ParameterDirection.Output
            };

            command.Parameters.Add(newid);
            try
            {
                MySqlConnection connection = GetConnection();
                command.Connection = connection;
                OpenConnection(connection);
                try
                {
                    command.ExecuteNonQuery();
                    newObject.Id = int.Parse(command.Parameters["@newid_b"].Value.ToString());
                    newObject.Path = path;
                }
                catch (MySqlException ex)
                {
                    Logging.LogToScreenAndFile("Error code: " + ex.Code + " Error message: " + ex.Message);
                }
                catch (Exception e)
                {
                    Logging.LogToScreenAndFile(e.Message);

                }
                finally {
                    CloseConnection(connection);
                }

                byte[] bytes = newObject.Doc.ToByteArray();
                string temppath = Path.GetTempPath();
                string fullpath = path;
                if (File.Exists(fullpath))
                {
                    File.Delete(fullpath);
                    File.WriteAllBytes(fullpath, bytes);
                }
                else File.WriteAllBytes(fullpath, bytes);

            }

            catch (Exception ex)
            {
                Logging.LogToScreenAndFile(ex.Message);

            }
        
            return newObject;
        }

        private string GetUniqueFileName()
        {
            string path = "";
            path += Directory.GetCurrentDirectory() + "\\Upload\\";
            path += DateTime.Today.ToShortDateString();
            path += Path.GetRandomFileName().Replace(".", "");
            return path;
        }
    }
}
