using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Common
{
    public static class TeacherHelper
    {
        public static IList<TblCourses> CurrentUserCourses(FxCourseOperations operation)
        {
            IList<int> iDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.COURSE, ServerModel.User.Current.ID, operation.ID, null);
            return ServerModel.DB.Load<TblCourses>(iDs);
        }

        public static IList<TblCourses> CurrentUserCourses()
        {
            IList<int> iDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.COURSE, ServerModel.User.Current.ID, null, null);
            return ServerModel.DB.Load<TblCourses>(iDs);
        }

        public static IList<TblCurriculums> CurrentUserCurriculums(FxCurriculumOperations operation)
        {
            IList<int> iDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM, ServerModel.User.Current.ID, operation.ID, null);
            return ServerModel.DB.Load<TblCurriculums>(iDs);
        }

        public static IList<TblCurriculums> CurrentUserCurriculums()
        {
            IList<int> iDs = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM, ServerModel.User.Current.ID, null, null);
            return ServerModel.DB.Load<TblCurriculums>(iDs);
        }

        public static IList<TblStages> StagesForTheme(TblThemes theme)
        {
            List<int> stagesIDs = ServerModel.DB.LookupMany2ManyIds<TblStages>(theme, null);
            return ServerModel.DB.Load<TblStages>(stagesIDs);
        }

        public static IList<TblThemes> ThemesOfStage(TblStages stage)
        {
            List<int> themesIDs = ServerModel.DB.LookupMany2ManyIds<TblThemes>(stage, null);
            return ServerModel.DB.Load<TblThemes>(themesIDs);
        }

        public static IList<TblStages> StagesOfCurriculum(TblCurriculums curriculum)
        {
            List<int> stagesIDs = ServerModel.DB.LookupIds<TblStages>(curriculum, null);
            return ServerModel.DB.Load<TblStages>(stagesIDs);
        }

        public static IList<TblThemes> ThemesOfCourse(TblCourses course)
        {
            List<int> themesIDs = ServerModel.DB.LookupIds<TblThemes>(course, null);
            return ServerModel.DB.Load<TblThemes>(themesIDs);
        }

        public static IList<TblPermissions> CurrentUserPermissionsForCourse(TblCourses course)
        {
            return ServerModel.DB.Query<TblPermissions>(
                  new AndCondtion(
                     new CompareCondition<int>(
                           DataObject.Schema.CourseRef,
                           new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL),
                     new CompareCondition<int>(
                           DataObject.Schema.OwnerUserRef,
                           new ValueCondition<int>(ServerModel.User.Current.ID), COMPARE_KIND.EQUAL)));
        }

        public static IList<TblPermissions> CurrentUserPermissionsForCurriculum(TblCurriculums curriculum)
        {
            return ServerModel.DB.Query<TblPermissions>(
                  new AndCondtion(
                     new CompareCondition<int>(
                           DataObject.Schema.CurriculumRef,
                           new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL),
                     new CompareCondition<int>(
                           DataObject.Schema.OwnerUserRef,
                           new ValueCondition<int>(ServerModel.User.Current.ID), COMPARE_KIND.EQUAL)));
        }

        public static IList<TblPermissions> AllPermissionsForCourse(TblCourses course)
        {
            return ServerModel.DB.Query<TblPermissions>(
                        new CompareCondition<int>(
                           DataObject.Schema.CourseRef,
                           new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblPermissions> AllPermissionsForCurriculum(TblCurriculums curriculum)
        {
            return ServerModel.DB.Query<TblPermissions>(
                        new CompareCondition<int>(
                           DataObject.Schema.CurriculumRef,
                           new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblPermissions> AllPermissionsForStage(TblStages stage)
        {
            return ServerModel.DB.Query<TblPermissions>(
                        new CompareCondition<int>(
                           DataObject.Schema.StageRef,
                           new ValueCondition<int>(stage.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblPermissions> AllPermissionsForTheme(TblThemes theme)
        {
            return ServerModel.DB.Query<TblPermissions>(
                        new CompareCondition<int>(
                           DataObject.Schema.ThemeRef,
                           new ValueCondition<int>(theme.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblPermissions> GroupPermissionsForCurriculum(TblGroups group, TblCurriculums curriculum)
        {
            return ServerModel.DB.Query<TblPermissions>(
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.CurriculumRef,
                            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerGroupRef,
                            new ValueCondition<int>(group.ID), COMPARE_KIND.EQUAL)));
        }

        public static IList<TblPermissions> GroupPermissionsForStage(TblGroups group, TblStages stage)
        {
            return ServerModel.DB.Query<TblPermissions>(
                     new AndCondtion(
                        new CompareCondition<int>(
                           DataObject.Schema.StageRef,
                           new ValueCondition<int>(stage.ID), COMPARE_KIND.EQUAL),
                        new CompareCondition<int>(
                            DataObject.Schema.OwnerGroupRef,
                            new ValueCondition<int>(group.ID), COMPARE_KIND.EQUAL)));
        }

        public static IList<TblUsers> GetCurriculumOwners(TblCurriculums curriculum)
        {
            IList<int> iDs = PermissionsManager.GetUsersForObject(SECURED_OBJECT_TYPE.CURRICULUM, curriculum.ID, null, null);
            return ServerModel.DB.Load<TblUsers>(iDs);

            return ServerModel.DB.Query<TblUsers>(
                new InCondition<int>(DataObject.Schema.ID,
                    new SubSelectCondition<TblPermissions>("OwnerUserRef",
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.CurriculumRef,
                            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CurriculumOperationRef,
                            new ValueCondition<int>(FxCurriculumOperations.Modify.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                              DataObject.Schema.SysState,
                              new ValueCondition<int>(0), COMPARE_KIND.EQUAL)))));
        }

        public static IList<TblGroups> GetGroupsForCurriculum(TblCurriculums curriculum)
        {
            //IList<int> iDs = PermissionsManager.GetGroupsForObject(SECURED_OBJECT_TYPE.CURRICULUM, curriculum.ID, null, null);
            //return ServerModel.DB.Load<TblGroups>(iDs);

            return ServerModel.DB.Query<TblGroups>(
                new InCondition<int>(DataObject.Schema.ID,
                   new SubSelectCondition<TblPermissions>("OwnerGroupRef",
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.CurriculumRef,
                            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.SysState,
                            new ValueCondition<int>(0), COMPARE_KIND.EQUAL)))));
        }

        public static IList<TblCurriculums> GetCurriculumsForGroup(TblGroups group)
        {
            //IList<int> iDs = PermissionsManager.GetObjectsForGroup(SECURED_OBJECT_TYPE.CURRICULUM, group.ID, null, null);
            //return ServerModel.DB.Load<TblCurriculums>(iDs);

            return ServerModel.DB.Query<TblCurriculums>(
                new InCondition<int>(DataObject.Schema.ID,
                   new SubSelectCondition<TblPermissions>("CurriculumRef",
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerGroupRef,
                            new ValueCondition<int>(group.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.SysState,
                            new ValueCondition<int>(0), COMPARE_KIND.EQUAL)))));
        }

        public static void UnSignGroupFromCurriculum(TblGroups group, TblCurriculums curriculum)
        {
            List<TblPermissions> curriculumPermissions = ServerModel.DB.Query<TblPermissions>(
                new AndCondtion(
                   new CompareCondition<int>(
                      DataObject.Schema.CurriculumRef,
                      new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL),
                   new CompareCondition<int>(
                      DataObject.Schema.OwnerGroupRef,
                      new ValueCondition<int>(group.ID), COMPARE_KIND.EQUAL)));

            ServerModel.DB.Delete<TblPermissions>(curriculumPermissions);

            foreach (TblStages stage in StagesOfCurriculum(curriculum))
            {
                List<TblPermissions> stagePermissions = ServerModel.DB.Query<TblPermissions>(
                new AndCondtion(
                   new CompareCondition<int>(
                      DataObject.Schema.StageRef,
                      new ValueCondition<int>(stage.ID), COMPARE_KIND.EQUAL),
                   new CompareCondition<int>(
                      DataObject.Schema.OwnerGroupRef,
                      new ValueCondition<int>(group.ID), COMPARE_KIND.EQUAL)));

                ServerModel.DB.Delete<TblPermissions>(stagePermissions);
            }

        }

        public static void SignGroupForCurriculum(TblGroups group, TblCurriculums curriculum)
        {
            PermissionsManager.Grand(curriculum, FxCurriculumOperations.View
                    , null, group.ID, DateTimeInterval.Full);
            PermissionsManager.Grand(curriculum, FxCurriculumOperations.Pass
                    , null, group.ID, DateTimeInterval.Full);

            foreach (TblStages stage in StagesOfCurriculum(curriculum))
            {
                PermissionsManager.Grand(stage, FxStageOperations.View
                   , null, group.ID, DateTimeInterval.Full);
                PermissionsManager.Grand(stage, FxStageOperations.Pass
                   , null, group.ID, DateTimeInterval.Full);
            }
        }

        public static bool StageContainsTheme(int stageID, int themeID)
        {
            TblStages stage = ServerModel.DB.Load<TblStages>(stageID);
            TblThemes theme = ServerModel.DB.Load<TblThemes>(themeID);
            foreach (TblThemes childThemes in ThemesOfStage(stage))
            {
                if (childThemes.ID == theme.ID)
                {
                    return true;
                }
            }
            return false;
        }

        public static void Share(TblPermissions myPermission, int userID, bool canBeDelegated)
        {
            TblPermissions sharedPermission = myPermission.Clone() as TblPermissions;
            sharedPermission.CanBeDelagated = canBeDelegated;
            if (sharedPermission.OwnerGroupRef != null)
            {
                throw new Exception("Only user based permissions can be shared");
            }
            sharedPermission.OwnerUserRef = userID;
            sharedPermission.ParentPermitionRef = myPermission.ID;
            ServerModel.DB.Insert<TblPermissions>(sharedPermission);
        }

        public static IList<TblUsers> GetTeachers()
        {
            List<int> teachersIDs = ServerModel.DB.LookupMany2ManyIds<TblUsers>(FxRoles.LECTOR, null);
            return ServerModel.DB.Load<TblUsers>(teachersIDs);
        }

        public static IList<TblUsers> GetStudentsOfGroup(TblGroups group)
        {
            List<int> studentsIDs = ServerModel.DB.LookupMany2ManyIds<TblUsers>(group, null);
            return ServerModel.DB.Load<TblUsers>(studentsIDs);
        }

        public static IList<TblPages> PagesOfTheme(TblThemes theme)
        {
            return ServerModel.DB.Query<TblPages>(new CompareCondition<int>(
                              DataObject.Schema.ThemeRef,
                              new ValueCondition<int>(theme.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblQuestions> QuestionsOfPage(TblPages page)
        {
            return ServerModel.DB.Query<TblQuestions>(new CompareCondition<int>(
                                 DataObject.Schema.PageRef,
                                 new ValueCondition<int>(page.ID), COMPARE_KIND.EQUAL));
        }

        public static TblUserAnswers GetUserAnswerForQuestion(TblUsers user, TblQuestions question)
        {
            IList<TblUserAnswers> answers = ServerModel.DB.Query<TblUserAnswers>(new AndCondtion(
                new CompareCondition<int>(
                                     DataObject.Schema.UserRef,
                                     new ValueCondition<int>(user.ID), COMPARE_KIND.EQUAL),
                new CompareCondition<int>(
                                     DataObject.Schema.QuestionRef,
                                     new ValueCondition<int>(question.ID), COMPARE_KIND.EQUAL)));

            TblUserAnswers lastAnswer = null;
            foreach (TblUserAnswers answer in answers)
            {
                if (lastAnswer == null)
                {
                    lastAnswer = answer;
                }
                else
                {
                    if (lastAnswer.Date < answer.Date)
                    {
                        lastAnswer = answer;
                    }
                }
            }

            return lastAnswer;
        }

        public static IList<FxCourseOperations> CourseOperations()
        {
            return ServerModel.DB.Query<FxCourseOperations>(null);
        }

        public static IList<FxCurriculumOperations> CurriculumsOperations()
        {
            return ServerModel.DB.Query<FxCurriculumOperations>(null);
        }

        public static IList<TblGroups> GetUserGroups(TblUsers user)
        {
            IList<int> groupIDs = ServerModel.DB.LookupMany2ManyIds<TblGroups>(user, null);
            return ServerModel.DB.Load<TblGroups>(groupIDs);
        }

        public static bool AreParentAndChildByCourse(TblUsers parent, TblUsers child, TblCourses course)
        {
            IList<TblPermissions> parentPermissions =
                ServerModel.DB.Query<TblPermissions>(
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerUserRef,
                            new ValueCondition<int>(parent.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CourseRef,
                            new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL)));

            IList<TblPermissions> childPermissions =
                ServerModel.DB.Query<TblPermissions>(
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerUserRef,
                            new ValueCondition<int>(child.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CourseRef,
                            new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL)));

            foreach (TblPermissions parentPermission in parentPermissions)
            {
                foreach (TblPermissions childPermission in childPermissions)
                {
                    if (childPermission.ParentPermitionRef.HasValue
                        && childPermission.ParentPermitionRef.Value == parentPermission.ID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool AreParentAndChildByCourse(TblPermissions parentPermission, TblUsers child, TblCourses course)
        {
            IList<TblPermissions> childPermissions =
                ServerModel.DB.Query<TblPermissions>(
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerUserRef,
                            new ValueCondition<int>(child.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CourseRef,
                            new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL)));

            foreach (TblPermissions childPermission in childPermissions)
            {
                if (childPermission.ParentPermitionRef.HasValue
                    && childPermission.ParentPermitionRef.Value == parentPermission.ID)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CanChildDelegateCourse(TblPermissions parentPermission, TblUsers child, TblCourses course)
        {
            IList<TblPermissions> childPermissions =
                ServerModel.DB.Query<TblPermissions>(
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerUserRef,
                            new ValueCondition<int>(child.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CourseRef,
                            new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL)));

            foreach (TblPermissions childPermission in childPermissions)
            {
                if (childPermission.ParentPermitionRef.HasValue
                    && childPermission.ParentPermitionRef.Value == parentPermission.ID)
                {
                    return childPermission.CanBeDelagated;
                }
            }

            return false;
        }

        public static bool CanChildDelegateCurriculum(TblPermissions parentPermission, TblUsers child, TblCurriculums curriculum)
        {
            IList<TblPermissions> childPermissions =
                ServerModel.DB.Query<TblPermissions>(
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerUserRef,
                            new ValueCondition<int>(child.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CurriculumRef,
                            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL)));

            foreach (TblPermissions childPermission in childPermissions)
            {
                if (childPermission.ParentPermitionRef.HasValue
                    && childPermission.ParentPermitionRef.Value == parentPermission.ID)
                {
                    return childPermission.CanBeDelagated;
                }
            }

            return false;
        }

        public static bool AreParentAndChildByCurriculum(TblUsers parent, TblUsers child, TblCurriculums curriculum)
        {
            IList<TblPermissions> parentPermissions =
                ServerModel.DB.Query<TblPermissions>(
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerUserRef,
                            new ValueCondition<int>(parent.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CourseRef,
                            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL)));

            IList<TblPermissions> childPermissions =
                ServerModel.DB.Query<TblPermissions>(
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerUserRef,
                            new ValueCondition<int>(child.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CourseRef,
                            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL)));

            foreach (TblPermissions parentPermission in parentPermissions)
            {
                foreach (TblPermissions childPermission in childPermissions)
                {
                    if (childPermission.ParentPermitionRef.HasValue
                        && childPermission.ParentPermitionRef.Value == parentPermission.ID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool AreParentAndChildByCurriculum(TblPermissions parentPermission, TblUsers child, TblCurriculums curriculum)
        {
            IList<TblPermissions> childPermissions =
                ServerModel.DB.Query<TblPermissions>(
                      new AndCondtion(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerUserRef,
                            new ValueCondition<int>(child.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CurriculumRef,
                            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL)));

            foreach (TblPermissions childPermission in childPermissions)
            {
                if (childPermission.ParentPermitionRef.HasValue
                    && childPermission.ParentPermitionRef.Value == parentPermission.ID)
                {
                    return true;
                }
            }

            return false;
        }

        public static TblPermissions CurrentUserPermissionForCourse(TblCourses course, FxCourseOperations operation)
        {
            IList<TblPermissions> permissions = ServerModel.DB.Query<TblPermissions>(
                           new AndCondtion(
                              new CompareCondition<int>(
                                 DataObject.Schema.OwnerUserRef,
                                 new ValueCondition<int>(ServerModel.User.Current.ID), COMPARE_KIND.EQUAL),
                              new CompareCondition<int>(
                                 DataObject.Schema.CourseOperationRef,
                                 new ValueCondition<int>(operation.ID), COMPARE_KIND.EQUAL),
                              new CompareCondition<int>(
                                 DataObject.Schema.CourseRef,
                                 new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL)));
            if (permissions.Count == 0)
            {
                return null;
            }

            return permissions[0];

        }

        public static TblPermissions CurrentUserPermissionForCurriculum(TblCurriculums curriculum, FxCurriculumOperations operation)
        {
            IList<TblPermissions> permissions = ServerModel.DB.Query<TblPermissions>(
                           new AndCondtion(
                              new CompareCondition<int>(
                                 DataObject.Schema.OwnerUserRef,
                                 new ValueCondition<int>(ServerModel.User.Current.ID), COMPARE_KIND.EQUAL),
                              new CompareCondition<int>(
                                 DataObject.Schema.CurriculumOperationRef,
                                 new ValueCondition<int>(operation.ID), COMPARE_KIND.EQUAL),
                              new CompareCondition<int>(
                                 DataObject.Schema.CurriculumRef,
                                 new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL)));
            if (permissions.Count == 0)
            {
                return null;
            }

            return permissions[0];

        }

        public static void RemoveChildPermissions(TblPermissions parentPermission)
        {
            IList<TblPermissions> childPermissions = ServerModel.DB.Query<TblPermissions>(
                              new CompareCondition<int>(
                                 DataObject.Schema.ParentPermitionRef,
                                 new ValueCondition<int>(parentPermission.ID), COMPARE_KIND.EQUAL));

            foreach (TblPermissions childPermission in childPermissions)
            {
                RemoveChildPermissions(childPermission);
                ServerModel.DB.Delete<TblPermissions>(childPermission.ID);
            }
        }

        public static TblPermissions GetPermissionForCourse(TblPermissions parentPermission, TblUsers user, TblCourses course, FxCourseOperations operation)
        {
            IList<TblPermissions> permissions = ServerModel.DB.Query<TblPermissions>(
                           new AndCondtion(
                              new CompareCondition<int>(
                                 DataObject.Schema.OwnerUserRef,
                                 new ValueCondition<int>(user.ID), COMPARE_KIND.EQUAL),
                              new CompareCondition<int>(
                                 DataObject.Schema.CourseOperationRef,
                                 new ValueCondition<int>(operation.ID), COMPARE_KIND.EQUAL),
                              new CompareCondition<int>(
                                 DataObject.Schema.CourseRef,
                                 new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL),
                              new CompareCondition<int>(
                                 DataObject.Schema.ParentPermitionRef,
                                 new ValueCondition<int>(parentPermission.ID), COMPARE_KIND.EQUAL)));

            if (permissions.Count == 0)
            {
                return null;
            }

            return permissions[0];
        }

        public static TblPermissions GetPermissionForCurriculum(TblPermissions parentPermission, TblUsers user, TblCurriculums curriculum, FxCurriculumOperations operation)
        {
            IList<TblPermissions> permissions = ServerModel.DB.Query<TblPermissions>(
                           new AndCondtion(
                              new CompareCondition<int>(
                                 DataObject.Schema.OwnerUserRef,
                                 new ValueCondition<int>(user.ID), COMPARE_KIND.EQUAL),
                              new CompareCondition<int>(
                                 DataObject.Schema.CurriculumOperationRef,
                                 new ValueCondition<int>(operation.ID), COMPARE_KIND.EQUAL),
                              new CompareCondition<int>(
                                 DataObject.Schema.CurriculumRef,
                                 new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL),
                              new CompareCondition<int>(
                                 DataObject.Schema.ParentPermitionRef,
                                 new ValueCondition<int>(parentPermission.ID), COMPARE_KIND.EQUAL)));

            if (permissions.Count == 0)
            {
                return null;
            }

            return permissions[0];
        }

        public static IList<TblCompiledAnswers> GetCompiledAnswers(TblUserAnswers userAnswer)
        {
            return ServerModel.DB.Load<TblCompiledAnswers>(ServerModel.DB.LookupIds<TblCompiledAnswers>(userAnswer, null));
        }
    }
}
