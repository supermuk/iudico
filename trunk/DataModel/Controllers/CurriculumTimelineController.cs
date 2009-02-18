using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Security;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumTimelineController : ControllerBase
    {
        public TreeView CurriculumTree { get; set; }
        public Button GrantButton { get; set; }
        public Button RemoveButton { get; set; }
        public Label NotifyLabel { get; set; }
        public TextBox DateSinceTextBox { get; set; }
        public TextBox DateTillTextBox { get; set; }


        public DropDownList OperationList { get; set; }

        [ControllerParameter]
        public int GroupID;
        [ControllerParameter]
        public int CurriculumID;

        public void PageLoad(object sender, EventArgs e)
        {
            CurriculumTree.SelectedNodeChanged += new EventHandler(CurriculumTree_SelectedNodeChanged);
            if (!(sender as Page).IsPostBack)
            {
                TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumID);
                TblGroups group = ServerModel.DB.Load<TblGroups>(GroupID);
                NotifyLabel.Text = "Detailed timeline for curriculum: " + curriculum.Name + " for group: " + group.Name;
                fillCurriculumTree();
            }

            //GrantButton.Click += new EventHandler(GrantButton_Click);
        }

        void CurriculumTree_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        //private void GrantButton_Click(object sender, EventArgs e)
        //{
        //    DateTime since = DateTime.Parse(DateSinceTextBox.Text);
        //    DateTime till = DateTime.Parse(DateTillTextBox.Text);
        //    DateTimeInterval datetime = new DateTimeInterval(since, till);


        //    foreach (IdendtityNode node in CurriculumTree.CheckedNodes)
        //    {
        //        ISecuredDataObject dataObject = null;
        //        switch (node.Type)
        //        {
        //            case NodeType.Curriculum:
        //                {
        //                    dataObject = ServerModel.DB.Load<TblCurriculums>(node.ID);
        //                    break;
        //                }
        //            case NodeType.Stage:
        //                {
        //                    dataObject = ServerModel.DB.Load<TblStages>(node.ID);
        //                    break;
        //                }
        //            case NodeType.Theme:
        //                {
        //                    dataObject = ServerModel.DB.Load<TblThemes>(node.ID);

        //                    break;
        //                }
        //        }

        //        PermissionsManager.Grand(dataObject, FxCurriculumOperations.View, null, groupID, datetime);

        //    }
        //}

        private void fillCurriculumTree()
        {
            CurriculumTree.Nodes.Clear();
            TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumID);
            IdendtityNode curriculumNode = new IdendtityNode(curriculum);

            foreach (TblStages stage in TeacherHelper.StagesForCurriculum(curriculum))
            {
                IdendtityNode stageNode = new IdendtityNode(stage);
                foreach (TblThemes theme in TeacherHelper.ThemesForStage(stage))
                {
                    IdendtityNode themeNode = new IdendtityNode(theme);
                    stageNode.ChildNodes.Add(themeNode);
                }
                curriculumNode.ChildNodes.Add(stageNode);
            }
            CurriculumTree.Nodes.Add(curriculumNode);
            CurriculumTree.ExpandAll();
        }

    }


}
