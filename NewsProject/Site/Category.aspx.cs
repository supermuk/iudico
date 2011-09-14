using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.NewsProject.Model.DB;

namespace NEWS
{
    public class CategoryController : ControllerBase
    {
    }

    public partial class CategoryContent : ControlledPage<CategoryController>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int cID;
            if (!int.TryParse(Request.Params["ID"], out cID))
            {
                this.GoHome();
            }
            Category = ServerModel.DB.Load<TblCategory>(cID);
            if (!IsPostBack & !IsCallback)
            {
                CategoryTitle.Text = Category.Name;

                var ids = ServerModel.DB.LookupIds<TblNews>(Category);
                News.DataSource = new CachedBindingList<TblNews>(ServerModel.DB, ids);
                News.DataBind();
            }
        }

        protected TblCategory Category;

        protected void News_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var news = (TblNews) e.Row.DataItem;
                var newsTitle = (LinkButton) e.Row.FindControl("NewsTitle");
                newsTitle.Text = news.Title;
                newsTitle.PostBackUrl = Redirector.NewsLink(news.ID);
            }
        }

        protected void AddNews_Click(object sender, EventArgs e)
        {
            this.GoToAddNews(Category.ID);
        }
    }
}
