using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Common.StudentUtils
{
    public class StudentRecordFinder
    {
        public static List<TblUserAnswers>  GetUserAnswersForQuestion(int userId, int questionId)
        {
            throw new NotImplementedException();
        }

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
    }
}
