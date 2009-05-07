using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class TestDetails : ControlledPage<TestDetailsController>
{
    protected override void BindController(TestDetailsController c)
    {
        base.BindController(c);
        Load += c.PageLoad;
        c.PageContent = (ContentPlaceHolder)Master.FindControl("MainContent");
        Bind(_maximumRankLabel, c.MaxPageRank, gn => string.Format("Maximal Posible Rank:{0}", gn));
        Bind(_pageRankLabel, c.PageRank, gn => string.Format("Page Rank:{0}", gn));
        Bind(_questionCountLabel, c.QuestionCount, gn => string.Format("Questions on Page:{0}", gn));
        BindTitle(c.PageTitle, pt => pt);
        Bind(_headerLabel, c.PageHeader);
    }
}
