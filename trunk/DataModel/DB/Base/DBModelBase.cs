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

    public interface INamedDataObject
    {
        string Name { get; }
    }

    public interface IIntKeyedDataObject
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
}
