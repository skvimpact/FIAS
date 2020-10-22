using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Linq;
using Fias.DataBase;

namespace Fias.Extensions
{
    public static class IEnumerableExtensions
    {
        public static DbDataReaderFactory<T> ToDataReader<T>(this IEnumerable<T> list, Metadata<T> metadata) =>
            new DbDataReaderFactory<T>(list, metadata);
    }

    public class DbDataReaderFactory<T> : DbDataReader, IDataReader
    {
        private IEnumerator<T> list = null;
        private List<PropertyInfo> properties;
        private Dictionary<string, int> nameLookup = new Dictionary<string, int>();

        public DbDataReaderFactory(IEnumerable<T> list, Metadata<T> metadata)
        {
            this.list = list.GetEnumerator();
            properties = metadata.Column2Property.Values.ToList();
            Enumerable.
                Range(0, properties.Count).
                ToList().
                ForEach(i => nameLookup[properties[i].Name] = i);
        }

        #region IDataReader Members

        public override void Close()
        {
            closed = true;
            list.Dispose();
        }

        public override int Depth => 0;

        public override DataTable GetSchemaTable()
        {
            var dt = new DataTable();
            properties.ForEach(p => dt.Columns.Add(new DataColumn(p.Name, p.PropertyType)));
            return dt;
            //throw new NotImplementedException();
        }
        private bool closed;
        public override bool IsClosed => closed;

        public override bool HasRows => true;

        public override bool NextResult()
        {
            throw new NotImplementedException();
        }

        public override bool Read()
        {
            if (IsClosed)
                throw new InvalidOperationException("DataReader is closed");

            return list.MoveNext();
        }

        public override int RecordsAffected => -1;
        //{
        //    get { throw new NotImplementedException(); }
        //}

        #endregion

        #region IDisposable Members

        public new void Dispose()
        {
            Close();
        }

        #endregion

        #region IDataRecord Members

        public override int FieldCount => properties.Count;

        public override bool GetBoolean(int i)
        {
            return (bool)GetValue(i);
        }

        public override byte GetByte(int i)
        {
            return (byte)GetValue(i);
        }

        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public override char GetChar(int i)
        {
            return (char)GetValue(i);
        }

        public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public new IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public override string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetDateTime(int i)
        {
            return (DateTime)GetValue(i);
        }

        public override decimal GetDecimal(int i)
        {
            return (decimal)GetValue(i);
        }

        public override double GetDouble(int i)
        {
            return (double)GetValue(i);
        }

        public override Type GetFieldType(int i)
        {
            return properties[i].PropertyType;
        }

        public override float GetFloat(int i)
        {
            return (float)GetValue(i);
        }

        public override Guid GetGuid(int i)
        {
            return (Guid)GetValue(i);
        }

        public override short GetInt16(int i)
        {
            return (short)GetValue(i);
        }

        public override int GetInt32(int i)
        {
            return (int)GetValue(i);
        }

        public override long GetInt64(int i)
        {
            return (long)GetValue(i);
        }

        public override string GetName(int i)
        {
            return properties[i].Name;
        }

        public override int GetOrdinal(string name)
        {
            if (nameLookup.ContainsKey(name))
            {
                return nameLookup[name];
            }
            else
            {
                return -1;
            }
        }

        public override string GetString(int i)
        {
            return (string)GetValue(i);
        }

        public override object GetValue(int i)
        {
            return properties[i].GetValue(list.Current, null);
        }

        public override int GetValues(object[] values)
        {
            int getValues = Math.Max(FieldCount, values.Length);

            for (int i = 0; i < getValues; i++)
            {
                values[i] = GetValue(i);
            }

            return getValues;
        }

        public override bool IsDBNull(int i)
        {
            return GetValue(i) == null;
        }

        public override IEnumerator GetEnumerator()
        {
            return this.list;
        }

        public override object this[string name]
        {
            get
            {
                return GetValue(GetOrdinal(name));
            }
        }

        public override object this[int i]
        {
            get
            {
                return GetValue(i);
            }
        }

        #endregion
    }
}
