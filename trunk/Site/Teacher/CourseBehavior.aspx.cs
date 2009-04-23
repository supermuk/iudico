using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Teacher;

public partial class CourseBehavior : ControlledPage<CourseBehaviorController>
{
    protected override void BindController(CourseBehaviorController c)
    {
        base.BindController(c);
        Load += c.PageLoad;
        c.CourseBehaviorTable = courseBehavior;
        saveButton.Click += c.saveButton_Click;
        BindTitle(c.CourseName, cn => string.Format("Course Details For: {0}", cn));
        Bind(headerLabel, c.CourseName, cn => string.Format("Course Details For: {0}", cn));
        descriptionLabel.Text = "On this page you can change behavior of course themes";
    }

}
