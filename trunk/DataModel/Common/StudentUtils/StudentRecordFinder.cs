using System.Collections.Generic;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Common.StudentUtils
{
    public class StudentRecordFinder
    {
        public static IList<TblCurriculums> GetCurriculumnsForUser(int userId)
        {
            var curriculumnIds = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.CURRICULUM, userId, null, null);

            return ServerModel.DB.Load<TblCurriculums>(curriculumnIds);
        }

        public static List<int> GetUserGroups(int userId)
        {
            var user = ServerModel.DB.Load<TblUsers>(userId);

            return  ServerModel.DB.LookupMany2ManyIds<TblGroups>(user, null);

        }

        public static IList<TblPermissions> GetAllPermissionsForCurrriculumn(int curriculumnId)
        {
            var curriculumn = ServerModel.DB.Load<TblCurriculums>(curriculumnId);

            var permissionsIds = ServerModel.DB.LookupIds<TblPermissions>(curriculumn, null);

            return ServerModel.DB.Load<TblPermissions>(permissionsIds);
        }

        public static IList<TblPermissions> GetAllPermissionsForStage(int stageId)
        {
            var stage = ServerModel.DB.Load<TblStages>(stageId);

            var permissionsIds = ServerModel.DB.LookupIds<TblPermissions>(stage, null);

            return ServerModel.DB.Load<TblPermissions>(permissionsIds);
        }

        public static IList<TblPermissions> GetPermissionsForCurriculumn(int userId, int curriculumnId, int curriculumnOperationId)
        {
            var result = new List<TblPermissions>();

            var groupsIds = GetUserGroups(userId);
            var permissionsForCurrriculumn = GetAllPermissionsForCurrriculumn(curriculumnId);

            foreach (var p in permissionsForCurrriculumn)
                if (p.CurriculumRef == curriculumnId && p.CurriculumOperationRef == curriculumnOperationId && groupsIds.Contains((int)p.OwnerGroupRef))
                    result.Add(p);

            return result;
        }

        public static IList<TblPermissions> GetPermissionsForStage(int userId, int stageId, int stageOperationId)
        {
            var result = new List<TblPermissions>();

            var groupsIds = GetUserGroups(userId);
            var permissionsForStage = GetAllPermissionsForStage(stageId);

            foreach (var p in permissionsForStage)
                if (p.StageRef == stageId && p.StageOperationRef == stageOperationId && groupsIds.Contains((int) p.OwnerGroupRef))
                    result.Add(p);

            return result;
        }

        public static IList<TblThemes> GetThemesForStage(TblStages stage)
        {
            List<int> themesIds = ServerModel.DB.LookupMany2ManyIds<TblThemes>(stage, null);
            return ServerModel.DB.Load<TblThemes>(themesIds);
        }

        public static IList<TblStages> GetStagesForCurriculum(TblCurriculums curriculum)
        {
            List<int> stagesIds = ServerModel.DB.LookupIds<TblStages>(curriculum, null);
            return ServerModel.DB.Load<TblStages>(stagesIds);
        }

        public static IList<TblQuestions> GetQuestionsForPage(int pageId)
        {
            var page = ServerModel.DB.Load<TblPages>(pageId);
            var questionsIds = ServerModel.DB.LookupIds<TblQuestions>(page, null);

            return ServerModel.DB.Load<TblQuestions>(questionsIds);
        }

        public static TblThemes GetThemeForPage(int pageId)
        {
            var page = ServerModel.DB.Load<TblPages>(pageId);
            return ServerModel.DB.Load<TblThemes>((int)page.ThemeRef);
        }

        public static IList<TblUserAnswers> GetAnswersForQuestion(TblQuestions question)
        {
            var allUsersAnswersIdsForQuestion = ServerModel.DB.LookupIds<TblUserAnswers>(question, null);

            return ServerModel.DB.Load<TblUserAnswers>(allUsersAnswersIdsForQuestion);
        }

        public static IList<TblUserAnswers> GetUserAnswersForQuestion(TblQuestions question, int userId)
        {
            var allAnswersForQuestion = GetAnswersForQuestion(question);

            var result = new List<TblUserAnswers>();

            foreach (var a in allAnswersForQuestion)
                if (a.UserRef == userId)
                    result.Add(a);

            return result;
        }

        public static List<TblUserAnswers> ExtractIncludedAnswers(IList<TblUserAnswers> userAnswers)
        {
            var result = new List<TblUserAnswers>();

            foreach (var ua in userAnswers)
                if (ua.AnswerTypeRef == FxAnswerType.UserAnswer.ID)
                    result.Add(ua);

            return result;
        }

        public static IList<TblUserAnswers> GetAnswersForUser(int userId)
        {
            var user = ServerModel.DB.Load<TblUsers>(userId);
             
            var answersIds = ServerModel.DB.LookupIds<TblUserAnswers>(user, null);

            return ServerModel.DB.Load<TblUserAnswers>(answersIds);
        }

        public static TblPages GetPageForQuestion(int questionId)
        {
            var que = ServerModel.DB.Load<TblQuestions>(questionId);

            return ServerModel.DB.Load<TblPages>((int)que.PageRef);
        }

        public static TblThemes GetTheme(int themeId)
        {
            return ServerModel.DB.Load<TblThemes>(themeId);
        }

        public static IList<TblQuestions> GetQuestionsForPage(TblPages page)
        {
            var questionsIDs = ServerModel.DB.LookupIds<TblQuestions>(page, null);

            return ServerModel.DB.Load<TblQuestions>(questionsIDs);
        }

        public static IList<TblCompiledAnswers> GetCompiledAnswersForAnswer(TblUserAnswers ua)
        {
            var compiledAnswersIds = ServerModel.DB.LookupIds<TblCompiledAnswers>(ua, null);

            return ServerModel.DB.Load<TblCompiledAnswers>(compiledAnswersIds);
        }

        public static TblCompiledQuestionsData GetCompiledQuestionDataForCompiledAnswer(TblCompiledAnswers ca)
        {
            return ServerModel.DB.Load<TblCompiledQuestionsData>(ca.CompiledQuestionsDataRef);
        }

        public static IList<TblPages> GetPagesForTheme(int themeId)
        {
            var theme = ServerModel.DB.Load<TblThemes>(themeId);

            List<int> pagesIds = ServerModel.DB.LookupIds<TblPages>(theme, null);

            return ServerModel.DB.Load<TblPages>(pagesIds);
        }

        public static IList<TblPages> GetCoursePages(int courseId)
        {
            var course = ServerModel.DB.Load<TblCourses>(courseId);

            var themesIds = ServerModel.DB.LookupIds<TblThemes>(course, null);
            var themes = ServerModel.DB.Load<TblThemes>(themesIds);

            var allPagesIds = new List<int>();

            foreach (var theme in themes)
            {
                var pagesIds = ServerModel.DB.LookupIds<TblPages>(theme, null);
                allPagesIds.AddRange(pagesIds);
            }

            return ServerModel.DB.Load<TblPages>(allPagesIds);
        }
    }
}
