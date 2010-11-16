using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumEdit : ControlledPage<CurriculumEditController>
{
    protected override void BindController(CurriculumEditController c)
    {
        base.BindController(c);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);

        Bind(Button_CreateCurriculum, c.CreateCurriculumButton_Click);
        Bind(Button_AddStage, c.AddStageButton_Click);
        Bind(Button_AddTheme, c.AddThemeButton_Click);
        Bind(Button_Delete, c.DeleteButton_Click);
        Bind(Button_Modify, c.ModifyButton_Click);

        BindEnabled(Button_AddStage, c.AddStageButtonEnabled);
        BindEnabled(Button_AddTheme, c.AddThemeButtonEnabled);
        BindEnabled(Button_Delete, c.DeleteButtonEnabled);
        BindEnabled(Button_Modify, c.ModifyButtonEnabled);

        Bind2Ways(TextBox_Name, c.ObjectName);
        Bind2Ways(TextBox_Description, c.ObjectDescription);

        c.CourseTree = TreeView_Courses;
        c.CurriculumTree = TreeView_Curriculums;
        c.RawUrl = Request.RawUrl;

        
    }

    public override void DataBind()
    {
        base.DataBind();

        TreeView_Courses.DataSource = Controller.GetCourses();
        TreeView_Curriculums.DataSource = Controller.GetCurriculums();
    }
}
