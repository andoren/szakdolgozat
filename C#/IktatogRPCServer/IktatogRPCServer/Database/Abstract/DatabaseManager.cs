using Iktato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IktatogRPCServer.Database.Abstract
{
    public abstract class DatabaseManager<T> where T: new ()
    {
        protected ConnectionManager connectionManager;
        private DatabaseManager() { }
        public DatabaseManager(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }
        abstract public void OpenConnection(object connection);
        abstract public void CloseConnection(object connection);
        abstract public T Add(NewTorzsData newObject, User user);
        virtual public T Add(T newObject, User user) { return new T(); }
        abstract public Answer Update(T modifiedObject);
        abstract public Answer Delete(int id,User user);
        abstract public List<T> GetAllData(object filter);
    }
}
