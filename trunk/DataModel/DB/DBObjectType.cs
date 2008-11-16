using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.DB
{
    public enum DB_OBJECT_TYPE
    {
        [SecuredObjectType("Course", typeof(TblCourses))]
        COURSE
    }
}