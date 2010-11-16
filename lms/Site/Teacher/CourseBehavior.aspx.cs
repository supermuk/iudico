using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Teacher;

public partial class CourseBehavior : ControlledPage<CourseBehaviorController>
{
    protected override void BindController(CourseBehaviorController c)
    {
        base.BindController(c);
        Load += c.PageLoad;
        c.CourseBehaviorTable = _courseBehavior;
        _saveButton.Click += c.SaveButtonClick;
        BindTitle(c.CourseName, cn => string.Format("Course Details For: {0}", cn));
        Bind(_headerLabel, c.CourseName, cn => string.Format("Course Details For: {0}", cn));
        Bind(_descriptionLabel, c.Description);
    }

}
