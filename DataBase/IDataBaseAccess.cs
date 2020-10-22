using System.Collections.Generic;

namespace Fias.DataBase
{
    public interface IDataBaseAccess
    {
        int? BatchSize { get; set; }
        void BulkCopy<T>(IEnumerable<T> items, Metadata<T> metadata);
    }
}
