using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class UploadCourse : ControlledPage<UploadCourseController>
{
    protected override void BindController(UploadCourseController c)
    {
        base.BindController(c);
        submitButton.Click += c.submitButton_Click;
        c.Name = nameTextBox;
        c.Description = descriptionTextBox;
        c.FileUpload = CourseUpload;
    }
}