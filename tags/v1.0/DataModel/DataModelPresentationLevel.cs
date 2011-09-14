using System.Collections.ObjectModel;

namespace IUDICO.DataModel
{
    public interface IDataModelChange
    {
        void Apply();
    }

    public interface IDataModelPresenter
    {
        bool IsDirty { get; }
        ReadOnlyCollection<IDataModelChange> Changes { get; }
    }
}
