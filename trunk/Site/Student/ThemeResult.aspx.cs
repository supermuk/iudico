using System;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class ThemeResult : ControlledPage<ThemeResultController>
{
    private const string themeId = "themeId";

    protected override void BindController(ThemeResultController c)
    {
        base.BindController(c);
        Load += PageLoad;
  
    }
    public void PageLoad(object sender, EventArgs e)
    {
        if (Request[themeId] != null)
        {
            themeResult.ThemeId = int.Parse(Request[themeId]);
        }
        else
        {
            throw new Exception("ThemeId is not specified");
        }
    }
}
