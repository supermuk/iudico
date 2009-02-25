using System.Web.UI;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CourseEdit : ControlledPage<CourseEditController>
{
    protected override void BindController(CourseEditController c)
    {
        base.BindController(c);

        c.NameTextBox = TextBox_Name;
        c.DescriptionTextBox = TextBox_Description;
        c.CourseUpload = FileUpload_Course;
        c.NotifyLabel = Label_Notify;
        c.ImportButton = Button_Import;
        c.DeleteButton = Button_Delete;
        c.CourseTree = TreeView_Courses;

        Title = "Course Edit";

        Load += c.PageLoad;

        //Add postback trigger for file upload control
        UpdatePanelControlTrigger trigger = new PostBackTrigger();
        trigger.ControlID = Button_Import.UniqueID;
        UpdatePanel panel = (UpdatePanel)Master.FindControl("UpdatePanel1");
        panel.Triggers.Add(trigger);

    }
}