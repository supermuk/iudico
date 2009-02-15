using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Common
{
    class TeacherHelper
    {
        public static IList<TblStages> StagesForTheme(TblThemes theme)
        {
            List<int> stagesIDs = ServerModel.DB.LookupMany2ManyIds<TblStages>(theme, null);
            return ServerModel.DB.Load<TblStages>(stagesIDs);
        }

        public static TblStages StageForTheme(TblThemes theme)
        {
            IList<TblStages> relatedStages = StagesForTheme(theme);
            if (relatedStages.Count > 0)
            {
                return relatedStages[0];
            }
            else
            {
                return null;
            }
        }

        public static IList<TblThemes> ThemesForStage(TblStages stage)
        {
            List<int> themesIDs = ServerModel.DB.LookupMany2ManyIds<TblThemes>(stage, null);
            return ServerModel.DB.Load<TblThemes>(themesIDs);
        }

        public static IList<TblStages> StagesForCurriculum(TblCurriculums curriculum)
        {
            List<int> stagesIDs = ServerModel.DB.LookupIds<TblStages>(curriculum, null);
            return ServerModel.DB.Load<TblStages>(stagesIDs);
        }

        public static IList<TblPermissions> PermissionsForCourse(TblCourses course)
        {
            return ServerModel.DB.Query<TblPermissions>(
                        new CompareCondition<int>(
                           DataObject.Schema.CourseRef,
                           new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblPermissions> PermissionsForCurriculum(TblCurriculums curriculum)
        {
            return ServerModel.DB.Query<TblPermissions>(
                        new CompareCondition<int>(
                           DataObject.Schema.CurriculumRef,
                           new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblPermissions> PermissionsForStage(TblStages stage)
        {
            return ServerModel.DB.Query<TblPermissions>(
                        new CompareCondition<int>(
                           DataObject.Schema.StageRef,
                           new ValueCondition<int>(stage.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblPermissions> PermissionsForTheme(TblThemes theme)
        {
            return ServerModel.DB.Query<TblPermissions>(
                        new CompareCondition<int>(
                           DataObject.Schema.ThemeRef,
                           new ValueCondition<int>(theme.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblCourses> MyCourses(FxCourseOperations operation)
        {
            //IList<int> myCoursesIDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.COURSE,
            //    ServerModel.User.Current.ID, operation.ID, DateTime.Now);

            //return ServerModel.DB.Load<TblCourses>(myCoursesIDs);

            return ServerModel.DB.Query<TblCourses>(
                  new InCondition<int>(DataObject.Schema.ID,
                     new SubSelectCondition<TblPermissions>("CourseRef",
                        new AndCondtion(
                           new CompareCondition<int>(
                              DataObject.Schema.OwnerUserRef,
                              new ValueCondition<int>(ServerModel.User.Current.ID), COMPARE_KIND.EQUAL),
                           new CompareCondition<int>(
                              DataObject.Schema.CourseOperationRef,
                              new ValueCondition<int>(operation.ID), COMPARE_KIND.EQUAL)))));
        }

        public static IList<TblCurriculums> MyCurriculums(FxCurriculumOperations operation)
        {
            //IList<int> myCurriculumsIDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM,
            //    ServerModel.User.Current.ID, operation.ID, DateTime.Now);

            //return ServerModel.DB.Load<TblCurriculums>(myCurriculumsIDs);

            return ServerModel.DB.Query<TblCurriculums>(
                  new InCondition<int>(DataObject.Schema.ID,
                     new SubSelectCondition<TblPermissions>("CurriculumRef",
                        new AndCondtion(
                           new CompareCondition<int>(
                              DataObject.Schema.OwnerUserRef,
                              new ValueCondition<int>(ServerModel.User.Current.ID), COMPARE_KIND.EQUAL),
                           new CompareCondition<int>(
                              DataObject.Schema.CurriculumOperationRef,
                              new ValueCondition<int>(operation.ID), COMPARE_KIND.EQUAL)))));
        }

        public static IList<TblThemes> ThemesForCourse(TblCourses course)
        {
            List<int> themesIDs = ServerModel.DB.LookupIds<TblThemes>(course, null);
            return ServerModel.DB.Load<TblThemes>(themesIDs);
        }

        public static IList<TblUsers> GetCurriculumOwners(TblCurriculums curriculum)
        {
            return ServerModel.DB.Query<TblUsers>(
                new InCondition<int>(DataObject.Schema.ID,
                    new SubSelectCondition<TblPermissions>("OwnerUserRef",
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.CurriculumRef,
                            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CurriculumOperationRef,
                            new ValueCondition<int>(FxCurriculumOperations.Modify.ID), COMPARE_KIND.EQUAL)))));
        }

        public static TblUsers GetCurriculumOwner(TblCurriculums curriculum)
        {
            IList<TblUsers> owners = GetCurriculumOwners(curriculum);
            if (owners.Count > 0)
            {
                return owners[0];
            }
            else
            {
                return null;
            }
        }

        public static IList<TblGroups> GetGroupsForCurriculum(TblCurriculums curriculum)
        {
            return ServerModel.DB.Query<TblGroups>(
                new InCondition<int>(DataObject.Schema.ID,
                    new SubSelectCondition<TblPermissions>("OwnerGroupRef",
                         new CompareCondition<int>(
                            DataObject.Schema.CurriculumRef,
                            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL))));
        }

        public static IList<TblCurriculums> GetCurriculumsForGroup(TblGroups group)
        {
            return ServerModel.DB.Query<TblCurriculums>(
                new InCondition<int>(DataObject.Schema.ID,
                    new SubSelectCondition<TblPermissions>("CurriculumRef",
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerGroupRef,
                            new ValueCondition<int>(group.ID), COMPARE_KIND.EQUAL))));
        }



        public static void UnSignGroupFromCurriculum(TblGroups group, TblCurriculums curriculum)
        {
            List<TblPermissions> persmissions = ServerModel.DB.Query<TblPermissions>(
                new AndCondtion(
                   new CompareCondition<int>(
                      DataObject.Schema.CurriculumRef,
                      new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL),
                   new CompareCondition<int>(
                      DataObject.Schema.OwnerGroupRef,
                      new ValueCondition<int>(group.ID), COMPARE_KIND.EQUAL)));

            ServerModel.DB.Delete<TblPermissions>(persmissions);
        }

        public static bool StageContainsTheme(int stageID, int themeID)
        {
            TblStages stage = ServerModel.DB.Load<TblStages>(stageID);
            TblThemes theme = ServerModel.DB.Load<TblThemes>(themeID);

            foreach (TblThemes childThemes in ThemesForStage(stage))
            {
                if (childThemes.ID == theme.ID)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
