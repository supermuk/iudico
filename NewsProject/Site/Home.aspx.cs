using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.NewsProject.Model.DB;

namespace NEWS
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriesGrid.DataSource = ServerModel.DB.FullCached<TblCategory>();
            CategoriesGrid.DataBind();   
        }

        protected void CategoryGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var cat = (TblCategory) e.Row.DataItem;
                var nl = (LinkButton) e.Row.FindControl("NameLink");
                nl.Text = cat.Name;
                nl.ToolTip = cat.Description;
                nl.PostBackUrl = Redirector.CategoryLink(cat.ID);
            }
        }
    }
}
