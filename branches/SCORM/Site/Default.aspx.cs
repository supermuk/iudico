using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;


public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string searchPath = @"C:\Documents and Settings\iryna.martyniv\Desktop\IUDICO_Checking\Site\Assets";
        string[] dirs = Directory.GetDirectories(searchPath, "*");
        List<int> ids = new List<int>();

        foreach (string dir in dirs)
        {
            int directoryId;
            if (int.TryParse(new DirectoryInfo(dir).Name, out directoryId))
            {
                ids.Add(Convert.ToInt32(new DirectoryInfo(dir).Name));
                //MessageBox.Show(dir.ToString());
            }
        }

        var stages = ServerModel.DB.Load<TblResources>("CourseRef", ids);

        string[] filePaths = Directory.GetFiles(@"C:\apps\tomcat-solr\apache-solr-1.4.0\Iudico", "*.xml");
        foreach (string filePath in filePaths)
            File.Delete(filePath);


        string filename = "";
        int i = 0;
        XmlTextWriter writer;

        try
        {
            foreach (TblResources res in stages)
            {
                i++;

                filename = "XML" + i.ToString() + DateTime.Now.TimeOfDay.Hours.ToString() + DateTime.Now.TimeOfDay.Minutes.ToString() + DateTime.Now.TimeOfDay.Seconds.ToString() + ".xml";

                writer = new XmlTextWriter("C:\\apps\\tomcat-solr\\apache-solr-1.4.0\\Iudico\\" + filename, null);
                writer.WriteStartElement("add");
                writer.WriteStartElement("doc");
                writer.WriteStartElement("field");
                writer.WriteStartAttribute("name");
                writer.WriteString("id");
                writer.WriteEndAttribute();
                writer.WriteString(res.CourseRef.ToString());
                writer.WriteEndElement();

                var stages2 = ServerModel.DB.Load<TblCourses>(res.CourseRef);

                string name = stages2.Name;

                writer.WriteStartElement("field");
                writer.WriteStartAttribute("name");
                writer.WriteString("name");
                writer.WriteEndAttribute();
                writer.WriteString(name);
                writer.WriteEndElement();

                FileStream file = new FileStream("C:\\Documents and Settings\\iryna.martyniv\\Desktop\\IUDICO_Checking\\Site\\Assets\\" + res.CourseRef.ToString() + "\\" + res.Href.ToString(), FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(file);
                string s = sr.ReadToEnd();
                sr.Close();
                file.Close();

                writer.WriteStartElement("field");
                writer.WriteStartAttribute("name");
                writer.WriteString("content");
                writer.WriteEndAttribute();
                writer.WriteCData(s);

                writer.WriteEndElement();

                writer.Flush();
                writer.Close();
                Response.Write(res.Href + "<br>");



            }

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "C:\\apps\\tomcat-solr\\apache-solr-1.4.0\\delete.bat";
            proc.Start();

            System.Diagnostics.Process proc2 = new System.Diagnostics.Process();
            proc2.EnableRaisingEvents = false;
            proc2.StartInfo.FileName = "C:\\apps\\tomcat-solr\\apache-solr-1.4.0\\index.bat";
            proc2.Start();

        }
        catch (Exception ex)
        {

            Response.Write("<br>");
        }


        
    }
    
    
}
