using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Caching;
using IUDICO.DataModel.Common;
using LEX.CONTROLS;

namespace IUDICO.DataModel.DB.Base
{
    public sealed class SqlSerializationContext
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

    internal class DataObjectInfo<TDataObject>
        where TDataObject : IDataObject, new()
    {
        public static readonly string TableName;
        public static readonly List<ColumnInfo> Columns;
        public static readonly string ColumnNames;
        public static readonly string SelectSql;

        private static SqlConnection __CacheDepCommandConnection;

        public static CacheDependency CacheDependency
        {
            get
            {
                if (__CacheDepCommandConnection == null)
                {
                    __CacheDepCommandConnection = (SqlConnection)ServerModel.DB.GetConnectionSafe();
                }
                var c = new SqlCommand(string.Format("SELECT * FROM [{0}]", TableName), __CacheDepCommandConnection);
                return new SqlCacheDependency(c);
            }
        }

        static DataObjectInfo()
        {
            // Precalculation dataobject information
            Type type = typeof(TDataObject);
            var ta = type.GetAtr<TableAttribute>();
            TableName = SqlSerializationContext.ExtractTableName(ta.Name);

            Columns = new List<ColumnInfo>(
                from p in type.GetProperties()
                where p.HasAtr<ColumnAttribute>()
                select new ColumnInfo(type, p));
            ColumnNames = Columns.Select(c => SqlUtils.WrapDbId(c.Name)).ConcatComma();
            SelectSql = "SELECT " + ColumnNames + " FROM " + SqlUtils.WrapDbId(TableName);
        }

        public static void AppendQuerySql([NotNull] SqlSerializationContext context, [CanBeNull]IDBCondition cond)
        {
            context.Write(SelectSql);
            if (cond != null)
            {
                context.Write(" WHERE ");
                cond.Write(context);
            }
        }

        public static List<TDataObject> FullRead(IDataReader reader, int estimatedCount)
        {
            var res = new List<TDataObject>(estimatedCount > 0 ? estimatedCount : 5);
            while (reader.Read())
            {
                res.Add(Read(reader));
            }
            return res;
        }

        public static TDataObject Read(IDataRecord reader)
        {
            var result = new TDataObject();
            for (int i = Columns.Count - 1; i >= 0; --i)
            {
                AssignValue(Columns[i].Storage, reader.GetValue(i), result);
            }
            return result;
        }

        private static void AssignValue(FieldInfo f, object value, TDataObject instance)
        {
            if (value != DBNull.Value)
            {
                object v = value.GetType() == typeof(byte[]) ? new Binary((byte[])value) : value;
                f.SetValue(instance, v);
            }
            else
            {
                f.SetValue(instance, null);
            }
        }

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
    }

    internal static class DataObjectSqlSerializer<TDataObject>
        where TDataObject : IIntKeyedDataObject, new()
    {
        public static readonly DataObjectInfo<TDataObject>.ColumnInfo KeyColumn;
        private static readonly string __ColumnNamesWithoutID;

        private static readonly string __SelectRangeSqlTemplate_1;
        private const string __SelectRangeSqlTemplate_2 = @"SELECT * FROM OrderedObjects WHERE RowNumber BETWEEN {0} AND {1}";
        private static readonly string UpdateSqlHeader;
        public static readonly string DeleteSqlHeader;
        public static readonly string InsertSqlHeader;

        static DataObjectSqlSerializer()
        {
            // Precalculation dataobject information
            Type type = typeof(TDataObject);
            KeyColumn = new DataObjectInfo<TDataObject>.ColumnInfo(type, type.GetProperty("ID"));
            __ColumnNamesWithoutID = (from c in DataObjectInfo<TDataObject>.Columns where c.Name != "ID" select SqlUtils.WrapDbId(c.Name)).ConcatComma();
            string tName = DataObjectInfo<TDataObject>.TableName;
            UpdateSqlHeader = "UPDATE " + SqlUtils.WrapDbId(tName) + " SET ";
            InsertSqlHeader = "INSERT INTO " + SqlUtils.WrapDbId(tName) + " " + SqlUtils.WrapArc(__ColumnNamesWithoutID) + " VALUES ";
            DeleteSqlHeader = "DELETE " + SqlUtils.WrapDbId(tName) + " WHERE ID";
            __SelectRangeSqlTemplate_1 = string.Format(@"WITH OrderedObjects AS
(SELECT {0}, ROW_NUMBER() OVER (ORDER BY ID) AS 'RowNumber'
    FROM {1} as objs)", DataObjectInfo<TDataObject>.ColumnNames, tName);
        }

 

        public static void AppendSelectRangeSql([NotNull] SqlSerializationContext context, int from, int to, IDBCondition st)
        {
            context.Write(__SelectRangeSqlTemplate_1, context.AddParameter(from), context.AddParameter(to));
            if (st != null)
                st.Write(context);
            context.Write(Environment.NewLine);
            context.Write(__SelectRangeSqlTemplate_2);
        }

        public static void AppendUpdateSql([NotNull]SqlSerializationContext context,[NotNull]TDataObject instance)
        {
            context.Write(UpdateSqlHeader +
                          (from ci in DataObjectInfo<TDataObject>.Columns
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
                    (from ci in DataObjectInfo<TDataObject>.Columns 
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
    }
}