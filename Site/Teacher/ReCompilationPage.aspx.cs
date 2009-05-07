using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Teacher;

public partial class Teacher_ReCompilationPage : ControlledPage<ReCompilePageController>
{
    protected override void BindController(ReCompilePageController c)
    {
        base.BindController(c);
        Load += c.PageLoad;

        c.GroupDropDownList = _groupDropDownList;
        c.CurriculumnDropDownList = _curriculumnDropDownList;
        c.StageDropDownList = _stageDropDownList;
        c.ThemeDropDownList = _themeDropDownList;

        _groupDropDownList.SelectedIndexChanged += c.GroupDropDownListSelectedIndexChanged;
        _curriculumnDropDownList.SelectedIndexChanged += c.CurriculumnDropDownListSelectedIndexChanged;
        _stageDropDownList.SelectedIndexChanged += c.StageDropDownListSelectedIndexChanged;

        _reCompileButton.Click += c.ReCompileButtonClick;

        _headerLabel.Text = "ReCompile Page";
        _descriptionLabel.Text = "On this page you can recompile group results";
    }
}
