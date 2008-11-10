using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.DB
{
    [DBObjects(3)]
    public enum DB_OBJECT_TYPE
    {
        [SecuredObjectType("Course", typeof(TblCourses))]
        COURSE
    }
}