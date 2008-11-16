using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using IUDICO.DataModel.Common;
using System.Data;
using LEX.CONTROLS;
using System.Text;
using System.Data.Common;

namespace IUDICO.DataModel.DB
{
    public class DBEnumAttribute : Attribute
    {
        public readonly string TableName;

        public DBEnumAttribute(string tableName)
        {
            TableName = tableName;
        }
    }

    public interface IIntKeyedDataObject
    {
        int ID { get; }
    }

    public class DBEnum<T>
        where T : struct
    {
        static DBEnum()
        {
            Values = new ReadOnlyCollection<string>(new List<string>(
                from f in typeof(T).GetFields()
                where !f.IsSpecialName
                select f.Name));
        }

        public static readonly ReadOnlyCollection<string> Values;
    }

    public abstract class DataObject
    {
    }

    public partial class TblPermissions : IIntKeyedDataObject
    {
    }

    public partial class TblCompiledAnswers : IIntKeyedDataObject
    {
    }

    public partial class TblCompiledQuestions : IIntKeyedDataObject
    {
    }

    public partial class TblCourses : IIntKeyedDataObject
    {
    }

    public partial class TblCurriculums : IIntKeyedDataObject
    {
    }

    public partial class TblFiles : IIntKeyedDataObject {}

    public partial class TblGroups : IIntKeyedDataObject {}

    public partial class TblPages : IIntKeyedDataObject {}

    public partial class TblQuestions : IIntKeyedDataObject {}

    public partial class TblSampleBusinesObject : IIntKeyedDataObject {}

    public partial class TblStages : IIntKeyedDataObject {}

    public partial class TblThemes : IIntKeyedDataObject {}

    public partial class TblUserAnswers : IIntKeyedDataObject {}

    public partial class TblUsers : IIntKeyedDataObject { }

    public partial class DatabaseModel
    {
        public DbConnection GetConnectionSafe()
        {
            if (Connection.State != ConnectionState.Open)
            {
                throw new DMError("Incorrect connection state");
            }
            return Connection;
        }

        public void Initialize()
        {
        }

        public void Insert<TDataObject>(TDataObject obj)
            where TDataObject : IIntKeyedDataObject, new()
        {
            throw new NotImplementedException();
        }

        public void Insert<TDataObject>(IList<TDataObject> objs)
            where TDataObject : IIntKeyedDataObject, new()
        {
            throw new NotImplementedException();
        }

        public void Update<TDataObject>(TDataObject obj)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand) cmd);
                CompiledDataObjectSqlHelper<TDataObject>.AppendUpdateSql(sc, obj);
                cmd.CommandText = sc.GetSql();
                cmd.LexExecuteNonQuery();
            }
        }

        public void Update<TDataObject>(IList<TDataObject> objs)
            where TDataObject : IIntKeyedDataObject, new()
        {
            var connection = GetConnectionSafe();
            var transaction = connection.BeginTransaction();
            try
            {
                using (var cmd = connection.CreateCommand())
                {
                    var sc = new SqlSerializationContext((SqlCommand) cmd);
                    foreach (var o in objs)
                    {
                        CompiledDataObjectSqlHelper<TDataObject>.AppendUpdateSql(sc, o);
                        sc.Next();
                    }

                    cmd.Transaction = transaction;
                    cmd.CommandText = sc.GetSql();
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public TDataObject Load<TDataObject>(int id)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                cmd.CommandText = CompiledDataObjectSqlHelper<TDataObject>.SelectSql + " WHERE [ID] = " + id;
                using (var r = cmd.ExecuteReader())
                {
                    return CompiledDataObjectSqlHelper<TDataObject>.Read(r);
                }
            }
        }

        public IList<TDataObject> Load<TDataObject>(IList<int> ids)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var result = new List<TDataObject>(ids.Count);
                cmd.CommandText = CompiledDataObjectSqlHelper<TDataObject>.SelectSql + " WHERE ID IN (" + ids.ConcatComma() + ")";
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        result.Add(CompiledDataObjectSqlHelper<TDataObject>.Read(r));
                    }
                }
                if (result.Count != ids.Count)
                {
                    throw new DMError("Count of populated objects ({0}) lower than requested to extract {1}", result.Count, ids.Count);
                }
                return result;
            }
        }

        public void Delete<TDataObject>(int id)
            where TDataObject : IIntKeyedDataObject, new()
        {
            throw new NotImplementedException();
        }

        public void Delete<TDataObject>(IList<int> ids)
            where TDataObject : IIntKeyedDataObject, new()
        {
            throw new NotImplementedException();
        }

        #region CompiledDataObjectSqlHelper

        private struct ColumnInfo
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

        private static class CompiledDataObjectSqlHelper<TDataObject>
            where TDataObject : IIntKeyedDataObject, new()
        {
            public static readonly string SelectSql;

            private static readonly List<ColumnInfo> __Columns = new List<ColumnInfo>();
            private static readonly string __ColumnNames;
            private static readonly string __ColumnNamesWithoutID;
            private static readonly string __TableName;
            private static readonly string UpdateSqlHeader;
            //public static readonly string DeleteSql;
            //public static readonly string InsertSql;

            static CompiledDataObjectSqlHelper()
            {
                // Precalculation dataobject information
                Type type = typeof(TDataObject);

                var ta = type.GetAtr<TableAttribute>();
                if (ta == null)
                {
                    throw new DMError("{0} must have {1} attribute", type.FullName, typeof(TableAttribute).Name);
                }
                __TableName = ExtractTableName(ta.Name);

                __Columns.AddRange(
                    from p in type.GetProperties()
                    where p.HasAtr<ColumnAttribute>()
                    select new ColumnInfo(type, p));

                __ColumnNames = __Columns.Select(c => SqlUtils.WrapDbId(c.Name)).ConcatComma();
                __ColumnNamesWithoutID = (from c in __Columns where c.Name != "ID" select SqlUtils.WrapDbId(c.Name)).ConcatComma(); 
                SelectSql = "SELECT " + __ColumnNames + " FROM " + SqlUtils.WrapDbId(__TableName);
                UpdateSqlHeader = "UPDATE " + SqlUtils.WrapDbId(__TableName) + " SET ";
                //InsertSql = "INSERT INTO" + SqlUtils.WrapDbId(__TableName) + SqlUtils.WrapDbId(__ColumnNamesWithoutID) + " VALUES ";

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

            public static void AppendUpdateSql(SqlSerializationContext context, IIntKeyedDataObject instance)
            {
                context.Write(UpdateSqlHeader + 
                    (from ci in __Columns
                     where ci.Name != "ID"
                     select SqlUtils.WrapDbId(ci.Name) + "=@" + context.AddParameter(ci.Storage.GetValue(instance))
                     ).ConcatComma()
                     + " WHERE " + "ID = @" + context.AddParameter(instance.ID)
                );
            }

            private static void AssignValue(FieldInfo f, object value, IIntKeyedDataObject instance)
            {
                if (value != DBNull.Value)
                {
                    f.SetValue(instance, value);
                }
            }

            private static string ExtractTableName(string fullName)
            {
                int ind = fullName.LastIndexOf('.');
                return ind < 0 ? fullName : fullName.Substring(ind + 1);
            }
        }

        private sealed class SqlSerializationContext
        {
            public SqlSerializationContext(SqlCommand cmd)
            {
                Cmd = cmd;
            }

            public string AddParameter(object value)
            {
                var p = "P" + ++__ParameterID;
                Cmd.Parameters.AddWithValue(p, value ?? DBNull.Value);
                return p;
            }

            public void Next()
            {
                SqlBuilder.AppendLine();
            }

            public string GetSql()
            {
                return SqlBuilder.ToString();
            }

            [StringFormatMethod("fmt")]
            public void Write(string fmt, params object[] args)
            {
                SqlBuilder.AppendFormat(fmt, args);
            }

            public void Write(string str)
            {
                SqlBuilder.Append(str);
            }

            private readonly StringBuilder SqlBuilder = new StringBuilder();
            private readonly SqlCommand Cmd;
            private int __ParameterID;
        }

        #endregion
    }

    [DBEnum("fxRoles")]
    public enum FX_ROLE
    {
        STUDENT,
        LECTOR,
        TRAINER,
        ADMIN,
        SUPER_ADMIN
    }
}
