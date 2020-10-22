using Fias.DataBase;
using System.Collections.Generic;

namespace Fias.Repos
{
    public class Repo<T> : IRepo<T> where T : new()
    {
        public Metadata<T> Metadata { get; set; }
        public IDataBaseAccess DataBaseAccess { get; }

        public Repo(IDataBaseAccess dataBaseAccess)
        {
            DataBaseAccess = dataBaseAccess;
            Metadata = new Metadata<T>();
        }

        public Repo(IDataBaseAccess dataBaseAccess, string tableName, string schema) : this(dataBaseAccess)
        {
            Metadata.TableName = tableName;
            Metadata.Schema = schema;
        }

        public virtual void AddRange(IEnumerable<T> items) =>
            DataBaseAccess.BulkCopy(items, Metadata);
    }
}
