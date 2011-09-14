using System;
using System.Collections.Generic;
using System.Reflection;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    public interface IViewStateSerializable
    {
        object SaveViewStateData();
        void LoadViewStateData(object data);
    }

    [AttributeUsage(AttributeTargets.Field)]
    [BaseTypeRequired(typeof(ControllerBase), typeof(ControlledPage))]
    public sealed class PersistantFieldAttribute : Attribute
    {
    }

    public struct PersistantValueInfo
    {
#region Internal Types

        private interface IPersistManager
        {
            object Persist(object v);
            object Restore(ConstructorInfo c, object v);
        }

        private static readonly Type[] TYPED_COLLECTION_VALUE_INFO_CONSTRUCTOR_PARAMETERS
            = new[] {
                typeof(ConstructorInfo),
                typeof(ConstructorInfo),
                typeof(Func<object, object>),
                typeof(Func<ConstructorInfo, object, object>) 
            } ;

        private struct TypedCollectionValueInfo<T> : IPersistManager
        {
            private readonly ConstructorInfo creator;
            private readonly ConstructorInfo vCreator;
            private readonly Func<object, object> vPersistor;
            private readonly Func<ConstructorInfo, object, object> vRestorer;

            // Called with reflection
            public TypedCollectionValueInfo(ConstructorInfo creator, ConstructorInfo vCreator, Func<object, object> vPersistor, Func<ConstructorInfo, object, object> vRestorer)
            {
                this.creator = creator;
                this.vCreator = vCreator;
                this.vPersistor = vPersistor;
                this.vRestorer = vRestorer;
            }

            public object Persist(object v)
            {
                if (v == null)
                {
                    return null;
                }

                var iv = (ICollection<T>)v;
                var res = new object[iv.Count];

                int index = 0;
                foreach (var i in iv)
                {
                    res[index] = vPersistor(i);   
                    ++index;
                }

                return res;
            }

            public object Restore(ConstructorInfo c, object v)
            {
                if (v == null)
                {
                    return null;
                }

                var iv = (object[]) v;
                var res = (ICollection<T>)creator.Invoke(Type.EmptyTypes);
                foreach (var i in iv)
                {
                    res.Add((T)vRestorer(vCreator, i));
                }
                return res;
            }
        }
#endregion

        public PersistantValueInfo(IMemberAL storage)
        {
            if (!BuildProcs(storage.MemberType, out Creator, out Persistor, out Restorer, ref storage))
            {
                throw new DMError("Type {0} is not supposed to be marked with {2}. Invalid type. Only ValueTypes, strings and classes which implement {1} or {5}. Any combination with {3}<T> and {4}<T> by this types also supported.", storage.MemberType.FullName, typeof(IViewStateSerializable).Name, typeof(PersistantFieldAttribute).Name, typeof(IVariable<>).Name, typeof(ICollection<>).Name, typeof(IIntKeyedDataObject).Name);
            }
            Storage = storage;
        }

        public object Persist([NotNull] object owner)
        {
            return Persistor(Storage[owner]);
        }

        public void Restore([NotNull] object owner, object obj)
        {
            Storage[owner] = Restorer(Creator, obj);
        }

        private static bool BuildProcs(Type type, out ConstructorInfo creator, out Func<object, object> persistor, out Func<ConstructorInfo, object, object> restorer, ref IMemberAL storage)
        {
            if (type.IsValueType || type == typeof(string))
            {
                persistor = Persist_SimpleValue;
                restorer = Restore_SimpleValue;
                creator = null;
                return true;
            }

            bool isViewStateSerializable = false,
                 isTypedVariable = false,
                 isTypedCollection = false,
                 isIntKeyedDataObject = false;

            foreach (var intf in type.GetInterfaces().Append(type))
            {
                if (typeof(IViewStateSerializable).IsAssignableFrom(type))
                    isViewStateSerializable = true;

                if (typeof(IIntKeyedDataObject).IsAssignableFrom(type))
                    isIntKeyedDataObject = true;

                if (intf.IsGenericType)
                {
                    var gIntf = intf.GetGenericTypeDefinition();
                    if (gIntf == typeof(IVariable<>))
                        isTypedVariable = true;

                    if (gIntf == typeof(ICollection<>))
                        isTypedCollection = true;
                }
            }

            if (isIntKeyedDataObject)
            {
                persistor = Persist_IntKeyedDataObject;
                restorer = Restore_IntKeyedDataObject;
                creator = SafeGetConstructor(type);
                return true;
            }

            if (isViewStateSerializable)
            {
                persistor = Persist_IViewStateSerializable;
                restorer = Restore_IViewStateSerializable;
                creator = SafeGetConstructor(type);
                return true;
            }

            if (isTypedVariable)
            {
                storage = storage.GetMember("Value");
                if (BuildProcs(storage.MemberType, out creator, out persistor, out restorer, ref storage))
                {
                    return true;
                }
            }

            if (isTypedCollection)
            {
                var subType = type.GetGenericArguments()[0];
                ConstructorInfo c;
                Func<object, object> p;
                Func<ConstructorInfo, object, object> r;
                if (BuildProcs(subType, out c, out p, out r, ref storage))
                {
                    creator = SafeGetConstructor(type);
                    var man = (IPersistManager)typeof(TypedCollectionValueInfo<>).MakeGenericType(new[] { subType }).GetConstructor(TYPED_COLLECTION_VALUE_INFO_CONSTRUCTOR_PARAMETERS)
                        .Invoke(new object[] { creator, c, p, r });
                    persistor = man.Persist;
                    restorer = man.Restore;
                    return true;
                }
            }

            persistor = null;
            restorer = null;
            creator = null;
            return false;
        }

        private static ConstructorInfo SafeGetConstructor(Type t)
        {
            var res = t.GetConstructor(Type.EmptyTypes);
            if (res == null)
            {
                throw new DMError("Type {0} cannot be persisted because it's don't have parameterless constructor", t.FullName);
            }
            return res;
        }

        private static object Persist_SimpleValue(object v)
        {
            return v;
        }

        private static object Restore_SimpleValue(ConstructorInfo c, object v)
        {
            return v;
        }

        private static object Persist_IntKeyedDataObject(object v)
        {
            return ((IIntKeyedDataObject) v).ID;
        }

        private static object Restore_IntKeyedDataObject(ConstructorInfo c, object v)
        {
            return (IIntKeyedDataObject)DatabaseModel.LOAD_METHOD.MakeGenericMethod(new[] { c.DeclaringType }).Invoke(ServerModel.DB, new object[] { (int)v });
        }

        private static object Persist_IViewStateSerializable(object v)
        {
            return v != null ? ((IViewStateSerializable) v).SaveViewStateData() : null;
        }

        private static object Restore_IViewStateSerializable(ConstructorInfo c, object v)
        {
            if (v != null)
            {
                var res = (IViewStateSerializable) c.Invoke(null);
                res.LoadViewStateData(v);
                return res;
            }
            return null;
        }

        private readonly IMemberAL Storage;
        private readonly ConstructorInfo Creator;
        private readonly Func<object, object> Persistor;
        private readonly Func<ConstructorInfo, object, object> Restorer;
    }

    public struct PersistantStateMetaData
    {
        private PersistantStateMetaData(IList<PersistantValueInfo> values)
        {
            Values = values;
        }

        public static Func<Type, PersistantStateMetaData> Get = new Memorizer<Type, PersistantStateMetaData>(GetFieldsDataInternal);

        private static PersistantStateMetaData GetFieldsDataInternal(Type t)
        {
            var values = new List<PersistantValueInfo>();
            var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var inf in fields)
            {
                if (inf.HasAtr<PersistantFieldAttribute>())
                {
                    if (inf.IsStatic)
                    {
                        throw new DMError("{0} can be applied to instance field only but applied to static ({1}.{2})", typeof(PersistantFieldAttribute).Name, t.Name, inf.Name);
                    }
                    values.Add(new PersistantValueInfo(inf.ToAbstaction()));
                }
            }

            return new PersistantStateMetaData(values.ToArray());
        }

        public bool IsEmpty { get { return Values.Count == 0; } }

        public object SaveStateFor(object target)
        {
            if (IsEmpty)
            {
                return null;
            }

            var result = new object[Values.Count];
            for (var i = Values.Count - 1; i >= 0; --i)
            {
                result[i] = Values[i].Persist(target);
            }
            return result;
        }

        public void LoadStateFor(object target, object state)
        {
            if (IsEmpty)
            {
                return;
            }

            var st = (object[])state;
            if (st.Length != Values.Count)
            {
                throw new DMError("Invalid count of target values expected {0} but {1} found", Values.Count, st.Length);
            }
            for (var i = Values.Count - 1; i >= 0; --i)
            {
                Values[i].Restore(target, st[i]);
            }
        }

        private readonly IList<PersistantValueInfo> Values;
    }
}
