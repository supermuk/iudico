using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class ImportCourse : ControlledPage<ImportCourseController>
{
    protected override void BindController(ImportCourseController c)
    {
        base.BindController(c);
       
        importButton.Click += c.importButton_Click;
        
        c.Name = nameTextBox;
        c.Description = descriptionTextBox;
        c.EditLink = editCourseLink;
        c.CourseUpload = courseUpload;
    }
}