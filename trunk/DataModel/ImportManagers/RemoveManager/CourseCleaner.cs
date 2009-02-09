using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers.RemoveManager
{
    class CourseCleaner
    {
        public static void deleteCourse(int courseId)
        {
            var course = ServerModel.DB.Load<TblCourses>(courseId);
            var themes = ServerModel.DB.LookupIds<TblThemes>(course, null);

            foreach (var i in themes)
            {
                deleteTheme(i);
            }
            ServerModel.DB.Delete<TblCourses>(courseId);
        }

        public static void deleteTheme(int themeId)
        {
            var theme = ServerModel.DB.Load<TblThemes>(themeId);
            var pages = ServerModel.DB.LookupIds<TblPages>(theme, null);

            foreach (var i in pages)
            {
                deletePage(i);
            }
            ServerModel.DB.Delete<TblThemes>(themeId);
        }

        public static void deletePage(int pageId)
        {
            var page = ServerModel.DB.Load<TblPages>(pageId);
            deleteFiles(page);

            var question = ServerModel.DB.LookupIds<TblQuestions>(page, null);

            foreach (var i in question)
            {
                deleteQuestion(i);
            }

            ServerModel.DB.Delete<TblPages>(pageId);
        }

        private static void deleteFiles(TblPages page)
        {
            var files = ServerModel.DB.Load<TblFiles>(ServerModel.DB.LookupIds<TblFiles>(page, null));

            foreach (var file in files)
            {
                if (file.PID != null)
                {
                    ServerModel.DB.Delete<TblFiles>(file.ID);
                }
            }

            var folders = ServerModel.DB.Load<TblFiles>(ServerModel.DB.LookupIds<TblFiles>(page, null));

            foreach (var file in folders)
            {
                ServerModel.DB.Delete<TblFiles>(file.ID);
            }
        }

        private static void deleteQuestion(int questionId)
        {
            var question = ServerModel.DB.Load<TblQuestions>(questionId);

            if (question.IsCompiled)
            {
                int compiledQuestionRef = (int)question.CompiledQuestionRef;
                question.CompiledQuestionRef = null;
                ServerModel.DB.Update<TblQuestions>(question);
                deleteCompiledQuestion(compiledQuestionRef);
            }

            ServerModel.DB.Delete<TblQuestions>(questionId);
        }

        private static void deleteCompiledQuestion(int compiledQuestionId)
        {
            var compiledQuestion = (ServerModel.DB.Load<TblCompiledQuestions>(compiledQuestionId));

            var compiledQuestionsData = ServerModel.DB.LookupIds<TblCompiledQuestionsData>(compiledQuestion, null);

            foreach (var i in compiledQuestionsData)
            {
                deleteCompiledQuestionData(i);
            }

            ServerModel.DB.Delete<TblCompiledQuestions>(compiledQuestionId);
        }

        private static void deleteCompiledQuestionData(int compiledQuestionDataId)
        {
            ServerModel.DB.Delete<TblCompiledQuestionsData>(compiledQuestionDataId);
        }
    }
}
