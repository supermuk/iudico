namespace IUDICO.Common.Models
{
    public class DB: DBDataContext
    {
        protected static readonly DB instance = new DB();

        public static DB Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
