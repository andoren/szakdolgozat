using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Abstract
{
    public abstract class DatabaseManager
    {
        ConnectionManager connectionManager;
        public DatabaseManager(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }


        abstract public void OpenConnection();
        abstract public void CloseConnection();
    }
}
