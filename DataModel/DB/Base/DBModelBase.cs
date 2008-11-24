using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.DB.Base
{
    public class DBEnumAttribute : Attribute
    {
        public readonly string TableName;

        public DBEnumAttribute(string tableName)
        {
            TableName = tableName;
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
    public abstract class IntKeyedDataObject : DataObject
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
    }

    [DebuggerDisplay("Fx: {ID} - {Name}")]
    public abstract class FxDataObject : DataObject
    {
        protected FxDataObject()
        {
            ((INotifyPropertyChanging)this).PropertyChanging += (s, e) => { throw new DMError("Cannot change readonly object {0}", s.GetType().Name); };
        }
    }
}
