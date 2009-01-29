using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;
using TreeNode=System.Web.UI.WebControls.TreeNode;
using TreeView=System.Web.UI.WebControls.TreeView;
using TextBox=System.Web.UI.WebControls.TextBox;
namespace IUDICO.DataModel.Controllers
{
    public class EditCourseController : ControllerBase
    {
        public TreeView CourseTreeView { get; set; }

        public HttpRequest Request { get; set; }

        public TextBox RenameTextBox { get; set; }

        private const string isCourseFlag = "course:";

        private const string isThemeFlag = "theme:";

        private const string courseIdRequestParameter = "courseId";

        [PersistantField]
        private bool isTreeBuilded;

        public void moveUpButton_Click(object sender, EventArgs e)
        {
            if(isSelectedNodePage())
            {
                int index = GetIndexOfParentTheme();
                if (index > 0)
                {
                    MovePage(--index);
                }
            }
        }

        private bool isSelectedNodePage()
        {
            return !CourseTreeView.SelectedNode.Value.Contains(isThemeFlag)
                   && !CourseTreeView.SelectedNode.Value.Contains(isCourseFlag);
        }

        private int GetIndexOfParentTheme()
        {
            return CourseTreeView.Nodes[0].ChildNodes.IndexOf(CourseTreeView.SelectedNode.Parent);
        }

        private void MovePage(int index)
        {
            var newThemeValue = CourseTreeView.Nodes[0].ChildNodes[index].Value;
            var newThemeId = newThemeValue.Replace(isThemeFlag, string.Empty);
            AssignPageToNewTheme(int.Parse(CourseTreeView.SelectedNode.Value), int.Parse(newThemeId));
            RebuildTree();
        }

        private void RebuildTree()
        {
            isTreeBuilded = false;
            RenameTextBox.Text = string.Empty;
            CourseTreeView.Nodes.Clear();
        }

        public void moveDownButton_Click(object sender, EventArgs e)
        {
            if (isSelectedNodePage())
            {
                int index = GetIndexOfParentTheme();
                if (index < CourseTreeView.Nodes[0].ChildNodes.Count)
                {
                    MovePage(++index);
                }
            }
        }
       
        public void renameButton_Click(object sender, EventArgs e)
        {
            if (!RenameTextBox.Visible && CourseTreeView.SelectedNode != null)
            {
                RenameTextBox.Visible = true;
            }
            else
            {
                if(!RenameTextBox.Text.Equals(string.Empty))
                {
                    if (CourseTreeView.SelectedNode != null)
                    {
                        var selectedNodeVal = CourseTreeView.SelectedNode.Value;

                        if(selectedNodeVal.Contains(isCourseFlag))
                        {
                            RenameCourse(selectedNodeVal, RenameTextBox.Text);
                        }
                        else if(selectedNodeVal.Contains(isThemeFlag))
                        {
                            RenameTheme(selectedNodeVal, RenameTextBox.Text);
                        }
                        else
                        {
                            RenamePage(selectedNodeVal, RenameTextBox.Text);
                        }
                        RebuildTree();
                    }
                }
                RenameTextBox.Visible = false;

            }
        }
        public void deleteButton_Click(object sender, EventArgs e)
        {

        }

        public void pageLoad(object sender, EventArgs e)
        {
            if (Request[courseIdRequestParameter] != null)
            {
                int courseId;
                if (int.TryParse(Request[courseIdRequestParameter], out courseId))
                {
                    if(!isTreeBuilded)
                        BuildCourseTree(courseId);
                }
            }
        }

        private void BuildCourseTree(int courseId)
        {
           // if(isUserHavePermission(courseId))
            {
                var course = ServerModel.DB.Load<TblCourses>(courseId);
                var rootNode = new TreeNode(course.Name, isCourseFlag + course.ID);
                CourseTreeView.Nodes.Add(rootNode);

                BuildThema(course, rootNode);
                isTreeBuilded = true;
                CourseTreeView.ExpandAll();
            }
        }

        private static void BuildThema(TblCourses course, TreeNode rootNode)
        {
            var themas = ServerModel.DB.Load<TblThemes>(ServerModel.DB.LookupIds<TblThemes>(course, null));
            foreach (var i in themas)
            {
                var themaNode = new TreeNode(i.Name, isThemeFlag + i.ID);
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

        private static void AssignPageToNewTheme(int pageId, int themeId)
        {
            var page = ServerModel.DB.Load<TblPages>(pageId);
            page.ThemeRef = themeId;
            ServerModel.DB.Update(page);
        }

        private static void RenameCourse(string courseIdVal,string newName)
        {
            var courseId = int.Parse(courseIdVal.Replace(isCourseFlag, string.Empty));

            var course = ServerModel.DB.Load<TblCourses>(courseId);
            course.Name = newName;
            ServerModel.DB.Update(course);
        }

        private static void RenameTheme(string themeIdVal, string newName)
        {
            var themeId = int.Parse(themeIdVal.Replace(isThemeFlag, string.Empty));

            var theme = ServerModel.DB.Load<TblThemes>(themeId);
            theme.Name = newName;
            ServerModel.DB.Update(theme);
        }

        private static void RenamePage(string pageIdVal, string newName)
        {
            var pageId = int.Parse(pageIdVal);

            var page = ServerModel.DB.Load<TblPages>(pageId);
            page.PageName = CheckExtension(newName);
            ServerModel.DB.Update(page);
        }

        private static string CheckExtension(string newName)
        {
            var ext = Path.GetExtension(newName);

            var aspxExt = ".aspx";

            return !ext.Equals(aspxExt) ? newName + aspxExt : newName;
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
