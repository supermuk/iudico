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

    public interface IDBPredicate : IDBCondition<bool>
    {
    }

    public interface ISubSelectCondition : IDBCondition
    {
    }
}