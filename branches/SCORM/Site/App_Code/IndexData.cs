using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Mail;
using System.Web.Caching;
using System.Web.SessionState;
using System.IO;
using System.Messaging;
using System.Windows.Forms;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;
using System.Xml;
using IUDICO.DataModel.ImportManagers;
using mshtml;


/// <summary>
/// Summary description for IndexData
/// </summary>
public class IndexData
{
    public IndexData()
    {
        Index();
    }

    public void Index()
    {
       
            //SEARCHING FOR DIRECTORIES IN ASSETS DIRECTORY, WHICH ARE THEMES
            //string searchPath = Path.Combine(HttpRuntime.AppDomainAppVirtualPath, "Assets");
            string searchPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Assets");
            string[] dirs = Directory.GetDirectories(searchPath, "*");
            List<int> ids = new List<int>();

            foreach (string dir in dirs)
            {
                int directoryId;
                if (int.TryParse(new DirectoryInfo(dir).Name, out directoryId))
                {
                    ids.Add(Convert.ToInt32(new DirectoryInfo(dir).Name));
                }
            }

            var stages = ServerModel.DB.Load<TblResources>("CourseRef", ids);
            string xmlindex = Path.Combine(HttpRuntime.AppDomainAppPath, "tomcat-solr\\apache-solr-1.4.0\\Iudico\\");

        try
        {            
            //DELETING PREVIOUS CREATING XMLs
            string[] filePaths = Directory.GetFiles(xmlindex, "*.xml");
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }


            //DELETING SOLR INDEX
            HttpWebRequest request = WebRequest.Create("http://localhost:8080/apache-solr-1.4.0/update?stream.body=%3Cdelete%3E%3Cquery%3Ename:*%3C/query%3E%3C/delete%3E") as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            { }

            HttpWebRequest requestCommit = WebRequest.Create("http://localhost:8080/apache-solr-1.4.0/update?stream.body=%3Ccommit/%3E") as HttpWebRequest;
            using (HttpWebResponse response = requestCommit.GetResponse() as HttpWebResponse)
            { }

        }
        catch (Exception ex)
        {
            if (ex.Message.ToString() == "Unable to connect to the remote server")
            {
                System.Diagnostics.Process procTomcat = new System.Diagnostics.Process();
                procTomcat.EnableRaisingEvents = false;
                procTomcat.StartInfo.FileName = Path.Combine(HttpRuntime.AppDomainAppPath, "tomcat-solr\\tomcatStart.bat");
                procTomcat.Start();
            }
        }

        //CREATING NEW INDEX
        string filename = "";
        int i = 0;
        XmlTextWriter writer;

        try
        {
            foreach (TblResources res in stages)
            {
                i++;

                filename = "XML" + i.ToString() + DateTime.Now.TimeOfDay.Hours.ToString() + DateTime.Now.TimeOfDay.Minutes.ToString() + DateTime.Now.TimeOfDay.Seconds.ToString() + ".xml";

                //CREATING XML WITH ID, NAME AND CONTENT OF THEME
                writer = new XmlTextWriter(xmlindex + filename, null);
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

                string filePath = Path.Combine(CourseManager.GetCoursePath(res.CourseRef), res.Href.ToString());
                FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(file);
                string s = sr.ReadToEnd();
                sr.Close();
                file.Close();

                IHTMLDocument2 doc = new HTMLDocumentClass();
                doc.write(new object[] { s });
                doc.close();

                writer.WriteStartElement("field");
                writer.WriteStartAttribute("name");
                writer.WriteString("content");
                writer.WriteEndAttribute();
                writer.WriteString(doc.body.innerText);
                writer.WriteEndElement();

                writer.Flush();
                writer.Close();
                //Response.Write(res.Href + "<br>");


                //INDEXING OF XML BY METHOD POST VIA HTTP
                Encoding xmlEncoding = Encoding.UTF8;

                string filePathXml = xmlindex + filename;
                FileStream fileXML = new FileStream(filePathXml, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr1 = new StreamReader(fileXML);
                string ss = sr1.ReadToEnd();
                sr1.Close();
                fileXML.Close();

                HttpWebRequest requestIndex = WebRequest.Create("http://localhost:8080/apache-solr-1.4.0/update") as HttpWebRequest;
                requestIndex.Method = "POST";
                requestIndex.ContentType = "text/xml; charset=utf-8";
                requestIndex.ProtocolVersion = HttpVersion.Version10;
                requestIndex.KeepAlive = false;

                byte[] data = xmlEncoding.GetBytes(ss);
                requestIndex.ContentLength = ss.Length;

                using (var postParams = requestIndex.GetRequestStream())
                {
                    postParams.Write(data, 0, data.Length);
                    using (var response = requestIndex.GetResponse())
                    {
                        using (var rStream = response.GetResponseStream())
                        {
                            string r = xmlEncoding.GetString(ReadFully(rStream));
                        }
                    }
                }

            }


            HttpWebRequest requestCommit1 = WebRequest.Create("http://localhost:8080/apache-solr-1.4.0/update?stream.body=%3Ccommit/%3E") as HttpWebRequest;
            using (HttpWebResponse response = requestCommit1.GetResponse() as HttpWebResponse)
            {


            }


        }
        catch (Exception ex)
        {
            if (ex.Message.ToString() == "Unable to connect to the remote server")
            {
                System.Diagnostics.Process procTomcat = new System.Diagnostics.Process();
                procTomcat.EnableRaisingEvents = false;
                procTomcat.StartInfo.FileName = Path.Combine(HttpRuntime.AppDomainAppPath, "tomcat-solr\\tomcatStart.bat");
                procTomcat.Start();
            }
        }

    }

    public static byte[] ReadFully(Stream stream)
    {
        byte[] buffer = new byte[32768];
        using (MemoryStream ms = new MemoryStream())
        {
            while (true)
            {
                int read = stream.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                    return ms.ToArray();
                ms.Write(buffer, 0, read);
            }
        }
    }

        
}





