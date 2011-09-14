using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;
using System.Web.UI.WebControls;
using System.Collections;


namespace IUDICO.DataModel.Common
{
    /// <summary>
    /// Class to retrive data (bind to Teacher) from database
    /// </summary>
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

        public static IList<TblItems> ItemsOfOrganization(TblOrganizations org)
        {
            return ServerModel.DB.Query<TblItems>(new CompareCondition<int>(
                              DataObject.Schema.OrganizationRef,
                              new ValueCondition<int>(org.ID), COMPARE_KIND.EQUAL));
           
        }

        public static IList<TblItems> LeafItemsOfOrganization(TblOrganizations org)
        {
            return ServerModel.DB.Query<TblItems>(
                  new AndCondition(
                     new CompareCondition<int>(
                           DataObject.Schema.OrganizationRef,
                           new ValueCondition<int>(org.ID), COMPARE_KIND.EQUAL),
                     new CompareCondition<bool>(
                           DataObject.Schema.IsLeaf,
                           new ValueCondition<bool>(true), COMPARE_KIND.EQUAL)));
        }

        public static IList<TblThemes> ThemesOfStage(TblStages stage)
        {
            return ServerModel.DB.Query<TblThemes>(new CompareCondition<int>(
                              DataObject.Schema.StageRef,
                              new ValueCondition<int>(stage.ID), COMPARE_KIND.EQUAL));
        }
        public static IList<TblLearnerAttempts> AttemptsOfTheme(TblThemes theme)
        {
            return ServerModel.DB.Query<TblLearnerAttempts>(new CompareCondition<int>(
                              DataObject.Schema.ThemeRef,
                              new ValueCondition<int>(theme.ID), COMPARE_KIND.EQUAL));
        }
       
        public static IList<TblLearnerSessions> SessionsOfAttempt(TblLearnerAttempts attempt)
        {
            return ServerModel.DB.Query<TblLearnerSessions>(new CompareCondition<int>(
                              DataObject.Schema.LearnerAttemptRef,
                              new ValueCondition<int>(attempt.ID), COMPARE_KIND.EQUAL));
        }
        public static IList<TblVarsInteractionCorrectResponses> CorrectResponsesOfSession(TblLearnerSessions session)
        {
            return ServerModel.DB.Query<TblVarsInteractionCorrectResponses>(new CompareCondition<int>(
                              DataObject.Schema.LearnerSessionRef,
                              new ValueCondition<int>(session.ID), COMPARE_KIND.EQUAL));
        }
        public static IList<TblVarsInteractions> VarInteractionsOfSession(TblLearnerSessions session)
        {
            return ServerModel.DB.Query<TblVarsInteractions>(new CompareCondition<int>(
                              DataObject.Schema.LearnerSessionRef,
                              new ValueCondition<int>(session.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblStages> StagesOfCurriculum(TblCurriculums curriculum)
        {
            return ServerModel.DB.Query<TblStages>(new CompareCondition<int>(
                              DataObject.Schema.CurriculumRef,
                              new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblOrganizations> OrganizationsOfCourse(TblCourses course)
        {
            return ServerModel.DB.Query<TblOrganizations>(new CompareCondition<int>(
                              DataObject.Schema.CourseRef,
                              new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblThemes> ThemesOfCourse(TblCourses course)
        {
            return ServerModel.DB.Query<TblThemes>(new CompareCondition<int>(
                              DataObject.Schema.CourseRef,
                              new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL));
        }

        public static IList<TblPermissions> CurrentUserPermissionsForCourse(TblCourses course)
        {
            return ServerModel.DB.Query<TblPermissions>(
                  new AndCondition(
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
                  new AndCondition(
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


        public static IList<TblPermissions> AllPermissionsForOrganization(TblOrganizations organization)
        {
            return ServerModel.DB.Query<TblPermissions>(
                        new CompareCondition<int>(
                           DataObject.Schema.OrganizationRef,
                           new ValueCondition<int>(organization.ID), COMPARE_KIND.EQUAL));
        }
        
        public static IList<TblPermissions> GroupPermissionsForCurriculum(TblGroups group, TblCurriculums curriculum)
        {
            return ServerModel.DB.Query<TblPermissions>(
                      new AndCondition(
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
                     new AndCondition(
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
/*
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
 */ 
        }

        public static IList<TblGroups> GetGroupsForCurriculum(TblCurriculums curriculum)
        {
            //IList<int> iDs = PermissionsManager.GetGroupsForObject(SECURED_OBJECT_TYPE.CURRICULUM, curriculum.ID, null, null);
            //return ServerModel.DB.Load<TblGroups>(iDs);

            return ServerModel.DB.Query<TblGroups>(
                new InCondition<int>(DataObject.Schema.ID,
                   new SubSelectCondition<TblPermissions>("OwnerGroupRef",
                      new AndCondition(
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
                      new AndCondition(
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
                new AndCondition(
                   new CompareCondition<int>(
                      DataObject.Schema.CurriculumRef,
                      new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL),
                   new CompareCondition<int>(
                      DataObject.Schema.OwnerGroupRef,
                      new ValueCondition<int>(group.ID), COMPARE_KIND.EQUAL)));

            ServerModel.DB.Delete<TblPermissions>(curriculumPermissions);

            foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curriculum))
            {
                List<TblPermissions> stagePermissions = ServerModel.DB.Query<TblPermissions>(
                new AndCondition(
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

            foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curriculum))
            {
                PermissionsManager.Grand(stage, FxStageOperations.View
                   , null, group.ID, DateTimeInterval.Full);
                PermissionsManager.Grand(stage, FxStageOperations.Pass
                   , null, group.ID, DateTimeInterval.Full);
            }
        }

        public static bool StageContainsTheme(int stageID, int orgID)
        {
            TblStages stage = ServerModel.DB.Load<TblStages>(stageID);
            TblOrganizations org = ServerModel.DB.Load<TblOrganizations>(orgID);

            foreach (TblThemes childThemes in TeacherHelper.ThemesOfStage(stage))
            {
                if (childThemes.OrganizationRef == org.ID)
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
                throw new Exception(Translations.TeacherHelper_Share_Only_user_based_permissions_can_be_shared);
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
/*
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
*/
        public static int GetLastLearnerAttempt(int UserID, int ThemeID)
        {
            List<TblLearnerAttempts> learnerAttempts = ServerModel.DB.Query<TblLearnerAttempts>(
                        new AndCondition(
                            new CompareCondition<int>(
                                DataObject.Schema.ThemeRef,
                                new ValueCondition<int>(ThemeID), COMPARE_KIND.EQUAL),
                            new CompareCondition<int>(
                                DataObject.Schema.UserRef,
                                new ValueCondition<int>(UserID), COMPARE_KIND.EQUAL)));
            if (learnerAttempts.Count > 0)
                return learnerAttempts[learnerAttempts.Count - 1].ID;
            else return 0;
        }
        public static int GetLastIndexOfAttempts(int UserID, int ThemeID)
        {
            List<TblLearnerAttempts> learnerAttempts = ServerModel.DB.Query<TblLearnerAttempts>(
                       new AndCondition(
                           new CompareCondition<int>(
                               DataObject.Schema.ThemeRef,
                               new ValueCondition<int>(ThemeID), COMPARE_KIND.EQUAL),
                           new CompareCondition<int>(
                               DataObject.Schema.UserRef,
                               new ValueCondition<int>(UserID), COMPARE_KIND.EQUAL)));
            return (learnerAttempts.Count);
            
        }

        public static TblUserAnswers GetUserAnswerForQuestion(TblUsers user, TblQuestions question)
        {
            IList<TblUserAnswers> answers = ServerModel.DB.Query<TblUserAnswers>(new AndCondition(
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
                      new AndCondition(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerUserRef,
                            new ValueCondition<int>(parent.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CourseRef,
                            new ValueCondition<int>(course.ID), COMPARE_KIND.EQUAL)));

            IList<TblPermissions> childPermissions =
                ServerModel.DB.Query<TblPermissions>(
                      new AndCondition(
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
                      new AndCondition(
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
                      new AndCondition(
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
                      new AndCondition(
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
                      new AndCondition(
                         new CompareCondition<int>(
                            DataObject.Schema.OwnerUserRef,
                            new ValueCondition<int>(parent.ID), COMPARE_KIND.EQUAL),
                         new CompareCondition<int>(
                            DataObject.Schema.CourseRef,
                            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL)));

            IList<TblPermissions> childPermissions =
                ServerModel.DB.Query<TblPermissions>(
                      new AndCondition(
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
                      new AndCondition(
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
                           new AndCondition(
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
                           new AndCondition(
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
                           new AndCondition(
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
                           new AndCondition(
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
        public static Table Sort(Table table, TblCurriculums curriculum)
        {
            Table temp = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.Text = Translations.TeacherHelper_Sort_Student;
            headerRow.Cells.Add(headerCell);

            foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curriculum))
            {
                foreach (TblThemes theme in TeacherHelper.ThemesOfStage(stage))
                {
                    headerCell = new TableHeaderCell();
                    headerCell.Text = theme.Name;
                    headerRow.Cells.Add(headerCell);
                }
            }
            headerCell = new TableHeaderCell();
            headerCell.Text = Translations.TeacherHelper_Sort_Total;
            headerRow.Cells.Add(headerCell);

            headerCell = new TableHeaderCell();
            headerCell.Text = Translations.TeacherHelper_Sort_Percent;
            headerRow.Cells.Add(headerCell);

            headerCell = new TableHeaderCell();
            headerCell.Text = "ECTS";
            headerRow.Cells.Add(headerCell);

            temp.Rows.Add(headerRow);

            for (int i = 1; i < table.Rows.Count; i++)
            {
                int max = 0;
                int row = 0;
                for (int j = 1; j < table.Rows.Count; j++)
                {
                    if (Int32.Parse(table.Rows[j].Cells[table.Rows[j].Cells.Count - 2].Text) > max)
                    {
                        max = Int32.Parse(table.Rows[j].Cells[table.Rows[j].Cells.Count - 2].Text);
                        row = j;

                    }
                }
                temp.Rows.Add(table.Rows[row]);
                table.Rows.RemoveAt(row);
                i--;

            }
            return temp;
        }

        public static Table Search_Function(Table table, string Search_Name, TblCurriculums curriculum, List<TblCurriculums> curriculums, int GroupID, string RawUrl)
        {
            Table temp = new Table();
            if (curriculum != null)
            {
                TableHeaderRow headerRow = new TableHeaderRow();
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = Translations.TeacherHelper_Sort_Student;
                headerRow.Cells.Add(headerCell);

                foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curriculum))
                {
                    foreach (TblThemes theme in TeacherHelper.ThemesOfStage(stage))
                    {
                        headerCell = new TableHeaderCell();
                        headerCell.Text = theme.Name;
                        headerRow.Cells.Add(headerCell);
                    }
                }
                headerCell = new TableHeaderCell();
                headerCell.Text = Translations.TeacherHelper_Sort_Total;
                headerRow.Cells.Add(headerCell);

                headerCell = new TableHeaderCell();
                headerCell.Text = Translations.TeacherHelper_Sort_Percent;
                headerRow.Cells.Add(headerCell);

                headerCell = new TableHeaderCell();
                headerCell.Text = "ECTS";
                headerRow.Cells.Add(headerCell);

                temp.Rows.Add(headerRow);


            }
            else
            {
                TableHeaderRow headerRow = new TableHeaderRow();

                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = Translations.TeacherHelper_Sort_Student;
                headerRow.Cells.Add(headerCell);



                foreach (TblCurriculums curr in curriculums)
                {
                    headerCell = new TableHeaderCell { HorizontalAlign = HorizontalAlign.Center };
                    headerCell.Controls.Add(new HyperLink
                    {
                        Text = curr.Name,
                        NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new IUDICO.DataModel.Controllers.StatisticShowController
                        {
                            GroupID = GroupID,
                            CurriculumID = curr.ID,
                            BackUrl = RawUrl
                        })
                    });
                    headerRow.Cells.Add(headerCell);
                }

                headerCell = new TableHeaderCell();
                headerCell.Text = Translations.TeacherHelper_Sort_Total;
                headerRow.Cells.Add(headerCell);

                headerCell = new TableHeaderCell();
                headerCell.Text = Translations.TeacherHelper_Sort_Percent;
                headerRow.Cells.Add(headerCell);

                headerCell = new TableHeaderCell();
                headerCell.Text = "ECTS";
                headerRow.Cells.Add(headerCell);


                temp.Rows.Add(headerRow);
            }
                for (int i = 1; i < table.Rows.Count; i++)
                {

                    string[] temp_array = table.Rows[i].Cells[0].Text.ToString().Split(' ');

                    if (temp_array.Length == 2)
                    {
                        if (Find_Student(Search_Name, temp_array[0], temp_array[1]) == true)
                        {

                            temp.Rows.Add(table.Rows[i]);
                            i--;
                        }

                    }
                    else
                    {

                        if (Find_Student(Search_Name, temp_array[0], "") == true)
                        {

                            temp.Rows.Add(table.Rows[i]);
                        }
                    }


                }
            

            return temp;

        }
        public static string ECTS_code(double Points)
        {
            if (Points >= 91)
                return "A";
            if ((Points >= 81) && (Points <= 90))
                return "B";
            if ((Points >= 71) && (Points <= 80))
                return "C";
            if ((Points >= 61) && (Points <= 70))
                return "D";
            if ((Points >= 51) && (Points <= 60))
                return "E";
            if ((Points >= 31) && (Points <= 50))
                return "F";
            if ((Points >= 0) && (Points <= 30))
                return "Fx";
            return null;
        }
        public static bool Find_Student(string Find_name, string Student_name, string Student_LastName)
        {
            string[] Find = Find_name.Split(' ');
            if (Student_name == null)
            {
                Student_name = "";
            }
            if (Student_LastName == null)
            {
                Student_LastName = "";
            }

            if ((Student_name.Length != 0) && (Student_LastName.Length != 0))
            {
                if (Find.Length == 2)
                {
                    if ((Student_name.Contains(Find[0]) == true) && (Student_LastName.Contains(Find[1]) == true))
                        return true;
                    else

                        if ((Student_name.Contains(Find[1]) == true) && (Student_LastName.Contains(Find[0]) == true))
                            return true;
                }
                else
                    if ((Student_name.Contains(Find[0]) == true) || (Student_LastName.Contains(Find[0]) == true))
                        return true;
            }
            else
            {
                if ((Student_name.Length != 0) && (Student_LastName.Length == 0) && (Find.Length == 1) && (Student_name.Contains(Find[0]) == true))
                    return true;
                else
                    if ((Student_name.Length == 0) && (Student_LastName.Length != 0) && (Find.Length == 1) && (Student_LastName.Contains(Find[0]) == true))
                        return true;
            }
            return false;

        }
    }
}
