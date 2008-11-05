using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class UploadCourse : ControlledPage<UploadCourseController>
{
    protected override void BindController(UploadCourseController c)
    {
        base.BindController(c);
        submitButton.Click += c.submitButton_Click;
        c.Name = nameTextBox.Text;
        c.Description = descriptionTextBox.Text;
        c.FileUpload = CourseUpload;
    }
}