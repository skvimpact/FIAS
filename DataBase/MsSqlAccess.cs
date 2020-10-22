using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Fias.Extensions;

namespace Fias.DataBase
{
    public class MsSqlAccess : DataBaseAccess<SqlConnection, SqlCommand>, IDataBaseAccess
    {
        public int? BatchSize { get; set; }
        public MsSqlAccess(string connectionString) : base(connectionString) { }
        public void BulkCopy<T>(IEnumerable<T> items, Metadata<T> metadata)
        {
            if (items.Count() == 0) return;
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_c as SqlConnection) {
                DestinationTableName = (metadata.Schema != null) ?
                    $"[{metadata.Schema}].[{metadata.TableName}]" :
                    $"[{metadata.TableName}]",
                BulkCopyTimeout = 1800,
                BatchSize = BatchSize ?? 50000,
                EnableStreaming = true})
            {
                metadata.Column2Property.ToList().
                    ForEach(i => bulkCopy.ColumnMappings.Add(i.Value.Name, i.Key));
                bulkCopy.WriteToServer(items.ToDataReader(metadata));
            }
        }
    }
}
