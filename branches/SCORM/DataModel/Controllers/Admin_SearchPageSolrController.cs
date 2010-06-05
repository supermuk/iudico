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
        
        int i = 0;

        public void Button1_Click(object sender, EventArgs e)
        {
            int UserID = 0;
            string result = "";
            string score = "";
            this.ResultsPanel.Controls.Clear();
            for (int k = 0; k < 100; k++)
            {
                linkButton[i].Visible = false;
            }
            try
            {
                if (this.SearchQuery1.Text.ToString() != "")
               {
                    HttpWebRequest request = WebRequest.Create("http://localhost:8080/apache-solr-1.4.0/select?q=" + this.SearchQuery1.Text + "&fl=*%2Cscore") as HttpWebRequest;
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

                                linkButton[i].Text = docName;
                                linkButton[i].ID = docId;
                                linkButton[i].Visible = true;
                                System.Web.UI.WebControls.Label labelInf = new System.Web.UI.WebControls.Label();
                                labelInf.Text = "            Description: " + name + " ; . . . . Curriculumn: " + curriculumn + " ; . . . . Score: " + score;
                                this.ResultsPanel.Controls.Add(linkButton[i]);
                                this.ResultsPanel.Controls.Add(labelInf);   
                                i++;
                            }
                        }
                        if (i ==0)
                        {
                            System.Web.UI.WebControls.Label noResult = new System.Web.UI.WebControls.Label();
                            noResult.Text = "No data found";
                            this.ResultsPanel.Controls.Add(noResult); 
                        }
                    }
                    ResultsPanel.Visible = true;
 
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


        public void openCourse(object sender, EventArgs e)
        {
            string id = ((HyperLink)sender).ID;

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
