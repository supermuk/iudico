using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel;

public partial class CurriculumEdit : ControlledPage<CurriculumEditController>
{
    protected override void BindController(CurriculumEditController c)
    {
        base.BindController(c);

        #region Controls binding
        c.CourseTree = TreeView_Courses;
        c.CurriculumTree = TreeView_Curriculums;

        c.CreateCurriculumButton = Button_CreateCurriculum;
        c.CreateStageButton = Button_AddStage;
        c.AddThemeButton = Button_AddTheme;
        c.DeleteButton = Button_Delete;
        c.ModifyButton = Button_Modify;

        c.NameTextBox = TextBox_Name;
        c.DescriptionTextBox = TextBox_Description;
        #endregion

        Load += c.PageLoad;   
    }




}
