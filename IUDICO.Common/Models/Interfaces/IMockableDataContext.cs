using System;

namespace IUDICO.Common.Models.Interfaces
{
    public interface IMockableDataContext : IDisposable
    {
        void SubmitChanges();
    }
}
