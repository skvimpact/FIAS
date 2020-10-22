using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Fias
{
    public class AttributeReader
    {
        public static U Class<U>(Type type) where U : class =>
            type.
                GetCustomAttributes(false).Where(x => x is U).FirstOrDefault() as U;

        public static IDictionary<PropertyInfo, U> Property2Attribute<U>(Type type) where U : class =>
            new List<PropertyInfo>(
                type.
                    GetProperties(
                        BindingFlags.GetProperty |
                        BindingFlags.Instance |
                        BindingFlags.Public)).
                Select(p => new
                {
                    Property = p,
                    Attribute = p.GetCustomAttributes(false).OfType<U>().FirstOrDefault() as U
                }).
                //Where(a => a.Attribute != null).
                ToDictionary(k => k.Property, k => k.Attribute);
    }
}
