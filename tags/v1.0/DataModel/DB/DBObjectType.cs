using System;
using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Security;
using LEX.CONTROLS;

namespace IUDICO.DataModel.DB
{
    public enum SECURED_OBJECT_TYPE
    {
        [SecuredObjectType("Course", typeof(TblCourses), typeof(FxCourseOperations))]
        COURSE,

        [SecuredObjectType("Theme", typeof(TblThemes), typeof(FxThemeOperations))]
        THEME,

        [SecuredObjectType("Stage", typeof(TblStages), typeof(FxStageOperations))]
        STAGE,

        [SecuredObjectType("Group", typeof(TblGroups), typeof(FxGroupOperations))]
        GROUP,

        [SecuredObjectType("Curriculum", typeof(TblCurriculums), typeof(FxCurriculumOperations))]
        CURRICULUM
    }

    public static class ObjectTypeHelper
    {
        static ObjectTypeHelper()
        {
            var fs = typeof(SECURED_OBJECT_TYPE).GetFields();

            __SecuredTypes = new Dictionary<Type, SECURED_OBJECT_TYPE>(fs.Length - 1);

            foreach (var f in fs)
            {
                if (!f.IsSpecialName)
                {
                    __SecuredTypes.Add(f.GetAtr<SecuredObjectTypeAttribute>().RuntimeClass, (SECURED_OBJECT_TYPE)f.GetValue(null));
                }
            }
        }

        public static ICollection<SECURED_OBJECT_TYPE> All
        {
            get
            {
                return __SecuredTypes.Values;
            }
        }

        public static SecuredObjectTypeAttribute GetSecurityAtr(this SECURED_OBJECT_TYPE obj)
        {
            return GetAttribute(obj);
        }

        public static SECURED_OBJECT_TYPE GetObjectType([NotNull] Type t)
        {
            SECURED_OBJECT_TYPE res;
            if (!__SecuredTypes.TryGetValue(t, out res))
            {
                throw new DMError(Translations.ObjectTypeHelper_GetObjectType_Could_not_figure_out_secured_index_of__0___All_SecuredDataObject_s_classes_should_be_added_to__1_, t.FullName, typeof(SECURED_OBJECT_TYPE).Name);
            }
            return res;            
        }

        private static readonly Func<SECURED_OBJECT_TYPE, SecuredObjectTypeAttribute> GetAttribute = 
            new Memorizer<SECURED_OBJECT_TYPE, SecuredObjectTypeAttribute>(GetAttributeInternal);

        private static SecuredObjectTypeAttribute GetAttributeInternal(SECURED_OBJECT_TYPE obj)
        {
            return typeof (SECURED_OBJECT_TYPE).GetField(obj.ToString()).GetAtr<SecuredObjectTypeAttribute>();
        }

        private static readonly Dictionary<Type, SECURED_OBJECT_TYPE> __SecuredTypes;
    }
}