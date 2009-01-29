using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CourseEdit : ControlledPage<EditCourseController>
{
    protected override void BindController(EditCourseController c)
    {
        base.BindController(c);

        LoadComplete += c.pageLoad;
        moveUp.Click += c.moveUpButton_Click;
        moveDown.Click += c.moveDownButton_Click;
        rename.Click += c.renameButton_Click;
        delete.Click += c.deleteButton_Click;

        c.Request = Request;

        c.RenameTextBox = renameTextBox;
        c.CourseTreeView = courseTreeView;
    }
}
