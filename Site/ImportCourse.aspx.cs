using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class UploadCourse : ControlledPage<ImportCourseController>
{
    protected override void BindController(ImportCourseController c)
    {
        base.BindController(c);
       
        importButton.Click += c.importButton_Click;
        openButton.Click += c.openButton_Click;
        deleteButton.Click += c.deleteButton_Click;
        editButton.Click += c.editButton_Click;
        
        c.Name = nameTextBox;
        c.Description = descriptionTextBox;
        c.CourseUpload = courseUpload;
        c.CourseTree = courseTree;
        
    }
}