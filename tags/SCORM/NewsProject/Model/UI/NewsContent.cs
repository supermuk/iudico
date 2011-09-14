using System.Web.UI;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;

namespace LEX.NewsProject.Model.UI
{
    public class NewsContent : Control
    {
        public int NewsID
        {
            get
            {
                return (int) ViewState["NewsID"];
            }
            set
            {
                ViewState["NewsID"] = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            var news = ServerModel.DB.Load<TblNews>(NewsID);
            writer.Write(news.Contents);
        }
    }
}