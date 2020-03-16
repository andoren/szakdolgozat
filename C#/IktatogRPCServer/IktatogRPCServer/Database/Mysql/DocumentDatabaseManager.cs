using Google.Protobuf;
using Iktato;
using IktatogRPCServer.Database.Mysql.Abstract;
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

        public override bool Add(Document newObjet)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Document> GetAllData()
        {
            throw new NotImplementedException();
        }

        public override Document GetDataById(int id)
        {
			Document document = new Document();
			string path = GetDocumentInfoByIdo(ref document,id);
			document.Doc = ByteString.CopyFrom(GetBytesFromFile(path));
			
			return document;
        }
		/// <summary>
		/// Beállítja a nevet és a tipusát a fájlnak és vissza tér a fájl elérési útvonalával.
		/// </summary>
		/// <param name="document">A küldeni kívánt dokumentum</param>
		/// <param name="id">Dokumentum id-je</param>
		/// <returns></returns>
		private string GetDocumentInfoByIdo(ref Document document,int id) {
			document.Name = "December";
			document.Type = "xlsx";
			return $"{ Directory.GetCurrentDirectory()}\\Upload\\December.xlsx";
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
				System.Windows.MessageBox.Show(e.Message);
			}
			return (ba1);
		}
		public override bool Update(Document modifiedObject)
        {
            throw new NotImplementedException();
        }
    }
}
