using IktatogRPCServer.Database.Abstract;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCServer.Database.Mysql.Abstract
{
    public abstract class MysqlDatabaseManager<T> : DatabaseManager
    {
        public MysqlDatabaseManager(ConnectionManager connection) : base(connection)
        {

        }

        public override void CloseConnection()
        {
            throw new NotImplementedException();
        }

        public MySqlConnection GetConnection()
        {
            throw new NotImplementedException();
        }

        public override void OpenConnection()
        {
            throw new NotImplementedException();
        }
        abstract public bool Add(T newObjet);
        abstract public bool Update(T modifiedObject);
        abstract public bool Delete(int id);

        abstract public List<T> GetAllData();
        abstract public T GetDataById(int id);
    }
}
