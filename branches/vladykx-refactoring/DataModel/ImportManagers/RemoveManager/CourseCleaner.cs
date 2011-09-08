using IUDICO.DataModel.DB;
using System;
using System.IO;

namespace IUDICO.DataModel.ImportManagers.RemoveManager
{
    /// <summary>
    /// Class for cleaning courses
    /// </summary>
    class CourseCleaner
    {
        public static void DeleteCourse(int courseId)
        {
            var course = ServerModel.DB.Load<TblCourses>(courseId);
            var organizations = ServerModel.DB.LookupIds<TblOrganizations>(course, null);
            var resources = ServerModel.DB.LookupIds<TblResources>(course, null);
            var themes = ServerModel.DB.LookupIds<TblThemes>(course, null);

            foreach (var i in organizations)
            {
                DeleteOrganization(i);
            }

            foreach (var i in resources)
            {
                DeleteResource(i);
            }

            foreach (var i in themes)
            {
                DeleteTheme(i);
            }

            ServerModel.DB.Delete<TblCourses>(courseId);
            Directory.Delete(CourseManager.GetCoursePath(courseId), true);
        }

        public static void DeleteOrganization(int organizationID)
        {
            var organization = ServerModel.DB.Load<TblOrganizations>(organizationID);
            var items = ServerModel.DB.LookupIds<TblItems>(organization, null);

            foreach (var i in items)
            {
                ServerModel.DB.Delete<TblItems>(i);
            }

            ServerModel.DB.Delete<TblOrganizations>(organizationID);
        }

        public static void DeleteResource(int resourceID)
        {
            ServerModel.DB.Delete<TblResources>(resourceID);
        }

        public static void DeleteTheme(int themeId)
        {
            ServerModel.DB.Delete<TblThemes>(themeId);
        }
        /*
        public static void DeletePage(int pageId)
        {
            var page = ServerModel.DB.Load<TblPages>(pageId);
            DeleteFiles(page);

            var question = ServerModel.DB.LookupIds<TblQuestions>(page, null);

            //foreach (var i in question)
                //DeleteQuestion(i);


            //ServerModel.DB.Delete<TblPages>(pageId);
        }
        
        private static void DeleteFiles(TblCourses course)
        {
            var files = ServerModel.DB.Load<TblFiles>(ServerModel.DB.LookupIds<TblFiles>(page, null));

            throw new NotImplementedException();

            foreach (var file in files)
            {
                if (file.PID != null)
                    ServerModel.DB.Delete<TblFiles>(file.ID);
            }

            var folders = ServerModel.DB.Load<TblFiles>(ServerModel.DB.LookupIds<TblFiles>(page, null));

            foreach (var file in folders)
                ServerModel.DB.Delete<TblFiles>(file.ID);
        }
        
        private static void DeleteQuestion(int questionId)
        {
            var question = ServerModel.DB.Load<TblQuestions>(questionId);

            var userAnswersIds = ServerModel.DB.LookupIds<TblUserAnswers>(question, null);
            
            foreach (var ua in userAnswersIds)
                DeleteUserAnswer(ua);

            if (question.IsCompiled)
            {
                var compiledQuestionRef = (int)question.CompiledQuestionRef;
                question.CompiledQuestionRef = null;
                ServerModel.DB.Update(question);
                DeleteCompiledQuestion(compiledQuestionRef);
            }

            ServerModel.DB.Delete<TblQuestions>(questionId);
        }

        private static void DeleteCompiledQuestion(int compiledQuestionId)
        {
            var compiledQuestion = (ServerModel.DB.Load<TblCompiledQuestions>(compiledQuestionId));

            var compiledQuestionsData = ServerModel.DB.LookupIds<TblCompiledQuestionsData>(compiledQuestion, null);

            foreach (var i in compiledQuestionsData)
                DeleteCompiledQuestionData(i);

            ServerModel.DB.Delete<TblCompiledQuestions>(compiledQuestionId);
        }

        private static void DeleteCompiledQuestionData(int compiledQuestionDataId)
        {
            ServerModel.DB.Delete<TblCompiledQuestionsData>(compiledQuestionDataId);
        }
        
        private static void DeleteUserAnswer(int userAnswerId)
        {
            var userAnswer = ServerModel.DB.Load<TblUserAnswers>(userAnswerId);

            var compiledAnswersIds = ServerModel.DB.LookupIds<TblCompiledAnswers>(userAnswer, null);

            foreach (var ca in compiledAnswersIds)
                DeleteCompiledAnswer(ca);

            ServerModel.DB.Delete<TblUserAnswers>(userAnswerId);
        }

        private static void DeleteCompiledAnswer(int compiledAnswerId)
        {
            ServerModel.DB.Delete<TblCompiledAnswers>(compiledAnswerId);
        }
        */
    }
}
