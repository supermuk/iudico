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
using System.Data.Common;
using System.Data.Linq;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.DB
{
    public partial class FxCourseOperations : FxDataObject, IFxDataObject { }

    public partial class FxThemeOperations : FxDataObject, IFxDataObject { }

    public partial class FxStageOperations : FxDataObject, IFxDataObject {}

    public partial class FxRoles : FxDataObject, IFxDataObject {}

    public partial class TblPermissions : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCompiledAnswers : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCompiledQuestions : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCompiledQuestionsData : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCourses : IntKeyedDataObject, IIntKeyedDataObject, INamedDataObject
    {
    }

    public partial class TblCurriculums : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblFiles : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblGroups : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblPages : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblQuestions : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblSampleBusinesObject : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblStages : IntKeyedDataObject, IIntKeyedDataObject, INamedDataObject
    {
    }

    public partial class TblThemes : IntKeyedDataObject, IIntKeyedDataObject, INamedDataObject
    {
    }

    public partial class TblUserAnswers : IntKeyedDataObject, IIntKeyedDataObject {}

    public partial class TblUsers : IntKeyedDataObject, IIntKeyedDataObject { }

    [ManyToManyRelationship(typeof(TblUsers), typeof(FxRoles))]
    public partial class RelUserRoles : RelTable
    {   
    }

    [ManyToManyRelationship(typeof(TblUsers), typeof(TblGroups))]
    public partial class RelUserGroups : RelTable
    {
    }

    public partial class DatabaseModel
    {
        static DatabaseModel()
        {
            if (FIXED_METHOD == null)
            {
                throw new DMError("'Fx' method is not initialized");
            }
            if (LOAD_LIST_METHOD == null)
            {
                throw new DMError("'Load List' method is not initialized");
            }
        }

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
            using (Logger.Scope("Initializing Database Model..."))
            {
                foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (t.IsClass)
                    {
                        ManyToManyRelationshipAttribute mmat;
                        if (t.TryGetAtr(out mmat))
                        {
                            if (!t.IsSubclassOf(typeof (RelTable)))
                                throw new DMError("Class {0} cannot take participation into many-to-many relationship because it is not derived from {1}", t.FullName, typeof(RelTable).Name);
                            if (!t.HasAtr<TableAttribute>())
                                throw new DMError("Class {0} cannot take participation into many-to-many relationship because it is not marked with {1}", t.FullName, typeof(TableAttribute).Name);
                            LookupHelper.RegisterMMLookup(mmat, t);
                        }
                        else if (t.GetInterface(typeof(IIntKeyedDataObject).Name) != null && 
                            t.HasAtr<TableAttribute>())
                        {
                            foreach (var p in t.GetProperties())
                            {
                                AssociationAttribute aa;
                                if (p.TryGetAtr(out aa) &&
                                    p.PropertyType.IsGenericType &&
                                    p.PropertyType.GetGenericTypeDefinition() == typeof(EntitySet<>))
                                {
                                    var pt = p.PropertyType.GetGenericArguments()[0];
                                    if (pt.IsSubclassOf(typeof(DataObject)))
                                    LookupHelper.RegisterLookup(t, pt, aa);
                                }
                            }
                        }
                    }
                }
            }
        }

        public int Insert<TDataObject>(TDataObject obj)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand) cmd);
                DataObjectSqlSerializer<TDataObject>.AppendInsertSql(sc, obj);
                sc.Finish();

                using (var r = cmd.LexExecuteReader())
                {
                    r.Read();
                    var id = Convert.ToInt32(r.GetDecimal(0));
                    DataObjectSqlSerializer<TDataObject>.KeyColumn.Storage.SetValue(obj, id);
                    return id;
                }
            }
        }

        public void Insert<TDataObject>(IList<TDataObject> objs)
            where TDataObject : IIntKeyedDataObject, new()
        {
            if (objs.Count > 0)
            {
                DbConnection cn = GetConnectionSafe();

                var transaction = cn.BeginTransaction();
                try
                {
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        var sc = new SqlSerializationContext((SqlCommand) cmd);
                        foreach (var o in objs)
                        {
                            if (o.ID != 0)
                            {
                                throw new DMError("DataObject has been already inserted.");
                            }
                            DataObjectSqlSerializer<TDataObject>.AppendInsertSql(sc, o);
                            sc.Next();
                        }

                        sc.Finish();
                        using (var r = cmd.LexExecuteReader())
                        {
                            int i = 0, c = objs.Count;

                            while (true)
                            {
                                if (!r.Read())
                                    throw new DMError("Invalid Data Reader");
                                int id = Convert.ToInt32(r.GetDecimal(0));
                                DataObjectSqlSerializer<TDataObject>.KeyColumn.Storage.SetValue(objs[i], id);
                                ++i;
                                if (i < c)
                                    r.NextResult();
                                else
                                    break;
                            }
                        }
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
        }

        public void Update<TDataObject>(TDataObject obj)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand) cmd);
                DataObjectSqlSerializer<TDataObject>.AppendUpdateSql(sc, obj);
                sc.Finish();
                cmd.LexExecuteNonQuery();
            }
        }

        public void Update<TDataObject>(IList<TDataObject> objs)
            where TDataObject : IIntKeyedDataObject, new()
        {
            if (objs.Count > 0)
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
                            DataObjectSqlSerializer<TDataObject>.AppendUpdateSql(sc, o);
                            sc.Next();
                        }
                        sc.Finish();

                        cmd.Transaction = transaction;
                        cmd.LexExecuteNonQuery();
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
        }

        public TDataObject Load<TDataObject>(int id)
            where TDataObject : IIntKeyedDataObject, new()
        {
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                cmd.CommandText = DataObjectSqlSerializer<TDataObject>.SelectSql + " WHERE [ID] = " + id;
                using (var r = cmd.LexExecuteReader())
                {
                    if (r.Read())
                    {
                        return DataObjectSqlSerializer<TDataObject>.Read(r);
                    }
                    throw new DMError("Invalid object ID: {0}", id);
                }
            }
        }

        public IList<TDataObject> Load<TDataObject>(IList<int> ids)
            where TDataObject : IIntKeyedDataObject, new()
        {
            if (ids.Count == 0)
            {
                return new TDataObject[0];
            }

            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var result = new List<TDataObject>(ids.Count);
                cmd.CommandText = DataObjectSqlSerializer<TDataObject>.SelectSql + " WHERE ID IN (" + ids.ConcatComma() + ")";
                using (var r = cmd.LexExecuteReader())
                {
                    while (r.Read())
                    {
                        result.Add(DataObjectSqlSerializer<TDataObject>.Read(r));
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
            using (var cmd = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand)cmd);
                DataObjectSqlSerializer<TDataObject>.AppendDeleteSql(sc, id);
                sc.Finish();
                cmd.LexExecuteNonQuery();
            }
        }

        public void Delete<TDataObject>(IList<int> ids)
            where TDataObject : IIntKeyedDataObject, new()
        {
            if (ids.Count > 0)
            {
                using (var cmd = GetConnectionSafe().CreateCommand())
                {
                    var sc = new SqlSerializationContext((SqlCommand) cmd);
                    DataObjectSqlSerializer<TDataObject>.AppendDeleteSql(sc, ids);
                    sc.Finish();
                    cmd.LexExecuteNonQuery();
                }
            }
        }

        public void Delete<TDataObject>(IList<TDataObject> objs)
            where TDataObject : IIntKeyedDataObject, new()
        {
            Delete<TDataObject>(new List<int>(objs.Select(o => o.ID)));
        }

        public ReadOnlyCollection<TFxDataObject> Fx<TFxDataObject>()
            where TFxDataObject : FxDataObject
        {
            return FxObjectsStorage<TFxDataObject>.Items;
        }

        public List<int> LookupIds<TDataObject>(IIntKeyedDataObject owner)
            where TDataObject : DataObject
        {
            using (var c = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand)c);
                LookupHelper.AppendLookupSql(sc, owner, typeof(TDataObject));
                sc.Finish();
                return c.FullReadInts();
            }
        }

        public List<int> LookupMany2ManyIds<TDataObject>(IIntKeyedDataObject firstPart)
        {
            using (var c = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand) c);
                LookupHelper.AppendMMLookupSql(sc, firstPart, typeof(TDataObject));
                sc.Finish();
                return c.FullReadInts();
            }
        }

        public void Link(IIntKeyedDataObject do1, IIntKeyedDataObject do2)
        {
            using (var c = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand)c);
                LookupHelper.AppendMMLinkSql(sc, do1, do2);
                sc.Finish();
                c.LexExecuteNonQuery();
            }           
        }

        public void UnLink(IIntKeyedDataObject do1, IIntKeyedDataObject do2)
        {
            using (var c = GetConnectionSafe().CreateCommand())
            {
                var sc = new SqlSerializationContext((SqlCommand)c);
                LookupHelper.AppendMMUnLinkSql(sc, do1, do2);
                sc.Finish();
                c.LexExecuteNonQuery();
            }   
        }

        public static readonly MethodInfo FIXED_METHOD = typeof(DatabaseModel).GetMethod("Fx");
        public static readonly MethodInfo LOAD_LIST_METHOD = typeof(DatabaseModel).GetMethod("Load", new[] { typeof(IList<int>) });

        #region FxObjectsStorage

        private static class FxObjectsStorage<T>
            where T: FxDataObject
        {
            static FxObjectsStorage()
            {
                Items = new ReadOnlyCollection<T>(new List<T>(ServerModel.DB.GetTable<T>()));
            }

            public static readonly ReadOnlyCollection<T> Items;
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
