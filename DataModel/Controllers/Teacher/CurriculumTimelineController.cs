using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Security;
using IUDICO.DataModel.DB.Base;
using EO.Web;
using LEX.CONTROLS;
using System.Collections.Generic;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumTimelineController : BaseTeacherController
    {
        public System.Web.UI.WebControls.TreeView CurriculumTree { get; set; }

        public IVariable<string> PermissionID = "-1".AsVariable();

        [ControllerParameter]
        public int GroupID;
        [ControllerParameter]
        public int CurriculumID;

        private TblGroups group;
        private TblCurriculums curriculum;

        private const string noNodeSelected = "Select node to modify.";
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
                CurriculumTree.Nodes[0].Select();
            }
            if (CurriculumTree.SelectedNode != null)
            {
                CurriculumTree_SelectedNodeChanged(this, EventArgs.Empty);
            }
        }

        private void CurriculumTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            IdendtityNode selectedNode = CurriculumTree.SelectedNode as IdendtityNode;
            switch (selectedNode.Type)
            {
                case NodeType.Curriculum:
                    {
                        string encodedData = GroupID.ToString() + " " + CurriculumID + " " + curriculumChar;
                        PermissionID.Value = encodedData;
                        break;
                    }
                case NodeType.Stage:
                    {
                        TblStages stage = ServerModel.DB.Load<TblStages>(selectedNode.ID);
                        string encodedData = GroupID.ToString() + " " + stage.ID + " " + stageChar;
                        PermissionID.Value = TeacherHelper.GroupPermissionsForStage(group,stage )[0].ID.ToString();
                        break;
                    }
                case NodeType.Theme:
                    {
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
