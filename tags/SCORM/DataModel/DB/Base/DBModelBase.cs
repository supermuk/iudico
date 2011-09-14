using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    [BaseTypeRequired(typeof(INamedDataObject))]
    public class TableRecordAttribute : Attribute
    {
        public readonly string Name;

        public TableRecordAttribute(string name)
        {
            Name = name;
        }

        public TableRecordAttribute()
            : this(null)
        {
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
                throw new DMError(Translations.ManyToManyRelationshipAttribute_CheckSupport_Class__0__is_not_support__1__so_it_cannot_take_participation_in_Many_To_Many_relationship, t.FullName, typeof(IIntKeyedDataObject).Name);
            }
        }
    }

    public interface IDataObject { }

    public interface INamedDataObject : IDataObject
    {
        string Name { get; }
    }

    public interface IIntKeyedDataObject : IDataObject
    {
        int ID { get; }
    }

    public interface ISecuredDataObject : IIntKeyedDataObject, INamedDataObject { }

    public interface ISecuredDataObject<TOperations> : ISecuredDataObject
        where TOperations : IFxDataObject
    {        
    }

    public interface IFxDataObject : IIntKeyedDataObject, INamedDataObject { }

    public interface IRelationshipTable : IDataObject
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
        protected DataObject()
        {
            ((INotifyPropertyChanged)this).PropertyChanged += PropertyChangedHook;
        }

        private static void PropertyChangedHook(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "sysState" || e.PropertyName == "ID")
            {
                throw new InvalidOperationException("It's forbidden to change " + e.PropertyName + "property ");
            }
        }

        public static class Schema
        {
            public static readonly IDBPropertyCondition<int>
                ID = new PropertyCondition<int>("ID"),
                UserRef = new PropertyCondition<int>("UserRef"),
                QuestionRef = new PropertyCondition<int>("QuestionRef"),
                InteractionRef =new PropertyCondition<int>("InteractionRef"),
                ThemeRef = new PropertyCondition<int>("ThemeRef"),
                PageRef = new PropertyCondition<int>("PageRef"),
                StageRef = new PropertyCondition<int>("StageRef"),
                CourseRef = new PropertyCondition<int>("CourseRef"),
                OrganizationRef = new PropertyCondition<int>("OrganizationRef"),
                CurriculumRef = new PropertyCondition<int>("CurriculumRef"),
                CurriculumOperationRef = new PropertyCondition<int>("CurriculumOperationRef"),
                StageOperationRef = new PropertyCondition<int>("StageOperationRef"),
                ThemeOperationRef = new PropertyCondition<int>("ThemeOperationRef"),
                CourseOperationRef = new PropertyCondition<int>("CourseOperationRef"),
                OwnerGroupRef = new PropertyCondition<int>("OwnerGroupRef"),
                OwnerUserRef = new PropertyCondition<int>("OwnerUserRef"),
                ParentPermitionRef = new PropertyCondition<int>("ParentPermitionRef"),
                SysState = new PropertyCondition<int>("sysState"),
                ItemRef = new PropertyCondition<int>("ItemRef"),
                LearnerAttemptRef = new PropertyCondition<int>("LearnerAttemptRef"),
                LearnerSessionRef = new PropertyCondition<int>("LearnerSessionRef"),
                Number = new PropertyCondition<int>("Number");
            
            public static readonly IDBPropertyCondition<DateTime>
                DateSince = new PropertyCondition<DateTime>("DateSince"),
                DateTill = new PropertyCondition<DateTime>("DateTill"),
				Date = new PropertyCondition<DateTime>("Date");

            public static readonly IDBPropertyCondition<string>
                Name = new PropertyCondition<string>("Name"),
                Value = new PropertyCondition<string>("Value"),
                Login = new PropertyCondition<string>("Login"),
                FirstName = new PropertyCondition<string>("FirstName"),
                LastName = new PropertyCondition<string>("LastName"),
                Email = new PropertyCondition<string>("Email");

            public static readonly IDBPropertyCondition<bool>
                IsLeaf = new PropertyCondition<bool>("IsLeaf");
        }
    }

    public abstract class IntKeyedDataObject : DataObject, ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public abstract class SecuredDataObject<TOperations> : IntKeyedDataObject
        where TOperations : IFxDataObject
    {
    }

    [DebuggerDisplay("Fx: {ID} - {Name}")]
    public abstract class FxDataObject : DataObject
    {
        protected FxDataObject()
        {
            ((INotifyPropertyChanging)this).PropertyChanging += (s, e) => { throw new DMError(Translations.FxDataObject_FxDataObject_Cannot_change_readonly_object__0_, s.GetType().Name); };
        }
    }

    public abstract class RelTable
    {
        protected RelTable()
        {
            throw new DMError(Translations.RelTable_RelTable_Impossible_to_create_relation_dataobject__Please_use_methods_of_ServerModel_DB_instead);
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

        void Update<TDataObject>([NotNull] IDBUpdateOperation<TDataObject> op, [NotNull] IDbConnection cn, [CanBeNull] IDbTransaction transaction)
            where TDataObject : class, IDataObject, new();

        TDataObject Load<TDataObject>(int id)
            where TDataObject : class, IIntKeyedDataObject, new();

        IList<TDataObject> Load<TDataObject>(IList<int> ids)
            where TDataObject : class, IIntKeyedDataObject, new();

        void Delete<TDataObject>(int id)
            where TDataObject : class, IIntKeyedDataObject, new();

        void Delete<TDataObject>(IList<int> ids)
            where TDataObject : class, IIntKeyedDataObject, new();

        void Delete<TDataObject>(IList<TDataObject> objs)
            where TDataObject : class, IIntKeyedDataObject, new();

        ReadOnlyCollection<TFxDataObject> Fx<TFxDataObject>()
            where TFxDataObject : class, IFxDataObject;

        List<TDataObject> Query<TDataObject>([CanBeNull] IDBPredicate cond)
            where TDataObject : IDataObject, new();

        TDataObject QuerySingleOrDefault<TDataObject>([NotNull] IDBPredicate cond)
            where TDataObject : IDataObject, new();

        List<int> LookupIds<TDataObject>([NotNull]IIntKeyedDataObject owner, [CanBeNull] IDBPredicate condition)
            where TDataObject : IDataObject;

        List<int> LookupMany2ManyIds<TDataObject>(IIntKeyedDataObject firstPart, IDBPredicate condition);

        void Link(IIntKeyedDataObject do1, IIntKeyedDataObject do2);

        void UnLink(IIntKeyedDataObject do1, IIntKeyedDataObject do2);
    }
}
