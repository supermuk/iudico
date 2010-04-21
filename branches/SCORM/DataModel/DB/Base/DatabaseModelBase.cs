using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web.Caching;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;

namespace IUDICO.DataModel.DB
{
    public interface IDBScope : IDisposable
    {
        SqlConnection Connection { get; }
    }

    public partial class DatabaseModel : IDBOperator
    {
        static DatabaseModel()
        {
            if (FIXED_METHOD == null)
            {
                throw new DMError(Translations.DatabaseModel_DatabaseModel__Fx__method_is_not_initialized);
            }
            if (LOAD_LIST_METHOD == null)
            {
                throw new DMError(Translations.DatabaseModel_DatabaseModel__Load_List__method_is_not_initialized);
            }
            if (QUERY_METHOD == null)
            {
                throw new DMError(Translations.DatabaseModel_DatabaseModel_Unable_to_find__Query__method);
            }
            if (LOAD_METHOD == null)
            {
                throw new DMError(Translations.DatabaseModel_DatabaseModel_Unable_to_find_Load_method);
            }
            if (DELETE_METHOD == null)
            {
                throw new DMError(Translations.DatabaseModel_DatabaseModel_Unable_to_find_Delete_method);
            }
        }

        public void Initialize(Cache c)
        {
            Initialize();
            if (c == null)
                throw new ArgumentNullException("c");
            Cache = c;
        }

        public Cache Cache { get; private set; }

        private void Initialize()
        {
            using (DBScope(Translations.DatabaseModel_Initialize_Initializing_Database_Model___))
            {
                foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (t.IsClass)
                    {
                        ManyToManyRelationshipAttribute mmat;
                        if (t.TryGetAtr(out mmat))
                        {
                            if (!t.IsSubclassOf(typeof (RelTable)))
                                throw new DMError(Translations.DatabaseModel_Initialize_Class__0__cannot_take_participation_into_many_to_many_relationship_because_it_is_not_derived_from__1_, t.FullName, typeof(RelTable).Name);
                            if (!t.HasAtr<TableAttribute>())
                                throw new DMError(Translations.DatabaseModel_Initialize_Class__0__cannot_take_participation_into_many_to_many_relationship_because_it_is_not_marked_with__1_, t.FullName, typeof(TableAttribute).Name);
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
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            using (var scope = DBScope(Translations.DatabaseModel_Insert_Inserting_ + typeof(TDataObject).Name))
            {
                using (var cmd = scope.Connection.CreateCommand())
                {
                    var sc = new SqlSerializationContext(cmd);
                    DataObjectSqlSerializer<TDataObject>.AppendInsertSql(sc, obj);
                    sc.Finish();

                    using (var r = cmd.LexExecuteReader())
                    {
                        r.Read();
                        var id = Convert.ToInt32(r.GetDecimal(0));
                        DataObjectSqlSerializer<TDataObject>.KeyColumn.Storage.SetValue(obj, id);

                        Logger.WriteLine("ID = " + id);
                        Added<TDataObject>();

                        return id;
                    }
                }
            }
        }

        public void Insert<TDataObject>(IList<TDataObject> objs)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            if (objs.Count > 0)
            {
                using (var scope = DBScope(Translations.DatabaseModel_Insert_Inserting_multiple_ + typeof(TDataObject).Name + Translations.DatabaseModel_Insert_ + objs.Count))
                {
                    RunInTransaction(scope.Connection, (cn, transaction) =>
                    {                        
                        using (var cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            var sc = new SqlSerializationContext((SqlCommand) cmd);
                            foreach (var o in objs)
                            {
                                if (o.ID != 0)
                                {
                                    throw new DMError(Translations.DatabaseModel_Insert_DataObject_has_been_already_inserted_);
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
                                        throw new DMError(Translations.DatabaseModel_Insert_Invalid_Data_Reader);
                                    int id = Convert.ToInt32(r.GetDecimal(0));
                                    TDataObject obj = objs[i];
                                    DataObjectSqlSerializer<TDataObject>.KeyColumn.Storage.SetValue(obj, id);

                                    CacheIt(obj);
                                    Added<TDataObject>();

                                    Logger.WriteLine("ID = " + id);

                                    ++i;
                                    if (i < c)
                                        r.NextResult();
                                    else
                                        break;
                                }
                            }
                        }
                    });
                }
            }
        }

        public void Update<TDataObject>(TDataObject obj)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            using (var scope = DBScope(Translations.DatabaseModel_Update_Updating_ + typeof(TDataObject).Name))
            {
                using (var cmd = scope.Connection.CreateCommand())
                {
                    var sc = new SqlSerializationContext(cmd);
                    DataObjectSqlSerializer<TDataObject>.AppendUpdateSql(sc, obj);
                    sc.Finish();
                    cmd.LexExecuteNonQuery();
                }
                CacheIt(obj);
            }
        }

        public void Update<TDataObject>(IList<TDataObject> objs)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            //TODO: Update cache here. not only delete cached items
            if (objs.Count > 0)
            {
                using (var scope = DBScope(Translations.DatabaseModel_Update_Updating_multiple_ + typeof(TDataObject).Name + Translations.DatabaseModel_Insert_ + objs.Count))
                {
                    RunInTransaction(scope.Connection, (cn, transaction) =>
                    {
                        using (var cmd = cn.CreateCommand())
                        {
                            var sc = new SqlSerializationContext((SqlCommand)cmd);
                            foreach (var o in objs)
                            {
                                DataObjectSqlSerializer<TDataObject>.AppendUpdateSql(sc, o);
                                Logger.WriteLine("ID = " + o.ID);
                                sc.Next();
                                CacheIt(o);
                            }
                            sc.Finish();

                            cmd.Transaction = transaction;
                            cmd.LexExecuteNonQuery();
                        }
                    });
                }
            }
        }

        [Obsolete("Use it on your own risk - this method don't update Cache. Your changes can be override by other request or left invisible for the system")]
        public void Update<TDataObject>([NotNull]IDBUpdateOperation<TDataObject> op, [NotNull] IDbConnection cn, [CanBeNull] IDbTransaction transaction)
            where TDataObject : class, IDataObject, new()
        {           
            using (DBScope(Translations.DatabaseModel_Update_Executing_update_operation))
            {               
                using (var cmd = cn.CreateCommand())
                {
                    cmd.Transaction = transaction;
                    var sc = new SqlSerializationContext((SqlCommand) cmd);
                    op.Write(sc);
                    sc.Finish();
                    cmd.LexExecuteNonQuery();
                }
            }
        }

        public TDataObject Load<TDataObject>(int id)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            using (var scope = DBScope(Translations.DatabaseModel_Load_Loading_ + typeof(TDataObject).Name + ". ID = " + id))
            {
                var res = (TDataObject) Cache[FormatCacheKey<TDataObject>(id)];
                if (res != null)
                {
                    Logger.WriteLine(Translations.DatabaseModel_Load_Got_from_cache);
                    return res;
                }

                Logger.WriteLine(Translations.DatabaseModel_Load_Running_sql___);
                using (var cmd = scope.Connection.CreateCommand())
                {
                    cmd.CommandText = DataObjectInfo<TDataObject>.SelectSql + " WHERE [ID] = " + id + " AND sysState = 0";
                    using (var r = cmd.LexExecuteReader())
                    {
                        if (r.Read())
                        {
                            res = DataObjectInfo<TDataObject>.Read(r);
                            CacheIt(res);
                            return res;
                        }
                        throw new DMError(Translations.DatabaseModel_Load_Invalid_object_ID___0_, id);
                    }
                }
            }
        }

        public IList<TDataObject> Load<TDataObject>(IList<int> ids)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            // TODO: Try to get needed objects from Cache first

            if (ids.Count == 0)
            {
                return new TDataObject[0];
            }

            using (var scope = DBScope(Translations.DatabaseModel_Load_Loading_multiple_ + typeof(TDataObject).Name + Translations.DatabaseModel_Insert_ + ids.Count))
            {
                using (var cmd = scope.Connection.CreateCommand())
                {
                    List<TDataObject> result;
                    cmd.CommandText = DataObjectInfo<TDataObject>.SelectSql + " WHERE (ID IN (" + ids.ConcatComma() + ")) AND sysState = 0";
                    using (var r = cmd.LexExecuteReader())
                    {
                        result = DataObjectInfo<TDataObject>.FullRead(r, ids.Count);
                    }
                    if (result.Count != ids.Count)
                    {
                        throw new DMError(Translations.DatabaseModel_Load_Count_of_populated_objects___0___lower_than_requested_to_extract__1_, result.Count, ids.Count);
                    }
                    foreach (var o in result)
                    {
                        Logger.WriteLine("ID = " + o.ID);
                        CacheIt(o);
                    }
                    return result;
                }
            }
        }

        public IList<TDataObject> Load<TDataObject>(string columnName, IList<int> ids)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            // TODO: Try to get needed objects from Cache first

            if (ids.Count == 0)
            {
                return new TDataObject[0];
            }

            using (var scope = DBScope(Translations.DatabaseModel_Load_Loading_multiple_ + typeof(TDataObject).Name + Translations.DatabaseModel_Insert_ + ids.Count))
            {
                using (var cmd = scope.Connection.CreateCommand())
                {
                    List<TDataObject> result;
                    cmd.CommandText = DataObjectInfo<TDataObject>.SelectSql + " WHERE (" + columnName + " IN (" + ids.ConcatComma() + ")) AND sysState = 0";
                    using (var r = cmd.LexExecuteReader())
                    {
                        result = DataObjectInfo<TDataObject>.FullRead(r, ids.Count);
                    }
                    //if (result.Count != ids.Count)
                    //{
                    //    throw new DMError("Count of populated objects ({0}) lower than requested to extract {1}", result.Count, ids.Count);
                    //}
                    //foreach (var o in result)
                    //{
                    //    Logger.WriteLine("ID = " + o.ID);
                    //    CacheIt(o);
                    //}
                    return result;
                }
            }
        }

        public void Delete<TDataObject>(int id)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            using (var scope = DBScope(Translations.DatabaseModel_Delete_Removing_ + typeof(TDataObject).Name + ". ID = " + id))
            {
                Removed<TDataObject>(id);
                RunInTransaction(scope.Connection, (cn, transaction) =>
                {
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        var sc = new SqlSerializationContext((SqlCommand)cmd);
                        DataObjectSqlSerializer<TDataObject>.AppendSoftDeleteSql(sc, id);
                        sc.Finish();
                        cmd.LexExecuteNonQuery();

                        if (DataObjectInfo<TDataObject>.IsSecured)
                        {
                            var updOp = new UpdateOperation<TblPermissions>(
                                new CompareCondition<int>
                                (
                                    new PropertyCondition<int>(ObjectTypeHelper.GetObjectType(typeof(TDataObject)).GetSecurityAtr().Name + "Ref"),
                                    new ValueCondition<int>(id),
                                    COMPARE_KIND.EQUAL
                                ),
                                new PropertyAssignement<int>(DataObject.Schema.SysState, new ValueCondition<int>(1)));
                            Update(updOp, cn, transaction);
                        }
                    }
                });
            }
        }

        public void Delete<TDataObject>(IList<int> ids)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            if (ids.Count > 0)
            {
                using (var scope = DBScope(Translations.DatabaseModel_Delete_Removing_multiple_ + typeof(TDataObject).Name + Translations.DatabaseModel_Insert_ + ids.Count))
                {
                    foreach (var i in ids)
                    {
                        Logger.WriteLine("ID = " + i);
                        Cache.Remove(FormatCacheKey<TDataObject>(i));
                    }

                    RunInTransaction(scope.Connection, (cn, transaction) =>
                    {
                        using (var cmd = cn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            var sc = new SqlSerializationContext((SqlCommand) cmd);
                            DataObjectSqlSerializer<TDataObject>.
                                AppendSoftDeleteSql(sc, ids);
                            sc.Finish();
                            cmd.LexExecuteNonQuery();
                        }

                        if (DataObjectInfo<TDataObject>.IsSecured)
                        {
                            var idCond = new ValueCondition<int>(0);

                            var updOp = new UpdateOperation<TblPermissions>(
                                new CompareCondition<int>
                                    (
                                    new PropertyCondition<int>(ObjectTypeHelper.GetObjectType(typeof(TDataObject)).GetSecurityAtr().Name + "Ref"),
                                    idCond,
                                    COMPARE_KIND.EQUAL
                                    ),
                                new PropertyAssignement<int>(DataObject.Schema.SysState, new ValueCondition<int>(1)));
                            foreach (var id in ids)
                            {
                                idCond.Value = id;
                                Update(updOp, cn, transaction);
                            }
                        }
                    });
                }
            }
        }

        public void Delete<TDataObject>(IList<TDataObject> objs)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            Delete<TDataObject>(new List<int>(objs.Select(o => o.ID)));
        }

        public ReadOnlyCollection<TFxDataObject> Fx<TFxDataObject>()
            where TFxDataObject : class, IFxDataObject
        {
            return FxObjectsStorage<TFxDataObject>.Items;
        }

        public List<int> LookupIds<TDataObject>([NotNull] IIntKeyedDataObject owner, [CanBeNull] IDBPredicate condition)
            where TDataObject : IDataObject
        {
            using (var scope = DBScope(Translations.DatabaseModel_LookupIds_Looking_up_ids_of_ + owner.GetType().Name + Translations.DatabaseModel_LookupIds__for_ + typeof(TDataObject).Name))
            {
                using (var c = scope.Connection.CreateCommand())
                {
                    var sc = new SqlSerializationContext(c);
                    LookupHelper.AppendLookupSql(sc, owner, typeof(TDataObject), condition);
                    sc.Finish();
                    return c.FullReadInts();
                }
            }
        }

        public List<TDataObject> Query<TDataObject>([CanBeNull] IDBPredicate cond)
            where TDataObject : IDataObject, new()
        {
            using (var scope = DBScope(Translations.DatabaseModel_Query_Custom_query_for_ + typeof(TDataObject)))
            {
                using (var c = scope.Connection.CreateCommand())
                {
                    var sc = new SqlSerializationContext(c);
                    DataObjectInfo<TDataObject>.AppendQuerySql(sc, cond);
                    sc.Finish();
                    using (var r = c.LexExecuteReader())
                    {
                        var res = DataObjectInfo<TDataObject>.FullRead(r, 1);
                        if (typeof(TDataObject).GetInterface(typeof(IIntKeyedDataObject).Name) != null)
                        {
                            // TODO: Cache them
//                            foreach (var i in res)
//                            {
//                                CacheIt<TDataObject>(i as IIntKeyedDataObject);
//                            }
                        }
                        return res;
                    }
                }
            } 
        }

        public TDataObject QuerySingleOrDefault<TDataObject>([NotNull] IDBPredicate cond)
            where TDataObject : IDataObject, new()
        {
            using (var scope = DBScope(Translations.DatabaseModel_Query_Custom_query_for_ + typeof(TDataObject)))
            {
                using (var c = scope.Connection.CreateCommand())
                {
                    var sc = new SqlSerializationContext(c);
                    DataObjectInfo<TDataObject>.AppendQuerySql(sc, cond);
                    sc.Finish();
                    using (var r = c.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            if (r.Read())
                                throw new InvalidOperationException(Translations.DatabaseModel_QuerySingleOrDefault_Too_many_objects);
                            return DataObjectInfo<TDataObject>.Read(r);
                        }
                        return default(TDataObject);
                    }
                }
            } 
        }

        public List<int> LookupMany2ManyIds<TDataObject>([NotNull]IIntKeyedDataObject firstPart, [CanBeNull]IDBPredicate condition)
        {
            using (var scope = DBScope(Translations.DatabaseModel_LookupMany2ManyIds_Looking_up_many_2_many_ids_between_ + firstPart.GetType().Name + Translations.DatabaseModel_LookupMany2ManyIds__and_ + typeof(TDataObject).Name))
            {
                using (var c = scope.Connection.CreateCommand())
                {
                    var sc = new SqlSerializationContext(c);
                    LookupHelper.AppendMMLookupSql(sc, firstPart, typeof(TDataObject));
                    sc.Finish();
                    return c.FullReadInts();
                }
            }
        }

        public void Link(IIntKeyedDataObject do1, IIntKeyedDataObject do2)
        {
            using (var scope = DBScope("Linking " + do1.GetType().Name + ": " + do1.ID + Translations.DatabaseModel_LookupMany2ManyIds__and_ + do2.GetType().Name + ": " + do2.ID))
            {
                using (var c = scope.Connection.CreateCommand())
                {
                    var sc = new SqlSerializationContext(c);
                    LookupHelper.AppendMMLinkSql(sc, do1, do2);
                    sc.Finish();
                    c.LexExecuteNonQuery();
                }
            }
        }

        public void UnLink(IIntKeyedDataObject do1, IIntKeyedDataObject do2)
        {
            using (var scope = DBScope(Translations.DatabaseModel_UnLink_UnLinking_ + do1.GetType().Name + ": " + do1.ID + Translations.DatabaseModel_LookupMany2ManyIds__and_ + do2.GetType().Name + ": " + do2.ID))
            {
                using (var c = scope.Connection.CreateCommand())
                {
                    var sc = new SqlSerializationContext(c);
                    LookupHelper.AppendMMUnLinkSql(sc, do1, do2);
                    sc.Finish();
                    c.LexExecuteNonQuery();
                }
            }
        }

        public static readonly MethodInfo FIXED_METHOD = typeof(DatabaseModel).GetMethod("Fx");
        public static readonly MethodInfo LOAD_METHOD = typeof (DatabaseModel).GetMethod("Load", new[] {typeof (int)});
        public static readonly MethodInfo LOAD_LIST_METHOD = typeof(DatabaseModel).GetMethod("Load", new[] { typeof(IList<int>) });
        public static readonly MethodInfo QUERY_METHOD = typeof (DatabaseModel).GetMethod("Query");
        public static readonly MethodInfo DELETE_METHOD = typeof(DatabaseModel).GetMethod("Delete", new[] { typeof(int) });

        private static void RunInTransaction([NotNull] IDbConnection connection, [NotNull] Action<IDbConnection, IDbTransaction> operation)
        {
            var transaction = connection.BeginTransaction();
            try
            {
                operation(connection, transaction);
                transaction.Commit();
            }
            catch (Exception)
            {
                Logger.WriteLine(Translations.DatabaseModel_RunInTransaction_Rolling_back_transaction___);
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        private static string FormatCacheKey<TDataObject>(int id)
            where TDataObject : IIntKeyedDataObject
        {
            return typeof (TDataObject).Name + "_" + id;
        }

        private static string FormatCacheCountKey<TDataObject>()
        {
            return typeof (TDataObject).Name + "_Count"; 
        }

        private void Removed<TDataObject>(int id)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            Cache.Remove(FormatCacheKey<TDataObject>(id));
            Cache.Remove(FormatCacheCountKey<TDataObject>());
        }

        private void Added<TDataObject>()
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            Cache.Remove(FormatCacheCountKey<TDataObject>());
        }

        private void CacheIt<TDataObject>(TDataObject obj)
            where TDataObject : IIntKeyedDataObject, new()
        {
            Cache[FormatCacheKey<TDataObject>(obj.ID)] = obj;
        }

        [NotNull]
        private static IDBScope DBScope([NotNull] string operation)
        {
            return new DBModelScope(operation);
        }

        #region FxObjectsStorage

        private static class FxObjectsStorage<T>
            where T: class, IFxDataObject
        {
            static FxObjectsStorage()
            {
                Items = new ReadOnlyCollection<T>(new List<T>(ServerModel.DB.GetTable<T>()));
            }

            public static readonly ReadOnlyCollection<T> Items;
        }
        #endregion

        #region DBModelScope

        private class DBModelScope : IDBScope
        {
            public readonly string Name;

            public DBModelScope([NotNull] string name)
            {
                //Monitor.Enter(typeof(DBModelScope));
//                if (Current != null)
//                {
//                    throw new InvalidOperationException("Cannot enter into DB scope. There is scope already assigned to the operation");
//                }
                Current = this;
                Name = name;
                Enter();
            }

            public void Enter()
            {
                Logger.WriteLine("[DB MODEL]--> " + Name);
                Logger.Indent();
            }

            public void Leave()
            {
                Logger.UnIndent();
                Logger.WriteLine("[DB MODEL]<-- " + Name);
            }

            public void Dispose()
            {
                if (_Connection != null)
                {
                    _Connection.Dispose();
                }
                Leave();
                Current = null;
                //Monitor.Exit(typeof(DBModelScope));
            }

            [NotNull]
            public SqlConnection Connection
            {
                get
                {
                    if (_Connection == null)
                    {
                        _Connection = ServerModel.AcruireOpenedConnection();
                    }
                    return _Connection;
                }
            }

            [CanBeNull]
            private SqlConnection _Connection;

            [CanBeNull] [ThreadStatic] 
            private static DBModelScope Current;
        }

        #endregion

    }
}