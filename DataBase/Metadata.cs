using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Fias.DataBase
{
    public class Metadata<F>
    {
        public string TableName { get; set; }
        public string Schema { get; set; }
        public IDictionary<string, PropertyInfo> Column2Property { get; set; }
        public Metadata()
        {
            TableName = AttributeReader.Class<TableAttribute>(typeof(F))?.Name;
            Schema = AttributeReader.Class<TableAttribute>(typeof(F))?.Schema;
            var p2c = AttributeReader.Property2Attribute<ColumnAttribute>(typeof(F));
            Column2Property = p2c.ToDictionary(kvp => kvp.Value?.Name ?? kvp.Key.Name, kvp => kvp.Key);
        }
    }
}
