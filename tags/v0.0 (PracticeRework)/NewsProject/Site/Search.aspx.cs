using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;

namespace NEWS
{
    public partial class Search : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void News_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var news = (TblNews) e.Row.DataItem;

                var nl = (LinkButton) e.Row.FindControl("NewsTitle");
                nl.Text = news.Title;
                nl.PostBackUrl = Redirector.NewsLink(news.ID);

                var cat = ServerModel.DB.Load<TblCategory>(news.CategoryRef);
                var cl = (LinkButton)e.Row.FindControl("CategoryTitle");
                cl.Text = cat.Name;
                cl.PostBackUrl = Redirector.CategoryLink(cat.ID);
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            IDBCondition c1 = null;
            IDBCondition c2 = null;
            if (TitleFilter.Text.IsNotNull())
            {
                c1 = new CompareCondition(
                    new PropertyCondition("Title"),
                    new ValueCondition("%" + TitleFilter.Text + "%"),
                    COMPARE_KIND.LIKE);
            }
            if (ContentFilter.Text.IsNotNull())
            {
                c2 = new CompareCondition(
                    new PropertyCondition("Contents"),
                    new ValueCondition("%" + ContentFilter.Text + "%"),
                    COMPARE_KIND.LIKE);
            }
            if (c1 == null && c2 != null)
            {
                c1 = c2;
                c2 = null;
            }
            IDBCondition sc = c2 != null ? new AndCondtion(c1, c2) : c1;

            var news = ServerModel.DB.Query<TblNews>(sc);
            NewsSearchGrid.DataSource = news;
            NewsSearchGrid.DataBind();
            NewsSearchGrid.Visible = true;
        }
    }
}
