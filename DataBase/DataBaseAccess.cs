using System.Data;
using System.Data.Common;

namespace Fias.DataBase
{
    public abstract class DataBaseAccess<TConnection, TCommand>
        where TConnection : DbConnection, new()
        where TCommand : DbCommand, new()
    {
        public string ConnectionString { get; }

        protected TConnection _connection
        {
            get
            {
                var connection = new TConnection {
                    ConnectionString = ConnectionString
                };
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                return connection;
            }
        }

        private TConnection _conn { get; }

        protected TConnection _c
        {
            get
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
                return _conn;
            }
        }

        private TCommand _command(DbConnection connection, string commandText) =>
            new TCommand {
                Connection = connection,
                CommandText = commandText,
                CommandTimeout = 600
            };

        public DataBaseAccess(string connectionString) {
            ConnectionString = connectionString;
            ////!!!!
            _conn = new TConnection
            {
                ConnectionString = ConnectionString
            };
        }
    }
}
