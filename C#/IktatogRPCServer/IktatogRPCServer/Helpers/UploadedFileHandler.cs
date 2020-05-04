using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Helpers
{
    class UploadedFileHandler
    {
        public byte[] GetBytesFromFile(string FileName)
        {
            byte[] ba1 = new byte[1];
            FileStream fs;
            BinaryReader br;
            try
            {
                Log.Debug("DocumentDatabaseManager.GetBytesFromFile: Filestream megnyitás az elérési úttal: {FileName}", FileName);
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
        public string GetUniqueFileName()
        {
            string path = "";
            path += Directory.GetCurrentDirectory() + "\\Upload\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += DateTime.Now.ToString("yyyyMMddHHmmssffff");
            path += Path.GetRandomFileName().Replace(".", "");
            return path;
        }
    }
}
