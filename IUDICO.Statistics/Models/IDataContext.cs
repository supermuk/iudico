using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models
{
    public interface IDataContext
    {
        IMockableTable<ManualResult> ManualResults { get; }
    }
}
