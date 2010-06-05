using System;
using System.Collections.Generic;
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

using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;
using IUDICO.DataModel.Controllers.Student;
using System.Web.Security;
using IUDICO.DataModel.Security;

public partial class Admin_SearchPageSolr : ControlledPage<Admin_SearchPageSolrController>
{
    protected override void BindController(Admin_SearchPageSolrController c)
    {
        List<System.Web.UI.WebControls.LinkButton> _linkButton = new List<LinkButton>();
        base.BindController(c);
        Button1.Click += c.Button1_Click;
        c.ResultsPanel = _resultsPanel;
        c.SearchQuery1 = _searchQuery;
        c.Open = _openResult;
        for (int i = 0; i < 100; i++)
        {
            LinkButton linkResult = new LinkButton();
            linkResult.Click += c.openCourse;
            linkResult.Visible = false;
            _linkButton.Add(linkResult);
        }
        c.linkButton = _linkButton;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _openResult.Visible = false;
        _resultsPanel.Visible = false;
    }


   
}
