using System.Collections.Generic;
using LEX.CONTROLS;

namespace IUDICO.DataModel.DB.Base
{
    public interface IDBOperatorStatement : IDBCondition
    {
    }

    public interface IDBCondition
    {
        void Write([NotNull]SqlSerializationContext context);
    }

    public interface IDBCondition<TResult> : IDBCondition
    {
    }

    public interface IDBPropertyCondition<TProperty> : IDBCondition<TProperty>
    {
        string PropertyName { get; }
    }

    public interface IDBPredicate : IDBCondition<bool>
    {
    }

    public interface ISubSelectCondition : IDBCondition
    {
    }

    public interface IDBPropertyAssignementBase : IDBCondition
    {       
    }

    public interface IDBPropertyAssignement<TProperty> : IDBPropertyAssignementBase
    {
        [NotNull]
        IDBPropertyCondition<TProperty> PropertyName { get; }
        [NotNull]
        IDBCondition<TProperty> Value { get; }
    }

    public interface IDBUpdateOperation<TDataObject> : IDBCondition
        where TDataObject : IDataObject, new()
    {
        [NotNull]
        IEnumerable<IDBPropertyAssignementBase> Assignements { get; }
        [CanBeNull]
        IDBPredicate Condition { get; }
    }
}