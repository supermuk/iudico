using System;
using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for CurriculumTimeline.aspx page
    /// </summary>
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
        private readonly string pageCaption = Translations.CurriculumTimelineController_pageCaption_Timeline_for_group___0__and_curriculum___1_;
        private readonly string pageDescription = Translations.CurriculumTimelineController_pageDescription_This_is_detailed_timeline_for_group___0__and_curriculum___1___You_can_manage_group_operations_here_;
        private readonly string curriculumChar = "c";
        private readonly string stageChar = "s";

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
