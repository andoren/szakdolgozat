using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Abstract
{
    public abstract class DatabaseManager<T>
    {
        protected ConnectionManager connectionManager;
        private DatabaseManager() { }
        public DatabaseManager(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }


        abstract public void OpenConnection();
        abstract public void CloseConnection();
        abstract public bool Add(T newObjet);
        abstract public bool Update(T modifiedObject);
        abstract public bool Delete(int id);

        abstract public List<T> GetAllData();
        abstract public T GetDataById(int id);
    }
}
