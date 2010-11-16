using System;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;

namespace NEWS
{
    public class AddNewsController : ControllerBase
    {
        public int AddNews(string content, string title, int catID)
        {
            return ServerModel.DB.Insert(new TblNews {CategoryRef = catID, Title = title, Contents = content});
        }
    }

    public partial class AddNews : ControlledPage<AddNewsController>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int cID;
            if (!int.TryParse(Request.Params["CategoryID"], out cID))
            {
                this.GoHome();
            }
            Category = ServerModel.DB.Load<TblCategory>(cID);
        }

        protected TblCategory Category;

        protected void Add_Click(object sender, EventArgs e)
        {
            int newsID = Controller.AddNews(ContentText.InnerText, TitleText.Text, Category.ID);
            this.GoToNews(newsID);
        }
    }
}
