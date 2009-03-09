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
        Bind(maximumRankLabel, c.MaxPageRank, gn => string.Format("Maximal Posible Rank:{0}", gn));
        Bind(pageRankLabel, c.PageRank, gn => string.Format("Page Rank:{0}", gn));
        Bind(questionCountLabel, c.QuestionCount, gn => string.Format("Questions on Page:{0}", gn));
        BindTitle(c.PageTitle, pt => pt);
        Bind(headerLabel, c.PageHeader);
    }
}
