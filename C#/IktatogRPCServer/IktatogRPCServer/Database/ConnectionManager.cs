using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

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
                    Log.Debug("Connectionstring kilvasása.");
                    return ConfigurationManager.ConnectionStrings["Test"].ConnectionString; 
                }
                catch (Exception e) {
                    Log.Error(nameof(this.ConnectionString)+ "-ban/ben hiba történt! {Message}",e);
                }
                return "";
            }
        }
    }
}
