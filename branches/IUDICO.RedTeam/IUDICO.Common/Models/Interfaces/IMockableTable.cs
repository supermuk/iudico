using System.Data.Linq;
using System.Linq;


namespace IUDICO.Common.Models.Interfaces
{
    public interface IMockableTable<TEntity> : ITable, IQueryable<TEntity>
    {

    }

}

