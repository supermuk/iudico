using System;
using System.Data.Linq;

namespace IUDICO.Common.Models.Interfaces
{
    public interface IMockableDataContext : IDisposable
    {
        ChangeConflictCollection ChangeConflicts { get; }

        void SubmitChanges();
        void SubmitChanges(ConflictMode failureMode);
    }
}
