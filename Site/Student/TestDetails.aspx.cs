using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class TestDetails : ControlledPage<TestDetailsController>
{
    protected override void BindController(TestDetailsController c)
    {
        base.BindController(c);
        Load += c.PageLoad;
        c.Request = Request;
        c.PageContent = (ContentPlaceHolder)Master.FindControl("MainContent");
        c.MaxPageRank = maximumRank;
        c.PageRank = pageRank;
        c.QuestionCount = questionCount;
    }
}
