using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using IUDICO.DataModel.Common;
using LEX.CONTROLS;

namespace IUDICO.DataModel.DB.Base
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class DBEnumAttribute : Attribute
    {
        public readonly string TableName;

        public DBEnumAttribute(string tableName)
        {
            TableName = tableName;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    [BaseTypeRequired(typeof(IFxDataObject))]
    public class TableRecordAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    [BaseTypeRequired(typeof(RelTable))]
    public class ManyToManyRelationshipAttribute : Attribute
    {
        public ManyToManyRelationshipAttribute(Type first, Type second)
        {
            CheckSupport(first);
            CheckSupport(second);

            First = first;
            Second = second;
        }

        public readonly Type First;
        public readonly Type Second;

        private static void CheckSupport(Type t)
        {
            if (t.GetInterface(typeof(IIntKeyedDataObject).Name) == null)
            {
                throw new DMError("Class {0} is not support {1} so it cannot take participation in Many-To-Many relationship", t.FullName, typeof(IIntKeyedDataObject).Name);
            } 
        }
    }

    public interface IDataObject {}

    public interface INamedDataObject : IDataObject
    {
        string Name { get; }
    }

    public interface IIntKeyedDataObject : IDataObject
    {
        int ID { get; }
    }

    public interface ISecuredDataObject : IIntKeyedDataObject, INamedDataObject {}

    public interface IFxDataObject : IIntKeyedDataObject, INamedDataObject { }

    public interface IRelationshipTable
    {
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

    [DebuggerDisplay("DataObject: {ID}")]
    public abstract class IntKeyedDataObject : DataObject, ICloneable
    {
        protected IntKeyedDataObject()
        {
            ((INotifyPropertyChanged)this).PropertyChanged += PropertyChangingHook;
        }

        private static void PropertyChangingHook(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ID")
            {
                throw new InvalidOperationException("Cannot change property because it is foreign key");
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public abstract class SecuredDataObject : IntKeyedDataObject
    {   
    }

    [DebuggerDisplay("Fx: {ID} - {Name}")]
    public abstract class FxDataObject : DataObject
    {
        protected FxDataObject()
        {
            ((INotifyPropertyChanging)this).PropertyChanging += (s, e) => { throw new DMError("Cannot change readonly object {0}", s.GetType().Name); };
        }
    }

    public abstract class RelTable
    {
        protected RelTable()
        {
            throw new DMError("Impossible to create relation dataobject. Please use methods of ServerModel.DB instead");
        }
    }

    public interface IDBOperator
    {
        int Insert<TDataObject>(TDataObject obj)
            where TDataObject : class, IIntKeyedDataObject, new();

        void Insert<TDataObject>(IList<TDataObject> objs)
            where TDataObject : class, IIntKeyedDataObject, new();

        void Update<TDataObject>(TDataObject obj)
            where TDataObject : class, IIntKeyedDataObject, new();

        void Update<TDataObject>(IList<TDataObject> objs)
            where TDataObject : class, IIntKeyedDataObject, new();

        TDataObject Load<TDataObject>(int id)
            where TDataObject : class, IIntKeyedDataObject, new();

        IList<TDataObject> Load<TDataObject>(IList<int> ids)
            where TDataObject : class, IIntKeyedDataObject, new();

        IList<TDataObject> LoadRange<TDataObject>(int from, int to)
            where TDataObject : class, IIntKeyedDataObject, new();

        void Delete<TDataObject>(int id)
            where TDataObject : class, IIntKeyedDataObject, new();

        void Delete<TDataObject>(IList<int> ids)
            where TDataObject : class, IIntKeyedDataObject, new();

        void Delete<TDataObject>(IList<TDataObject> objs)
            where TDataObject : class, IIntKeyedDataObject, new();

        ReadOnlyCollection<TFxDataObject> Fx<TFxDataObject>()
            where TFxDataObject : class, IFxDataObject;

        List<TDataObject> FullCached<TDataObject>()
            where TDataObject : class, IDataObject, new();

        List<TDataObject> Query<TDataObject>([NotNull] IDBCondition cond)
            where TDataObject : IDataObject, new();

        TDataObject QuerySingle<TDataObject>([NotNull] IDBCondition cond)
            where TDataObject : IDataObject, new();

        List<int> LookupIds<TDataObject>(IIntKeyedDataObject owner)
            where TDataObject : IDataObject;

        List<int> LookupMany2ManyIds<TDataObject>(IIntKeyedDataObject firstPart);

        void Link(IIntKeyedDataObject do1, IIntKeyedDataObject do2);

        void UnLink(IIntKeyedDataObject do1, IIntKeyedDataObject do2);

        int Count<TDataObject>()
            where TDataObject : IDataObject, new();
    }
}
