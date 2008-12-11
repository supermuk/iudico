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
}