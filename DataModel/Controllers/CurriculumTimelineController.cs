using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumTimelineController : ControllerBase
    {
        public TreeView CurriculumTree { get; set; }
        public Button GrantButton { get; set; }
        public TextBox DateSinceTextBox { get; set; }
        public TextBox DateTillTextBox { get; set; }
        public TextBox TimeSinceTextBox { get; set; }
        public TextBox TimeTillTextBox { get; set; }

        public DropDownList OperationList { get; set; }
        public DropDownList AssigmentList { get; set; }

        [ControllerParameter]
        public int GroupID;
        [ControllerParameter]
        public int CurriculumID;

        public void PageLoad(object sender, EventArgs e)
        {
            if (!(sender as Page).IsPostBack)
            {
                OperationList.Items.Add("View");
                OperationList.SelectedIndex = 0;

                fillAssigmentList();
            }

            //GrantButton.Click += new EventHandler(GrantButton_Click);
        }

        //private void GrantButton_Click(object sender, EventArgs e)
        //{
        //    DateTime since = DateTime.Parse(DateSinceTextBox.Text)
        //        .Add(TimeSpan.Parse(TimeSinceTextBox.Text));
        //    DateTime till = DateTime.Parse(DateTillTextBox.Text)
        //        .Add(TimeSpan.Parse(TimeTillTextBox.Text));
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

        //private void fillCurriculumTree()
        //{
        //    CurriculumTree.Nodes.Clear();

        //    TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(curriculumID);
        //    IdendtityNode curriculumNode = new IdendtityNode(curriculum);
        //    foreach (TblStages stage in ServerModel.DB.Query<TblStages>
        //        (new CompareCondition(
        //            DataObject.Schema.CurriculumRef,
        //            new ValueCondition<int>(curriculum.ID), COMPARE_KIND.EQUAL)))
        //    {
        //        IdendtityNode stageNode = new IdendtityNode(stage);
        //        foreach (TblThemes theme in ServerModel.DB.Query<TblThemes>
        //            (new InCondition(
        //                DataObject.Schema.ID,
        //                new SubSelectCondition<RelStagesThemes>("ThemeRef",
        //                   new CompareCondition(
        //                      DataObject.Schema.StageRef,
        //                      new ValueCondition<int>(stage.ID), COMPARE_KIND.EQUAL)))))
        //        {
        //            IdendtityNode themeNode = new IdendtityNode(theme);
        //            stageNode.ChildNodes.Add(themeNode);
        //        }
        //        curriculumNode.ChildNodes.Add(stageNode);
        //    }
        //    CurriculumTree.Nodes.Add(curriculumNode);

        //    CurriculumTree.ExpandAll();
        //}

        private void fillAssigmentList()
        {

        }

    }


}
