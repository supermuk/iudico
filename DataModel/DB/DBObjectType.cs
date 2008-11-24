using System;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.DB
{
    public enum DB_OBJECT_TYPE
    {
        [SecuredObjectType("Course", typeof(TblCourses), typeof(FxCourseOperations))]
        COURSE,

        [SecuredObjectType("Theme", typeof(TblThemes), typeof(FxThemeOperations))]
        THEME,

        [SecuredObjectType("Stage", typeof(TblStages), typeof(FxStageOperations))]
        STAGE
    }

    public static class ObjectTypeHelper
    {
        public static SecuredObjectTypeAttribute GetSecurityAtr(this DB_OBJECT_TYPE obj)
        {
            return GetAttribute(obj);
        }

        private static readonly Func<DB_OBJECT_TYPE, SecuredObjectTypeAttribute> GetAttribute = 
            new Memorizer<DB_OBJECT_TYPE, SecuredObjectTypeAttribute>(GetAttributeInternal);

        private static SecuredObjectTypeAttribute GetAttributeInternal(DB_OBJECT_TYPE obj)
        {
            return typeof (DB_OBJECT_TYPE).GetField(obj.ToString()).GetAtr<SecuredObjectTypeAttribute>();
        }
    }
}