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
    static LinkButton[] linkButtonsArray = new LinkButton[20];
    static int btn_count;
    Admin_SearchPageSolrController cRef;
    int i = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        _resultsPanel.Controls.Clear();
        _resultsPanel.Visible = false;
        _openResult.Visible = false;
        if (linkButtonsArray[0] is LinkButton)
        {
            foreach (LinkButton linkButton in linkButtonsArray)
            {
                if (linkButton != null)
                {
                    addLinkButton(linkButton);
                    linkButton.Click += new System.EventHandler(this.LinkButton_Click);
                }
            }
        }
    }

    //Search on _searchQuery textBox value
    protected void Button1_Click1(object sender, EventArgs e)
    {
        int UserID = 0;
        string result = "";
        string score = "";

        _resultsPanel.Controls.Clear();
        try
        {
            if (_searchQuery.Text.ToString() != "")
            {
                HttpWebRequest request = WebRequest.Create("http://localhost:8080/apache-solr-1.4.0/select?q=" + _searchQuery.Text + "&fl=*%2Cscore") as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream  
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    // Console application output
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(reader.ReadToEnd());
                    XmlNodeList nodes = xmlDoc.SelectNodes("/response/result/doc");
                    foreach (XmlNode node in nodes)
                    {
                        result = "";
                        XmlNodeList scoreNode = node.SelectNodes("float");
                        XmlNodeList docNodes = node.SelectNodes("str");
                        string docName = "";
                        string docId = "";
                        foreach (XmlNode floatNode in scoreNode)
                        {
                            if (floatNode.Attributes["name"].Value == "score")
                            {
                                score = floatNode.InnerText;
                            }
                        }
                        foreach (XmlNode strNode in docNodes)
                        {
                            if (strNode.Attributes["name"].Value == "name")
                            {
                                docName = strNode.InnerText;
                            }
                            if (strNode.Attributes["name"].Value == "id")
                            {
                                docId = strNode.InnerText;
                            }
                        }

                        List<int> usersCurriculums = new List<int>();
                        UserID = ((CustomUser)Membership.GetUser()).ID;
                        var currentUser = ServerModel.DB.Load<TblUsers>(UserID);
                        var groups = ServerModel.DB.Load<TblGroups>(ServerModel.DB.LookupMany2ManyIds<TblGroups>(currentUser, null));
                        List<int> listGroups = new List<int>();
                        foreach (TblGroups g in groups)
                        {
                            listGroups.Clear();
                            listGroups.Add(g.ID);
                            var currGroups = ServerModel.DB.Load<TblPermissions>("OwnerGroupRef", listGroups);
                            foreach (TblPermissions curG in currGroups)
                            {
                                if (curG.CurriculumRef != null)
                                {
                                    usersCurriculums.Add(Int32.Parse(curG.CurriculumRef.ToString()));
                                }
                            }
                        }
                        bool isStudent = false;
                        var role = ServerModel.DB.Load<FxRoles>(ServerModel.DB.LookupMany2ManyIds<FxRoles>(currentUser, null));
                        foreach (FxRoles r in role)
                        {
                            if (r.ID == 1) isStudent = true;
                            if ((isStudent == true) && (r.ID != 1)) isStudent = false;
                        }
                        var stages = ServerModel.DB.Load<TblCourses>(Int32.Parse(docId));
                        string name = stages.Description;
                        string curriculumn = "";
                        bool relevant = false;
                        List<int> docList = new List<int>();
                        docList.Add(Int32.Parse(docId));
                        var stagesT = ServerModel.DB.Load<TblThemes>("CourseRef", docList);
                        foreach (TblThemes st in stagesT)
                        {
                            var stagesS = ServerModel.DB.Load<TblStages>(st.StageRef);
                            var stagesC = ServerModel.DB.Load<TblCurriculums>(stagesS.CurriculumRef);
                            curriculumn = stagesC.Name;
                            if (isStudent == false) relevant = true;
                            else
                                if (usersCurriculums.Contains(stagesC.ID))
                                    relevant = true;
                        }
                        if (relevant == true)
                        {
                            if (name != "")
                            {
                                result = (i + 1).ToString() + ". " + docName + " ; . . . . Description: " + name + " ; . . . . Curriculumn: " + curriculumn + " ; . . . . Score: " + score;
                            }
                            else
                            {
                                result = (i + 1).ToString() + ". " + docName + " ; . . . . Curriculumn: " + curriculumn + " ; . . . . Score: " + score;
                            }

                            initLinkButton(result, docId);
                            i++;
                        }
                    }
                    if (i == 0) initLinkButton("No data found", "0");
                    _resultsPanel.Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            try
            {
                if (ex.Message.ToString() == "Unable to connect to the remote server")
                {
                    System.Diagnostics.Process procTomcat = new System.Diagnostics.Process();
                    procTomcat.EnableRaisingEvents = false;
                    procTomcat.StartInfo.FileName = Path.Combine(System.Environment.CurrentDirectory, "Site\\tomcat-solr\\tomcatStart.bat");
                    procTomcat.Start();
                }
            }
            catch (Exception ex1) { }
        }
    }
    

    protected void initLinkButton(string docName, string docId)
    {
        LinkButton linkButton = new LinkButton();
        linkButton.Text = docName;
        linkButton.ID = docId;
        linkButton.Visible = true;
        linkButtonsArray[btn_count++] = linkButton;
        linkButton.Click += new System.EventHandler(LinkButton_Click);
        addLinkButton(linkButton);
    }

    protected void addLinkButton(LinkButton linkButton)
    {
        _resultsPanel.Controls.Add(linkButton);
        _resultsPanel.Controls.Add(new LiteralControl("<br />"));
    }

    protected override void BindController(Admin_SearchPageSolrController c)
    {
        base.BindController(c);
        cRef = c;        
    }

    protected void LinkButton_Click(object sender, EventArgs e)
    {
        cRef.openCourse(sender,e);
    }
}
