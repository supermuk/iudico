#define MULTY_MACHINE

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web.Caching;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using System.Threading;

namespace IUDICO.DataModel.DB
{
    public partial class DatabaseModel : IDBOperator
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

        private void Initialize()
        {
            using (DBScope("Initializing Database Model..."))
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

        public IDBOperatorStatement QueryStatement 
        { 
            get
            {
                return _QueryStatement;
            } 
            set
            {
                _QueryStatement = value;
            }
        }

        public int Insert<TDataObject>(TDataObject obj)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            using (DBScope("Inserting " + typeof(TDataObject).Name))
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
                using (DBScope("Inserting multiple " + typeof(TDataObject).Name + ". Count = " + objs.Count))
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
                        transaction.Commit();
                    }
                    catch
                    {
                        Logger.WriteLine("Rolling back transaction...");
                        transaction.Rollback();
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
            }
        }

        public void Update<TDataObject>(TDataObject obj)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            using (DBScope("Updating " + typeof(TDataObject).Name))
            {
                using (var cmd = GetConnectionSafe().CreateCommand())
                {
                    var sc = new SqlSerializationContext((SqlCommand) cmd);
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
                using (DBScope("Updating multiple " + typeof(TDataObject).Name + ". Count = " + objs.Count))
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
                                Logger.WriteLine("ID = " + o.ID);
                                sc.Next();
                                CacheIt(o);
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
        }

        public TDataObject Load<TDataObject>(int id)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            using (DBScope("Loading " + typeof(TDataObject).Name + ". ID = " + id))
            {
                var res = (TDataObject) Cache[FormatCacheKey<TDataObject>(id)];
                if (res != null)
                {
                    Logger.WriteLine("Got from cache");
                    return res;
                }

                Logger.WriteLine("Running sql...");
                using (var cmd = GetConnectionSafe().CreateCommand())
                {
                    cmd.CommandText = DataObjectSqlSerializer<TDataObject>.SelectSql + " WHERE [ID] = " + id;
                    using (var r = cmd.LexExecuteReader())
                    {
                        if (r.Read())
                        {
                            res = DataObjectSqlSerializer<TDataObject>.Read(r);
                            CacheIt(res);
                            return res;
                        }
                        throw new DMError("Invalid object ID: {0}", id);
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

            using (DBScope("Loading multiple " + typeof(TDataObject).Name + ". Count = " + ids.Count))
            {
                using (var cmd = GetConnectionSafe().CreateCommand())
                {
                    List<TDataObject> result;
                    cmd.CommandText = DataObjectSqlSerializer<TDataObject>.SelectSql + " WHERE ID IN (" + ids.ConcatComma() + ")";
                    using (var r = cmd.LexExecuteReader())
                    {
                        result = DataObjectSqlSerializer<TDataObject>.FullRead(r, ids.Count);
                    }
                    if (result.Count != ids.Count)
                    {
                        throw new DMError("Count of populated objects ({0}) lower than requested to extract {1}", result.Count, ids.Count);
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

        public IList<TDataObject> LoadRange<TDataObject>(int from, int to)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            if (from > to)
                throw new ArgumentException("'from' must be not greater than 'to'");

            using (DBScope("Loading range of " + typeof(TDataObject).Name + " between " + from + " and " + to))
            {
                using (var cmd = GetConnectionSafe().CreateCommand())
                {
                    var cn = new SqlSerializationContext((SqlCommand) cmd);
                    DataObjectSqlSerializer<TDataObject>.AppendSelectRangeSql(cn, from, to, QueryStatement);
                    cn.Finish();
                    using (var r = cmd.LexExecuteReader())
                    {
                        var res = DataObjectSqlSerializer<TDataObject>.FullRead(r, to - from + 1);
                        foreach (var o in res)
                        {
                            CacheIt(o);
                            Logger.WriteLine("ID = " + o.ID);
                        }

                        return res;
                    }
                }
            }
        }

        public void Delete<TDataObject>(int id)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            using (DBScope("Removing " + typeof(TDataObject).Name + ". ID = " + id))
            {
                Removed<TDataObject>(id);
                using (var cmd = GetConnectionSafe().CreateCommand())
                {
                    var sc = new SqlSerializationContext((SqlCommand)cmd);
                    DataObjectSqlSerializer<TDataObject>.AppendDeleteSql(sc, id);
                    sc.Finish();
                    cmd.LexExecuteNonQuery();
                }
            }
        }

        public void Delete<TDataObject>(IList<int> ids)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            if (ids.Count > 0)
            {
                using (DBScope("Removing multiple " + typeof(TDataObject).Name + ". Count = " + ids.Count))
                {
                    foreach (var i in ids)
                    {
                        Logger.WriteLine("ID = " + i);
                        Cache.Remove(FormatCacheKey<TDataObject>(i));
                    }

                    using (var cmd = GetConnectionSafe().CreateCommand())
                    {
                        var sc = new SqlSerializationContext((SqlCommand) cmd);
                        DataObjectSqlSerializer<TDataObject>.AppendDeleteSql(sc, ids);
                        sc.Finish();
                        cmd.LexExecuteNonQuery();
                    }
                }
            }
        }

        public void Delete<TDataObject>(IList<TDataObject> objs)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            Delete<TDataObject>(new List<int>(objs.Select(o => o.ID)));
        }

        public ReadOnlyCollection<TFxDataObject> Fx<TFxDataObject>()
            where TFxDataObject : FxDataObject
        {
            return FxObjectsStorage<TFxDataObject>.Items;
        }

        public List<TDataObject> FullCached<TDataObject>()
            where TDataObject : class, IDataObject, new()
        {
            using (DBScope("Getting full cached table " + typeof(TDataObject).Name))
            {
                string tableName = DataObjectInfo<TDataObject>.TableName;
                var r = (List<TDataObject>)_Cache[tableName];
                if (r == null)
                {
                    Logger.WriteLine("Selecting...");
                    r = new List<TDataObject>(GetTable<TDataObject>());
                    _Cache.Add(tableName, r, DataObjectInfo<TDataObject>.CacheDependency, DateTime.MaxValue, new TimeSpan(1, 1, 1), CacheItemPriority.AboveNormal, null);
                }
                else
                {
                    Logger.WriteLine("Got from Cache");
                }
                return r;
            }
        }

        public List<int> LookupIds<TDataObject>(IIntKeyedDataObject owner)
            where TDataObject : IDataObject
        {
            using (DBScope("Looking up ids of " + owner.GetType().Name + " for " + typeof(TDataObject).Name))
            {
                using (var c = GetConnectionSafe().CreateCommand())
                {
                    var sc = new SqlSerializationContext((SqlCommand)c);
                    LookupHelper.AppendLookupSql(sc, owner, typeof(TDataObject));
                    sc.Finish();
                    return c.FullReadInts();
                }
            }
        }

        public List<int> LookupMany2ManyIds<TDataObject>(IIntKeyedDataObject firstPart)
        {
            using (DBScope("Looking up many-2-many ids between " + firstPart.GetType().Name + " and " + typeof(TDataObject).Name))
            {
                using (var c = GetConnectionSafe().CreateCommand())
                {
                    var sc = new SqlSerializationContext((SqlCommand)c);
                    LookupHelper.AppendMMLookupSql(sc, firstPart, typeof(TDataObject));
                    sc.Finish();
                    return c.FullReadInts();
                }
            }
        }

        public void Link(IIntKeyedDataObject do1, IIntKeyedDataObject do2)
        {
            using (DBScope("Linking " + do1.GetType().Name + ": " + do1.ID + " and " + do2.GetType().Name + ": " + do2.ID))
            {
                using (var c = GetConnectionSafe().CreateCommand())
                {
                    var sc = new SqlSerializationContext((SqlCommand)c);
                    LookupHelper.AppendMMLinkSql(sc, do1, do2);
                    sc.Finish();
                    c.LexExecuteNonQuery();
                }
            }
        }

        public void UnLink(IIntKeyedDataObject do1, IIntKeyedDataObject do2)
        {
            using (DBScope("UnLinking " + do1.GetType().Name + ": " + do1.ID + " and " + do2.GetType().Name + ": " + do2.ID))
            {
                using (var c = GetConnectionSafe().CreateCommand())
                {
                    var sc = new SqlSerializationContext((SqlCommand)c);
                    LookupHelper.AppendMMUnLinkSql(sc, do1, do2);
                    sc.Finish();
                    c.LexExecuteNonQuery();
                }
            }
        }

        public int Count<TDataObject>()
            where TDataObject : IDataObject
        {
            using (DBScope("Select count of " + typeof(TDataObject).Name))
            {
                var o = Cache[FormatCacheCountKey<TDataObject>()];
                if (o != null)
                {
                    var res = (int) o;
                    Logger.WriteLine("Cached: " + res);
                    return res;
                }

                using (var c = GetConnectionSafe().CreateCommand())
                {
                    c.CommandText = string.Format("SELECT COUNT(*) FROM [{0}]", DataObjectInfo<TDataObject>.TableName);
                    var res = c.LexExecuteScalar<int>();
                    Logger.WriteLine("Count: " + res);
                    Cache[FormatCacheCountKey<TDataObject>()] = res;
                    return res;
                }
            }
        }

        public static readonly MethodInfo FIXED_METHOD = typeof(DatabaseModel).GetMethod("Fx");
        public static readonly MethodInfo LOAD_LIST_METHOD = typeof(DatabaseModel).GetMethod("Load", new[] { typeof(IList<int>) });

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
            where TDataObject : class, IIntKeyedDataObject, new()
        {
#if MULTY_MACHINE   
            Cache.Add(FormatCacheKey<TDataObject>(obj.ID), obj, DataObjectInfo<TDataObject>.CacheDependency, DateTime.MaxValue, new TimeSpan(1, 1, 1), CacheItemPriority.Normal, null);            
#else
            Cache[FormatCacheKey<TDataObject>(obj.ID)] = obj;
#endif
        }

        private static IDisposable DBScope(string operation)
        {
            return new DBModelScope(operation);
        }

        [ThreadStatic]
        private static IDBOperatorStatement _QueryStatement;

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

        #region DBModelScope

        private class DBModelScope : IDisposable
        {
            public readonly string Name;

            public DBModelScope([NotNull] string name)
            {
                Monitor.Enter(typeof(DBModelScope));
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
                Leave();
                Monitor.Exit(typeof(DBModelScope));
            }
        }

        #endregion

    }

    public class WhereStatement : IDBOperatorStatement
    {
        public WhereStatement(IDBOperatorCondition cond)
        {
            _Cond = cond;
        }

        public void Append(SqlSerializationContext context)
        {
            context.Write(" WHERE ");
            _Cond.Append(context);
        }

        private readonly IDBOperatorCondition _Cond;
    }

    public class EqualCondition : IDBOperatorCondition
    {
        public EqualCondition(object param)
        {
            _Param = param;   
        }

        public void Append(SqlSerializationContext context)
        {
            bool notFirst = false;
            context.Write("(");
            foreach (var p in _Param.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (notFirst)
                {
                    context.Write(" AND ");
                }
                else
                {
                    notFirst = true;
                }
                context.Write("([{0}] = {1})", p.Name, context.AddParameter(p.GetValue(_Param, null)));
            }
            context.Write(")");
        }

        private readonly object _Param;
    }

    public class OrCondtion : IDBOperatorCondition
    {
        public OrCondtion(IDBOperatorCondition a, IDBOperatorCondition b)
        {
            _A = a;
            _B = b;
        }

        public void Append(SqlSerializationContext context)
        {
            context.Write("(");    
            _A.Append(context);
            context.Write(" OR ");
            _B.Append(context);
            context.Write(")");
        }

        private readonly IDBOperatorCondition _A, _B;
    }

    public class AndCondtion : IDBOperatorCondition
    {
        public AndCondtion(IDBOperatorCondition a, IDBOperatorCondition b)
        {
            _A = a;
            _B = b;
        }

        public void Append(SqlSerializationContext context)
        {
            context.Write("(");    
            _A.Append(context);
            context.Write(" AND ");
            _B.Append(context);
            context.Write(")");
        }

        private readonly IDBOperatorCondition _A, _B;
    }
}