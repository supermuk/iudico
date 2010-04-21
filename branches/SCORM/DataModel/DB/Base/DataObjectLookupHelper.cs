using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using IUDICO.DataModel.Common;
using LEX.CONTROLS;

namespace IUDICO.DataModel.DB.Base
{
    internal static class LookupHelper
    {
        public static void RegisterLookup(Type ownerType, Type detailType, AssociationAttribute aa)
        {
            Put(__Infos, ownerType, detailType,
                new LookupInfo(aa.OtherKey, SqlSerializationContext.ExtractTableName(detailType.GetAtr<TableAttribute>().Name)));
        }

        public static void RegisterMMLookup(ManyToManyRelationshipAttribute mmr, Type relType)
        {
            CheckMMSupport(mmr.First);
            CheckMMSupport(mmr.Second);

            string firstKey = null,
                   secondKey = null;

            foreach (var p in relType.GetProperties())
            {
                AssociationAttribute aa;
                if (p.TryGetAtr(out aa))
                {
                    if (p.PropertyType == mmr.First && p.PropertyType == mmr.Second)
                    {
                        if (firstKey == null)
                        {
                            firstKey = aa.ThisKey;
                        }
                        else if (secondKey == null)
                        {
                            secondKey = aa.ThisKey;
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }
                    else if (p.PropertyType == mmr.First)
                    {
                        if (firstKey != null)
                        {
                            throw new InvalidOperationException();
                        }
                        firstKey = aa.ThisKey;
                    }
                    else if (p.PropertyType == mmr.Second)
                    {
                        if (secondKey != null)
                        {
                            throw new InvalidOperationException();
                        }
                        secondKey = aa.ThisKey;
                    }
                }
            }
            if (firstKey.IsNull() || secondKey.IsNull())
            {
                throw new DMError(Translations.LookupHelper_RegisterMMLookup_Invalid_many_to_many_relationship_definition_for__0___1_, mmr.First.Name, mmr.Second.Name);
            }
            string tableName = SqlSerializationContext.ExtractTableName(relType.GetAtr<TableAttribute>().Name);
            Put(__MMInfos, mmr.First, mmr.Second, new ManyToManyLookupInfo(firstKey, tableName, secondKey, relType));
            Put(__MMInfos, mmr.Second, mmr.First, new ManyToManyLookupInfo(secondKey, tableName, firstKey, relType));
        }

        public static void AppendLookupSql([NotNull]SqlSerializationContext context, [NotNull]IIntKeyedDataObject owner, [NotNull]Type detailType, [CanBeNull]IDBPredicate condition)
        {
            var r = Get(__Infos, owner.GetType(), detailType);
            if (r.IsEmpty)
                throw new DMError(Translations.LookupHelper_AppendLookupSql_Couldnt_found_relation_between__0__and__1_, owner.GetType(), detailType);
            if (owner.ID <= 0)
                throw new DMError(Translations.LookupHelper_AppendLookupSql_, owner.GetType().Name, owner.ID);

            context.Write("SELECT ID FROM [{0}] where ([{1}] = {2}) and sysState = 0", r.TableName, r.RefColumnName, context.AddParameter(owner.ID));
            if (condition != null)
            {
                context.Write(" AND (");
                condition.Write(context);
                context.Write(")");
            }
        }

        public static void AppendMMLookupSql([NotNull] SqlSerializationContext context, IIntKeyedDataObject firstPart, Type otherType)
        {
            var r = Get(__MMInfos, firstPart.GetType(), otherType);
            if (r.IsEmpty)
                throw new DMError(Translations.LookupHelper_AppendMMLookupSql_Couldnt_fond_many_to_many_relation_between__1__and__1_, firstPart.GetType().Name, otherType.Name);
            if (firstPart.ID <= 0)
                throw new DMError(Translations.LookupHelper_AppendLookupSql_, firstPart.GetType(), firstPart.ID);

            context.Write("SELECT [{0}] FROM [{1}] WHERE ([{2}] = {3}) and sysState = 0",
                r.IDColumnName, r.TableName, r.RefColumnName, context.AddParameter(firstPart.ID));
        }

        public static void AppendMMLinkSql([NotNull] SqlSerializationContext context, IIntKeyedDataObject o1, IIntKeyedDataObject o2)
        {
            if (o1.ID <= 0)
                throw new DMError(Translations.LookupHelper_AppendLookupSql_, o1.GetType(), o1.ID);
            if (o2.ID <= 0)
                throw new DMError(Translations.LookupHelper_AppendLookupSql_, o2.GetType(), o2.ID);

            var r = Get(__MMInfos, o1.GetType(), o2.GetType());
            if (r.IsEmpty)
                throw new DMError(Translations.LookupHelper_AppendMMLinkSql_Couldnt_fond_many_to_many_relation_between__1__and__2_, o1.GetType().Name, o2.GetType().Name);

            context.Write("INSERT INTO [{0}] ([{1}], [{2}]) VALUES ({3}, {4})", 
                r.TableName, r.RefColumnName, r.IDColumnName, context.AddParameter(o1.ID), context.AddParameter(o2.ID));
        }

        public static void AppendMMUnLinkSql([NotNull] SqlSerializationContext context, IIntKeyedDataObject o1, IIntKeyedDataObject o2)
        {
            var r = Get(__MMInfos, o1.GetType(), o2.GetType());
            if (r.IsEmpty)
                throw new DMError(Translations.LookupHelper_AppendMMLookupSql_Couldnt_fond_many_to_many_relation_between__1__and__1_, o1.GetType().Name, o2.GetType().Name);

            if (o1.ID <= 0)
                throw new DMError(Translations.LookupHelper_AppendLookupSql_, o1.GetType(), o1.ID);
            if (o2.ID <= 0)
                throw new DMError(Translations.LookupHelper_AppendLookupSql_, o2.GetType(), o2.ID);

            context.Write("DELETE [{0}] WHERE [{1}] = {3} AND [{2}] = {4}",
                r.TableName, r.RefColumnName, r.IDColumnName, context.AddParameter(o1.ID), context.AddParameter(o2.ID));
        }

        public static void AppendMMUnlinkAllSqlSafe<TDataObject>([NotNull] SqlSerializationContext context, int id)
            where TDataObject : IIntKeyedDataObject
        {
            if (id <= 0)
                throw new DMError("Invalid ID: " + id);

            Dictionary<Type, ManyToManyLookupInfo> lookups;
            if (__MMInfos.TryGetValue(typeof(TDataObject), out lookups))
            {
                foreach (var vs in lookups.Values)
                {
                    context.Write("UPDATE [{0}] SET sysState = 1 WHERE {1} = {2}", vs.TableName, vs.RefColumnName, id);
                    context.Next();
                }                
            }
        }

        private struct LookupInfo
        {
            public LookupInfo(string refColumnName, string tableName)
            {
                if (refColumnName.IsNull() || tableName.IsNull())
                    throw new ArgumentException();

                RefColumnName = refColumnName;
                TableName = tableName;
            }

            public bool IsEmpty { get { return RefColumnName.IsNull(); } }

            public readonly string RefColumnName;
            public readonly string TableName;
        }

        private struct ManyToManyLookupInfo
        {
            public ManyToManyLookupInfo(string refColumnName, string tableName, string idColumnName, Type relType)
            {
                if (refColumnName.IsNull() || tableName.IsNull() || idColumnName.IsNull() || relType == null)
                    throw new ArgumentException();

                RefColumnName = refColumnName;
                TableName = tableName;
                IDColumnName = idColumnName;
                RelType = relType;
            }

            public bool IsEmpty { get { return RefColumnName.IsNull(); } }

            public readonly string RefColumnName;
            public readonly string TableName;
            public readonly string IDColumnName;
            public readonly Type RelType;
        }

        private static void CheckMMSupport(Type t)
        {
            if (t.GetInterface(typeof(IIntKeyedDataObject).Name) == null)
            {
                throw new DMError(Translations.LookupHelper_CheckMMSupport_Cannot_register_many_to_many_relationship__Class__0__doesn_t_support__1_, t.FullName, typeof(IIntKeyedDataObject).Name);
            }
        }

        private static T Get<T>(IDictionary<Type, Dictionary<Type, T>> dic, Type key1, Type key2)
            where T: struct
        {
            Dictionary<Type, T> r1;
            if (dic.TryGetValue(key1, out r1))
            {
                T res;
                if (r1.TryGetValue(key2, out res))
                {
                    return res;
                }
            }
            return default(T);
        }

        private static void Put<T>(IDictionary<Type, Dictionary<Type, T>> dic, Type key1, Type key2, T value)
            where T: struct
        {
            Dictionary<Type, T> d;
            if (!dic.TryGetValue(key1, out d))
            {
                d = new Dictionary<Type, T>(1);
                dic.Add(key1, d);
            }
            if (!d.ContainsKey(key2))
            {
                d.Add(key2, value);   
            }
            else
            {
                Logger.WriteLine(Translations.LookupHelper_Put__WARNING__Ignored__0_____1__relationship_because_the_same_is_already_exists, key1.Name, key2.Name);
            }
        }

        private static readonly Dictionary<Type, Dictionary<Type, LookupInfo>> __Infos = new Dictionary<Type, Dictionary<Type, LookupInfo>>();
        private static readonly Dictionary<Type, Dictionary<Type, ManyToManyLookupInfo>> __MMInfos = new Dictionary<Type, Dictionary<Type, ManyToManyLookupInfo>>();
    }
}