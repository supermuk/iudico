using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Controllers
{
    public class EditCourseController : ControllerBase
    {
        public TreeView CourseTreeView { get; set; }

        public HttpRequest Request { get; set; }

        [PersistantField]
        private int currentCourseId = -1;

        private const string courseIdRequestParameter = "courseId";


        public void moveUpButton_Click(object sender, EventArgs e)
        {
            
        }
        public void moveDownButton_Click(object sender, EventArgs e)
        {

        }
        public void renameButton_Click(object sender, EventArgs e)
        {
            
        }
        public void deleteButton_Click(object sender, EventArgs e)
        {

        }
        public void saveButton_Click(object sender, EventArgs e)
        {

        }
        public void pageLoad(object sender, EventArgs e)
        {
            if (Request[courseIdRequestParameter] != null)
            {
                int courseId;
                if (int.TryParse(Request[courseIdRequestParameter], out courseId))
                {
                    if(courseId != currentCourseId)
                        BuildCourseTree(courseId);
                }
            }
        }

        private void BuildCourseTree(int courseId)
        {
           // if(isUserHavePermission(courseId))
            {
                var course = ServerModel.DB.Load<TblCourses>(courseId);
                var rootNode = new TreeNode(course.Name, course.ID.ToString());
                CourseTreeView.Nodes.Add(rootNode);

                BuildThema(course, rootNode);
                currentCourseId = courseId;
            }
        }

        private static void BuildThema(TblCourses course, TreeNode rootNode)
        {
            var themas = ServerModel.DB.Load<TblThemes>(ServerModel.DB.LookupIds<TblThemes>(course, null));
            foreach (var i in themas)
            {
                var themaNode = new TreeNode(i.Name, i.ID.ToString());
                rootNode.ChildNodes.Add(themaNode);

                BuildPages(i, themaNode);
            }
        }

        private static void BuildPages(TblThemes theme, TreeNode themaNode)
        {
            var pages = ServerModel.DB.Load<TblPages>(ServerModel.DB.LookupIds<TblPages>(theme, null));
            
            foreach (var i in pages)
            {
                var pageNode = new TreeNode(i.PageName, i.ID.ToString());
                themaNode.ChildNodes.Add(pageNode);
            }
        }

        private static bool isUserHavePermission(int courseId)
        {
            IList<int> allUserCourses = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.COURSE,
                                                        ((CustomUser)Membership.GetUser()).ID, null, null);

            return allUserCourses.Contains(courseId);
        }
    }

    interface IEditCourseChange
    {
        void makeChange();
    }

    class DeletePage : IEditCourseChange
    {
        private int id;

        public DeletePage(int id)
        {
            this.id = id;
        }

        public void makeChange()
        {
            throw new System.NotImplementedException();
        }
    }

    class DeleteTheme : IEditCourseChange
    {
        private int id;

        public DeleteTheme(int id)
        {
            this.id = id;
        }

        public void makeChange()
        {
            throw new System.NotImplementedException();
        }
    }

    class MovePage : IEditCourseChange
    {
        private int pageId;

        private int themeId;

        public MovePage(int pageId, int themeId)
        {
            this.pageId = pageId;
            this.themeId = themeId;
        }

        public void makeChange()
        {
            throw new System.NotImplementedException();
        }
    }

    static class CourseCleaner
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
                if(file.PID != null)
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

            if(question.IsCompiled)
            {
                deleteCompiledQuestion((int) question.CompiledQuestionRef);
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
