using System.Web.UI;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CourseEdit : ControlledPage<CourseEditController>
{

    protected override void BindController(CourseEditController c)
    {
        base.BindController(c);

        Bind2Ways(TextBox_CourseName, c.CourseName);
        Bind2Ways(TextBox_CourseDescription, c.CourseDescription);
        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);
        Bind(Button_ImportCourse, c.ImportButton_Click);
        Bind(Button_DeleteCourse, c.DeleteButton_Click);
        Bind(Button_CourseBehaviour, c.CourseBehaviourButton_Click);
        BindEnabled(Button_DeleteCourse, c.DeleteButtonEnabled);

        c.CourseUpload = FileUpload_Course;
        c.CourseTree = TreeView_Courses;
        c.RawUrl = Request.RawUrl;
        
        //Add postback trigger for file upload control
        UpdatePanelControlTrigger trigger = new PostBackTrigger();
        trigger.ControlID = Button_ImportCourse.UniqueID;
        UpdatePanel panel = (UpdatePanel)Master.FindControl("UpdatePanel1");
        panel.Triggers.Add(trigger);

    }

    public override void DataBind()
    {
        base.DataBind();

        TreeView_Courses.DataSource = Controller.GetCourses();
    }
}