using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumEdit : ControlledPage<CurriculumEditController>
{
    protected override void BindController(CurriculumEditController c)
    {
        base.BindController(c);

        c.CourseTree = TreeView_Courses;
        c.CurriculumTree = TreeView_Curriculums;

        c.CreateCurriculumButton = Button_CreateCurriculum;
        c.CreateStageButton = Button_AddStage;
        c.AddThemeButton = Button_AddTheme;
        c.DeleteButton = Button_Delete;
        c.ModifyButton = Button_Modify;

        c.NameTextBox = TextBox_Name;
        c.DescriptionTextBox = TextBox_Description;

        c.NotifyLabel = Label_Notify;

        Load += c.PageLoad;   
    }




}
