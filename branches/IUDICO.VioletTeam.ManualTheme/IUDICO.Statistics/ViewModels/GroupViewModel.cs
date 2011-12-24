namespace IUDICO.Statistics.ViewModels
{
    public class GroupViewModel
    {
        #region Constructors

        public GroupViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
 
	    #endregion

        #region Public Properties

        public readonly string Name;

        public readonly int Id;
        
        #endregion
    }
}