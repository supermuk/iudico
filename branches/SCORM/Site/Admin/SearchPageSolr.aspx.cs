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

public partial class Admin_SearchPageSolr : ControlledPage<Admin_SearchPageSolrController>
{
    protected override void BindController(Admin_SearchPageSolrController c)
    {
        base.BindController(c);
        Button1.Click += c.Button1_Click;
        _openResult.Click += c.Button2_Click;
        c.ResultsListBox = _resultsListBox;
        c.SearchQuery1 = _searchQuery;
        c.Open = _openResult;



    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _openResult.Visible = false;
        _resultsListBox.Visible = false;
    }
}
