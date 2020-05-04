using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IktatogRPCServer.Database.Interfaces
{
    public interface IDatabaseConnectionManager
    {
        ConnectionManager ConnectionManager { get; set; }
        void OpenConnection(object connection);
        void CloseConnection(object connection);
    }
}
