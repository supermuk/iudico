using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.Controllers.Student;
using System.Web.Security;
using IUDICO.DataModel.Security;


namespace IUDICO.DataModel.Controllers
{
    public class Admin_SearchPageSolrController : ControllerBase
    {
        public TblUsers User;
        public System.Web.UI.WebControls.Panel ResultsPanel;
        public System.Web.UI.WebControls.TextBox SearchQuery1;
        public System.Web.UI.WebControls.Button Open;
        public System.Web.UI.WebControls.TextBox ResultText;
        public List<System.Web.UI.WebControls.LinkButton> linkButton = new List<LinkButton>();
        
     public void openCourse(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).ID;

            if (id.ToString() != "")
            {
                TblLearnerAttempts la = new TblLearnerAttempts
                {
                    ThemeRef = Int32.Parse(id),
                    UserRef = ServerModel.User.Current.ID,
                    Started = DateTime.Now,
                };

                int LearnerAttemptId = ServerModel.DB.Insert<TblLearnerAttempts>(la);

                HttpContext.Current.Session["CurrentLearnerAttemptId"] = LearnerAttemptId;

                RedirectToController(new OpenTestController
                {
                    BackUrl = string.Empty,
                    PageIndex = 0
                });
            }

        }
    }
}
