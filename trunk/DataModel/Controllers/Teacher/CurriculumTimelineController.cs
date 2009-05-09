using System;
using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumTimelineController : BaseTeacherController
    {
        public System.Web.UI.WebControls.TreeView CurriculumTree { get; set; }

        public IVariable<string> TimeLineData = "-1".AsVariable();

        [ControllerParameter]
        public int GroupID;
        [ControllerParameter]
        public int CurriculumID;
        [ControllerParameter]
        public string SelectedNode;

        private TblGroups group;
        private TblCurriculums curriculum;

        //private const string noNodeSelected = "Select node to modify.";
        private const string pageCaption = "Timeline for group: {0} and curriculum: {1}";
        private const string pageDescription = "This is detailed timeline for group: {0} and curriculum: {1}. You can manage group operations here.";
        private const string curriculumChar = "c";
        private const string stageChar = "s";

        public override void Loaded()
        {
            base.Loaded();

            group = ServerModel.DB.Load<TblGroups>(GroupID);
            curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumID);

            Caption.Value = pageCaption.
                                Replace("{0}", group.Name).
                                Replace("{1}", curriculum.Name);

            Description.Value = pageDescription.
                                    Replace("{0}", group.Name).
                                    Replace("{1}", curriculum.Name);
            Title.Value = Caption.Value;


            CurriculumTree.SelectedNodeChanged += new EventHandler(CurriculumTree_SelectedNodeChanged);

            if (!IsPostBack)
            {
                CurriculumTree.DataSource = GetCurriculum();
                CurriculumTree.ExpandAll();
                if (SelectedNode != null)
                {
                    CurriculumTree.FindNode(SelectedNode).Select();
                }
                else
                {
                    CurriculumTree.Nodes[0].Select();
                }
            }
            else
            {
                RedirectToController<CurriculumTimelineController>(new CurriculumTimelineController()
                {
                    BackUrl = RawUrl,
                    GroupID = group.ID,
                    CurriculumID = curriculum.ID,
                    SelectedNode = CurriculumTree.SelectedNode.ValuePath
                });
            }
            CurriculumTree_SelectedNodeChanged(CurriculumTree, EventArgs.Empty);
        }

        private void CurriculumTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            IdendtityNode selectedNode = CurriculumTree.SelectedNode as IdendtityNode;
            string encodedData = selectedNode.ValuePath + " " + GroupID.ToString() + " ";
            switch (selectedNode.Type)
            {
                case NodeType.Curriculum:
                    {
                        TimeLineData.Value = encodedData + selectedNode.ID + " " + curriculumChar;
                        break;
                    }
                case NodeType.Stage:
                    {
                        TimeLineData.Value = encodedData + selectedNode.ID + " " + stageChar;
                        break;
                    }
                case NodeType.Theme:
                    {
                        selectedNode.Parent.Select();
                        CurriculumTree_SelectedNodeChanged(CurriculumTree, EventArgs.Empty);
                        break;
                    }
            }
        }

        public IList<TblCurriculums> GetCurriculum()
        {
            List<TblCurriculums> result = new List<TblCurriculums>();
            result.Add(ServerModel.DB.Load<TblCurriculums>(CurriculumID));

            return result;
        }

    }


}
