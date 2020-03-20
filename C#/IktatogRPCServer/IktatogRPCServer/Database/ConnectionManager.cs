using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database
{
    public class ConnectionManager
    {
      public string ConnectionString
        {
            get
            {
                try
                {
                    //string server = "localhost";
                    //string database = "iktato";
                    //string uid = "root";
                    //string password = "k35Vl1o1L5";
                    //string connectionString;
                    //connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                    //database + ";" + "CharSet = utf8;" + "UID=" + uid + ";" + "PASSWORD=" + password + "; Connect Timeout=5";

                    return ConfigurationManager.ConnectionStrings["Test"].ConnectionString; 
                }
                catch (Exception e) {
                    Logger.Logging.LogToScreenAndFile($"{nameof(this.ConnectionString)} -ban/ben hiba történt! {e.Message}");
                }
                return "";
            }
        }
    }
}
