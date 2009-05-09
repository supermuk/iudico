using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class CurriculumEditController : BaseTeacherController
    {
        [PersistantField]
        public IVariable<string> ObjectName = string.Empty.AsVariable();
        [PersistantField]
        public IVariable<string> ObjectDescription = string.Empty.AsVariable();

        [PersistantField]
        public IVariable<bool> DeleteButtonEnabled = false.AsVariable();
        [PersistantField]
        public IVariable<bool> ModifyButtonEnabled = false.AsVariable();
        [PersistantField]
        public IVariable<bool> AddStageButtonEnabled = false.AsVariable();
        [PersistantField]
        public IVariable<bool> AddThemeButtonEnabled = false.AsVariable();

        public TreeView CourseTree { get; set; }
        public TreeView CurriculumTree { get; set; }

        //"magic words"
        private const string pageCaption = "Curriculum management.";
        private const string pageDescription = "This is curriculum edit page page. Create/Edit/Delete your curriculum here.";
        private const string noCourses = "You have no courses, upload some first.";
        private const string noThemesSelected = "Select some themes to add.";
        private const string alreadyHaveTheme = "Stage: {0} already contains theme(s) :";

        public override void Loaded()
        {
            base.Loaded();

            Caption.Value = pageCaption;
            Description.Value = pageDescription;
            Title.Value = Caption.Value;
            Message.Value = string.Empty;

            CurriculumTree.SelectedNodeChanged += new EventHandler(CurriculumTree_SelectedNodeChanged);
        }

        private void CurriculumTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            IdendtityNode selectedNode = CurriculumTree.SelectedNode as IdendtityNode;
            DeleteButtonEnabled.Value = true;
            switch (selectedNode.Type)
            {
                case NodeType.Curriculum:
                    {
                        AddStageButtonEnabled.Value = true;
                        ModifyButtonEnabled.Value = true;
                        AddThemeButtonEnabled.Value = false;
                        break;
                    }
                case NodeType.Stage:
                    {
                        AddStageButtonEnabled.Value = false;
                        ModifyButtonEnabled.Value = true;
                        AddThemeButtonEnabled.Value = true;
                        break;
                    }
                case NodeType.Theme:
                    {
                        AddStageButtonEnabled.Value = false;
                        ModifyButtonEnabled.Value = false;
                        AddThemeButtonEnabled.Value = false;
                        break;
                    }
            }
        }

        public void ModifyButton_Click()
        {
            //modifying selected node
            IdendtityNode modifiedNode = CurriculumTree.SelectedNode as IdendtityNode;
            if (modifiedNode.Type == NodeType.Stage)
            {
                TblStages stage = ServerModel.DB.Load<TblStages>(modifiedNode.ID);
                stage.Name = ObjectName.Value;
                stage.Description = ObjectDescription.Value;
                ServerModel.DB.Update<TblStages>(stage);
            }
            if (modifiedNode.Type == NodeType.Curriculum)
            {
                TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(modifiedNode.ID);
                curriculum.Name = ObjectName.Value;
                curriculum.Description = ObjectDescription.Value;
                ServerModel.DB.Update<TblCurriculums>(curriculum);
            }

            modifiedNode.Text = ObjectName.Value;
            modifiedNode.ToolTip = ObjectDescription.Value;
        }

        public void AddStageButton_Click()
        {
            //adding new stage
            IdendtityNode curriculumNode = CurriculumTree.SelectedNode as IdendtityNode;

            //Create new stage
            TblStages stage = new TblStages();
            stage.Name = ObjectName.Value;
            stage.Description = ObjectDescription.Value;
            stage.CurriculumRef = curriculumNode.ID;
            ServerModel.DB.Insert<TblStages>(stage);

            //Update curriculum tree
            curriculumNode.ChildNodes.Add(new IdendtityNode(stage));
        }

        public void CreateCurriculumButton_Click()
        {
            //create new curriculum
            TblCurriculums curriculum = new TblCurriculums();
            curriculum.Name = ObjectName.Value;
            curriculum.Description = ObjectDescription.Value;
            curriculum.ID = ServerModel.DB.Insert<TblCurriculums>(curriculum);

            //grant permissions for this curriculum
            PermissionsManager.Grand(curriculum, FxCurriculumOperations.Use, ServerModel.User.Current.ID, null, DateTimeInterval.Full);
            PermissionsManager.Grand(curriculum, FxCurriculumOperations.Modify, ServerModel.User.Current.ID, null, DateTimeInterval.Full);

            //Update curriculum tree
            CurriculumTree.Nodes.Add(new IdendtityNode(curriculum));
        }

        public void AddThemeButton_Click()
        {
            if (CourseTree.CheckedNodes.Count == 0)
            {
                Message.Value = noThemesSelected;
                return;
            }

            IdendtityNode stageNode = CurriculumTree.SelectedNode as IdendtityNode;
            Message.Value = alreadyHaveTheme.Replace("{0}", stageNode.Text);
            bool alreadyHaveSomeTheme = false;

            for (int i = 0; i < CourseTree.CheckedNodes.Count; i++)
            {
                IdendtityNode themeNode = CourseTree.CheckedNodes[i] as IdendtityNode;


                if (TeacherHelper.StageContainsTheme(stageNode.ID, themeNode.ID))
                {
                    Message.Value += themeNode.Text + ", ";
                    alreadyHaveSomeTheme = true;
                }
                else
                {
                    ServerModel.DB.Link(
                        ServerModel.DB.Load<TblStages>(stageNode.ID),
                        ServerModel.DB.Load<TblThemes>(themeNode.ID));
                    stageNode.ChildNodes.Add(new IdendtityNode(ServerModel.DB.Load<TblThemes>(themeNode.ID)));
                }

                CourseTree.CheckedNodes[i].Checked = false;
                i--;
            }

            if (alreadyHaveSomeTheme)
            {
                Message.Value = Message.Value.Remove(Message.Value.Length - 2) + ".";
            }
            else
            {
                Message.Value = string.Empty;
            }
        }

        public void DeleteButton_Click()
        {
            IdendtityNode deletedNode = CurriculumTree.SelectedNode as IdendtityNode;
            int themeID = -1;
            int stageID = -1;
            int curriculumID = -1;
            switch (deletedNode.Type)
            {
                case NodeType.Theme:
                    {
                        themeID = deletedNode.ID;
                        stageID = (deletedNode.Parent as IdendtityNode).ID;
                        curriculumID = (deletedNode.Parent.Parent as IdendtityNode).ID;
                        break;
                    }
                case NodeType.Stage:
                    {
                        stageID = deletedNode.ID;
                        curriculumID = (deletedNode.Parent as IdendtityNode).ID;
                        break;
                    }
                case NodeType.Curriculum:
                    {
                        curriculumID = deletedNode.ID;
                        break;
                    }
            }

            Redirect(ServerModel.Forms.BuildRedirectUrl<CurriculumDeleteConfirmationController>(
                 new CurriculumDeleteConfirmationController
                 {
                     BackUrl = RawUrl,
                     CurriculumID = curriculumID,
                     StageID = stageID,
                     ThemeID = themeID
                 }));

        }

        public IList<TblCourses> GetCourses()
        {
            IList<TblCourses> result = TeacherHelper.CurrentUserCourses(FxCourseOperations.Use);
            if (result.Count == 0)
            {
                Message.Value = noCourses;
            }
            return result;
        }

        public IList<TblCurriculums> GetCurriculums()
        {
            return TeacherHelper.CurrentUserCurriculums(FxCurriculumOperations.Modify);
        }
    }
}
