using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.Dao.Entity
{
    public class ThemeEntity
    {
        private readonly int courseRef;
        private readonly int id;
        private readonly int isControl;
        private readonly string name;

        public ThemeEntity(int courseRef, string name, bool isControl)
        {
            id = UniqueId.Generate();
            this.courseRef = courseRef;
            this.name = name;
            this.isControl = isControl ? 1 : 0;
        }

        public string Name
        {
            get { return name; }
        }

        public int CourseRef
        {
            get { return courseRef; }
        }

        public int Id
        {
            get { return id; }
        }

        public int IsControl
        {
            get { return isControl; }
        }
    }
}