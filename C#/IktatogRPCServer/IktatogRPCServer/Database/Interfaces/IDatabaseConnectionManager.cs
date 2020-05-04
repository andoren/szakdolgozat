using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IktatogRPCServer.Database.Interfaces
{
    public interface IDatabaseConnectionManager<T>
    {     
        ConnectionManager ConnectionManager { get;  }
        void OpenConnection(T connection);
        void CloseConnection(T connection);
        T GetConnection();
    }
}
