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
    public class StudentPageController : ControllerBase
    {
        public TreeView CurriculumnTreeView { get; set; }

        public HttpResponse Response { get; set; }

        [PersistantField]
        private bool isTreeBuilded;

        private const string isCourseFlag = "course:";

        public void openTestButton_Click(object sender, EventArgs e)
        {
            string selectedNodeValue = CurriculumnTreeView.SelectedNode.Value;

            if (!selectedNodeValue.Contains(isCourseFlag))
            {
                int themaId = int.Parse(selectedNodeValue);

                string url = string.Format("OpenTest.aspx?openThema={0}", themaId);
                Response.Redirect(url);
            }
        }

        public void showResultButton_Click(object sender, EventArgs e)
        {
            string selectedNodeValue = CurriculumnTreeView.SelectedNode.Value;

            if (!selectedNodeValue.Contains(isCourseFlag))
            {
                int themaId = int.Parse(selectedNodeValue);

                string url = string.Format("ThemeResult.aspx?themeId={0}", themaId);
                Response.Redirect(url);
            }
        }

        public void page_Load(object sender, EventArgs e)
        {
            if (!isTreeBuilded)
            {
                BuildTree();
                isTreeBuilded = true;
            }
        }

        private void BuildTree()
        {
            IList<int> userCoursesIds = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.COURSE,
                                                                             ((CustomUser)Membership.GetUser()).ID, null, null);
                
            var userCourses = ServerModel.DB.Load<TblCourses>(userCoursesIds);
            foreach (var course in userCourses)
            {
                BuildCourse(course);
            }
        }

        private void BuildCourse(TblCourses course)
        {
            var node = new TreeNode(course.Name, isCourseFlag + course.ID);
            CurriculumnTreeView.Nodes.Add(node);
            BuildThemes(course, node);
        }

        private static void BuildThemes(TblCourses course, TreeNode node)
        {
            var themes = ServerModel.DB.Load<TblThemes>(ServerModel.DB.LookupIds<TblThemes>(course, null));

            foreach (var theme in themes)
            {
                node.ChildNodes.Add(new TreeNode(theme.Name, theme.ID.ToString()));
            }
            
        }
    }
}
