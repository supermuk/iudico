using System;
using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;
using LEX.CONTROLS;

namespace IUDICO.DataModel.DB
{
    public enum DB_OBJECT_TYPE
    {
        [SecuredObjectType("Course", typeof(TblCourses), typeof(FxCourseOperations))]
        COURSE,

        [SecuredObjectType("Theme", typeof(TblThemes), typeof(FxThemeOperations))]
        THEME,

        [SecuredObjectType("Stage", typeof(TblStages), typeof(FxStageOperations))]
        STAGE,

        [SecuredObjectType("Page", typeof(TblPages), typeof(FxPageOperations))]
        PAGE
    }

    public static class ObjectTypeHelper
    {
        public static SecuredObjectTypeAttribute GetSecurityAtr(this DB_OBJECT_TYPE obj)
        {
            return GetAttribute(obj);
        }

        public static bool IsSecured(Type t)
        {
            if (__SecuredTypes == null)
            {
                var fs = typeof (DB_OBJECT_TYPE).GetFields();

                __SecuredTypes = new List<Type>(fs.Length - 1);
                
                foreach (var f in fs)
                {
                    if (!f.IsSpecialName)
                    {
                        __SecuredTypes.Add(f.GetAtr<SecuredObjectTypeAttribute>().RuntimeClass);
                    }
                }
            }
            return __SecuredTypes.Contains(t);
        }

        public static bool IsSecured([NotNull] this DataObject d)
        {
            return IsSecured(d.GetType());
        }

        private static readonly Func<DB_OBJECT_TYPE, SecuredObjectTypeAttribute> GetAttribute = 
            new Memorizer<DB_OBJECT_TYPE, SecuredObjectTypeAttribute>(GetAttributeInternal);

        private static SecuredObjectTypeAttribute GetAttributeInternal(DB_OBJECT_TYPE obj)
        {
            return typeof (DB_OBJECT_TYPE).GetField(obj.ToString()).GetAtr<SecuredObjectTypeAttribute>();
        }

        private static List<Type> __SecuredTypes;
    }
}