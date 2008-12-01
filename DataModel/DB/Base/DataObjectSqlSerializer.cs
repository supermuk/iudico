using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using IUDICO.DataModel.Common;
using LEX.CONTROLS;

namespace IUDICO.DataModel.DB.Base
{
    internal sealed class SqlSerializationContext
    {
        public SqlSerializationContext(SqlCommand cmd)
        {
            _Cmd = cmd;
            cmd.CommandText = null;
        }

        public string AddParameter(object value)
        {
            object v;
            if (value is Binary)
            {
                v = (value as Binary).ToArray();
            }
            else
            {
                v = value;
            }

            string p;
            if (v == null)
            {
                p = "NULL";
            }
            else
            {
                p = "@P" + ++__ParameterID;
                _Cmd.Parameters.AddWithValue(p, v);
            }
            return p;
        }

        public void Next()
        {
            _SqlBuilder.AppendLine();
        }

        public void Finish()
        {
            _Cmd.CommandText = _SqlBuilder.ToString();
            _SqlBuilder.Remove(0, _SqlBuilder.Length - 1);
        }

        [StringFormatMethod("fmt")]
        public void Write(string fmt, params object[] args)
        {
            _SqlBuilder.AppendFormat(fmt, args);
        }

        public void Write(string str)
        {
            _SqlBuilder.Append(str);
        }

        public static string ExtractTableName(string fullName)
        {
            int ind = fullName.LastIndexOf('.');
            return ind < 0 ? fullName : fullName.Substring(ind + 1);
        }

        private readonly StringBuilder _SqlBuilder = new StringBuilder();
        private readonly SqlCommand _Cmd;
        private int __ParameterID;
    }

    internal static class DataObjectSqlSerializer<TDataObject>
        where TDataObject : IIntKeyedDataObject, new()
    {
        #region ColumnInfo

        public struct ColumnInfo
        {
            public ColumnInfo(Type table, MemberInfo column)
                : this(column.Name,
                       table.GetField(column.GetAtr<ColumnAttribute>().Storage, BindingFlags.Instance | BindingFlags.NonPublic))
            {
            }

            public ColumnInfo([NotNull] string name, [NotNull] FieldInfo storage)
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }
                if (storage == null)
                {
                    throw new ArgumentNullException("storage");
                }

                Name = name;
                Storage = storage;
            }

            public readonly string Name;
            public readonly FieldInfo Storage;
        }

        #endregion

        public static readonly string SelectSql;
        public static readonly ColumnInfo KeyColumn;

        private static readonly List<ColumnInfo> __Columns = new List<ColumnInfo>();
        private static readonly string __ColumnNames;
        private static readonly string __ColumnNamesWithoutID;
        private static readonly string __TableName;
        private static readonly string UpdateSqlHeader;
        public static readonly string DeleteSqlHeader;
        public static readonly string InsertSqlHeader;

        static DataObjectSqlSerializer()
        {
            // Precalculation dataobject information
            Type type = typeof(TDataObject);

            var ta = type.GetAtr<TableAttribute>();
            if (ta == null)
            {
                throw new DMError("{0} must have {1} attribute", type.FullName, typeof(TableAttribute).Name);
            }
            __TableName = SqlSerializationContext.ExtractTableName(ta.Name);

            __Columns.AddRange(
                from p in type.GetProperties()
                where p.HasAtr<ColumnAttribute>()
                select new ColumnInfo(type, p));
            KeyColumn = new ColumnInfo(type, type.GetProperty("ID"));

            __ColumnNames = __Columns.Select(c => SqlUtils.WrapDbId(c.Name)).ConcatComma();
            __ColumnNamesWithoutID = (from c in __Columns where c.Name != "ID" select SqlUtils.WrapDbId(c.Name)).ConcatComma(); 
            SelectSql = "SELECT " + __ColumnNames + " FROM " + SqlUtils.WrapDbId(__TableName);
            UpdateSqlHeader = "UPDATE " + SqlUtils.WrapDbId(__TableName) + " SET ";
            InsertSqlHeader = "INSERT INTO " + SqlUtils.WrapDbId(__TableName) + " " + SqlUtils.WrapArc(__ColumnNamesWithoutID) + " VALUES ";
            DeleteSqlHeader = "DELETE " + SqlUtils.WrapDbId(__TableName) + " WHERE ID";
        }

        public static TDataObject Read(IDataRecord reader)
        {
            var result = new TDataObject();

            for (int i = __Columns.Count - 1; i >= 0; --i)
            {
                AssignValue(__Columns[i].Storage, reader.GetValue(i), result);
            }

            return result;
        }

        public static void AppendUpdateSql([NotNull]SqlSerializationContext context,[NotNull]TDataObject instance)
        {
            context.Write(UpdateSqlHeader + 
                          (from ci in __Columns
                           where ci.Name != "ID"
                           select SqlUtils.WrapDbId(ci.Name) + "=" + context.AddParameter(ci.Storage.GetValue(instance))
                          ).ConcatComma()
                          + " WHERE ID=" + context.AddParameter(instance.ID)
                );
        }

        public static void AppendInsertSql([NotNull]SqlSerializationContext context, [NotNull]TDataObject obj)
        {
            context.Write(InsertSqlHeader);
            context.Write(
                SqlUtils.WrapArc(
                    (from ci in __Columns 
                     where ci.Name != "ID"
                     select context.AddParameter(ci.Storage.GetValue(obj))
                    ).ConcatComma()
                    )
                );
            context.Next();
            context.Write("select SCOPE_IDENTITY()");
        }

        public static void AppendDeleteSql([NotNull]SqlSerializationContext context, int id)
        {
            context.Write(DeleteSqlHeader);
            context.Write("=" + id);
        }

        public static void AppendDeleteSql([NotNull]SqlSerializationContext context, [NotNull]ICollection<int> objs)
        {
            if (objs == null || objs.Count == 0)
            {
                throw new ArgumentException("Collection cannot be empty", "objs");
            }
            context.Write(DeleteSqlHeader + " IN " + SqlUtils.WrapArc(
                                                         objs.ConcatComma()
                                                         ));
        }

        private static void AssignValue(FieldInfo f, object value, TDataObject instance)
        {
            if (value != DBNull.Value)
            {
                object v;

                if (value.GetType() == typeof(byte[]))
                {
                    v = new Binary((byte[]) value);
                }
                else
                {
                    v = value;
                }
                    
                f.SetValue(instance, v);    
            }
            else
            {
                f.SetValue(instance, null);
            }
        }
    }
}