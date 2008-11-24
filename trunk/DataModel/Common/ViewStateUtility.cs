using System;
using System.Collections.Generic;
using System.Reflection;
using IUDICO.DataModel.Controllers;
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
    public sealed class PersistantField : Attribute
    {
    }

    public struct PeristantValueInfo
    {
        public PeristantValueInfo(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }
            Storage = field;

            var fieldType = field.FieldType;

            if (fieldType.IsValueType || fieldType.IsArray)
            {
                Persistor = Persist_SimpleValue;
                Restorer = Restore_SimpleValue;
                Creator = null;
            }
            else if (fieldType.GetInterface(typeof(IViewStateSerializable).Name) != null)
            {
                Persistor = Persist_IViewStateSerializable;
                Restorer = Restore_IViewStateSerializable;
                if ((Creator = fieldType.GetConstructor(Type.EmptyTypes)) == null)
                {
                    throw new DMError("Type {0} cannot be persisted because it's don't have parameterless constructor", fieldType.FullName);
                }
            }
            else
            {
                throw new DMError("Type {0} is not supposed to be persisted. Invalid type. Only {1}, arrays and {2} is supported for now.", typeof(ValueType).Name, typeof(IViewStateSerializable));
            }
        }

        public object Persist([NotNull] object owner)
        {
            return Persistor(Storage.GetValue(owner));
        }

        public void Restore([NotNull] object owner, object obj)
        {
            Storage.SetValue(owner, Restorer(Creator, obj));
        }

        private static object Persist_SimpleValue(object v)
        {
            return v;
        }

        private static object Restore_SimpleValue(ConstructorInfo c, object v)
        {
            return v;
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

        private readonly FieldInfo Storage;
        private readonly ConstructorInfo Creator;
        private readonly Func<object, object> Persistor;
        private readonly Func<ConstructorInfo, object, object> Restorer;
    }

    public struct PersistantStateMetaData
    {
        private PersistantStateMetaData(IList<PeristantValueInfo> values)
        {
            Values = values;
        }

        public static Func<Type, PersistantStateMetaData> Get = new Memorizer<Type, PersistantStateMetaData>(GetFieldsDataInternal);

        private static PersistantStateMetaData GetFieldsDataInternal(Type t)
        {
            var values = new List<PeristantValueInfo>();
            var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var inf in fields)
            {
                if (inf.HasAtr<PersistantField>())
                {
                    if (inf.IsStatic)
                    {
                        throw new DMError("{0} can be applied to instance field only but applied to static ({1}.{2})", typeof(PersistantField).Name, t.Name, inf.Name);
                    }
                    values.Add(new PeristantValueInfo(inf));
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

        private readonly IList<PeristantValueInfo> Values;
    }
}
