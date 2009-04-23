using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Teacher;

public partial class ThemePages : ControlledPage<ThemePagesController>
{
    protected override void BindController(ThemePagesController c)
    {
        base.BindController(c);
        Load += c.PageLoad;
        c.ThemePagesTable = pagesTable;
        BindTitle(c.ThemeName, cn => string.Format("Pages For Theme: {0}", cn));
        Bind(headerLabel, c.ThemeName, cn => string.Format("Pages For Theme: {0}", cn));
        descriptionLabel.Text = "You can see pages for theme, and go to pages correct answers";
    }
}
