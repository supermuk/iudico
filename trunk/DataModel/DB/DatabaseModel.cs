using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IUDICO.DataModel.DB
{
    public class DBEnum<T>
        where T: struct
    {
        static DBEnum()
        {
            Values = new ReadOnlyCollection<string>(new List<string>(
                from f in typeof(T).GetFields() 
                where !f.IsSpecialName select f.Name));
        }

        public static readonly ReadOnlyCollection<string> Values;
    }

    public class DataObject
    {
        
    }

    public partial class DatabaseModel
    {
    }

    public enum FX_ROLE
    {
        STUDENT,
        LECTOR,
        TRAINER,
        ADMIN,
        SUPER_ADMIN
    }
}
