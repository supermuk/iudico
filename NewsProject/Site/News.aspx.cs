using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.NewsProject.Model.DB;
using NEWS;

public class NewsController : ControllerBase
{
    public void LeaveComment(string text, int newsID)
    {
        ServerModel.DB.Insert(new TblComment { Content = text, NewsRef = newsID });
    }
}

public partial class NewsPage : ControlledPage<NewsController>
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id;
        if (!int.TryParse(Request.Params["ID"], out id))
        {
            this.GoHome();
        }

        News = ServerModel.DB.Load<TblNews>(id);
        if (!IsPostBack && !IsCallback)
        {
            NewsContent.NewsID = id;
            ReBindGrid();

            NewsTitle.Text = News.Title;
        }
    }

    private void ReBindGrid()
    {
        var cmIds = ServerModel.DB.LookupIds<TblComment>(News);
        CommentsGrid.DataSource = new CachedBindingList<TblComment>(ServerModel.DB, cmIds);
        CommentsGrid.DataBind();
    }

    protected TblNews News;

    protected void Comments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var cm = (TblComment)e.Row.DataItem;
            var ct = (Label)e.Row.FindControl("CommentText");
            ct.Text = cm.Content;
        }
    }

    protected void LeaveComment_Click(object sender, EventArgs e)
    {
        Controller.LeaveComment(Comment.InnerText, News.ID);
        ReBindGrid();
    }
}

