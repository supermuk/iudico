<%@ Application Language="C#" %>
<%@ Import Namespace="IUDICO.DataModel.Common"%>
<%@ Import Namespace="LEX.CONTROLS"%>
<%@ Import Namespace="App_Code"%>
<%@ Import Namespace="IUDICO.DataModel"%>
<%@ Import Namespace="System.Web.Configuration"%>
<%@ Import Namespace="System.Net"%>
<%@ Import Namespace="System.Windows.Forms"%>
<%@ Import Namespace="System"%>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import Namespace="System.Linq"%>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ Import Namespace="System.Security.Principal"%>
<%@ Import Namespace="System.Threading"%>
<%@ Import Namespace="System.Diagnostics"%>
<%@ Import Namespace="System.Collections"%>
<%@ Import Namespace="System.ComponentModel"%>
<%@ Import Namespace="System.Web"%>
<%@ Import Namespace="System.Web.Caching"%>
<%@ Import Namespace="System.IO"%>
<%@ Import Namespace="System.Web.UI"%>
<%@ Import Namespace="System.Text"%>
<%@ Import Namespace="IUDICO.DataModel"%>
<%@ Import Namespace="IUDICO.DataModel.Controllers.Student"%>
<%@ Import Namespace="IUDICO.DataModel.DB"%>
<%@ Import Namespace="System.Xml"%>
<%@ Import Namespace="IUDICO.DataModel.ImportManagers"%>
<%@ Import Namespace="mshtml"%>

<script runat="server">
    
    private const string DummyPageUrl = "http://localhost:2935/WebForm1.aspx";
    private const string DummyCacheItemKey = "GGG";
    private string ApplicationPath;
    private Dictionary<string, DateTime> activity;
    
    void Application_Start(object sender, EventArgs e) 
    {
        ApplicationPath = HttpRuntime.AppDomainAppPath;

        try
        {

            WriteStartTomcat();
            System.Diagnostics.Process procTomcat = new System.Diagnostics.Process();
            procTomcat.EnableRaisingEvents = false;
            procTomcat.StartInfo.FileName = Path.Combine(ApplicationPath, "tomcat-solr\\tomcatStart.bat");
            procTomcat.Start();
        }
        catch (Exception ex)
        { }
        
        ServerModel.Initialize(WebConfigurationManager.ConnectionStrings["IUDICO"].ConnectionString, HttpRuntime.Cache);
        PagesReg.RegisterPages(ServerModel.Forms);
        activity = new Dictionary<string, DateTime>();
        Application["activityList"] = activity;

        RegisterCacheEntry();
    } 
        
    void Application_End(object sender, EventArgs e) 
    {
        ServerModel.UnInitialize();      
        
    }
        
    void Application_Error(object sender, EventArgs ea)
    {
        Exception e = Server.GetLastError();
        var es = new StringBuilder("EXCEPTION: ");
        while (e != null)
        {
            es.AppendLine(e.Message);
            var sEx = e as SqlCommandException;
            if (sEx != null)
            {
                es.AppendLine("SQL: " + sEx.Sql);
                e = sEx.SqlException;
            }
            else
            {
                es.AppendLine("STACK: " + e.StackTrace);
            }
            e = e.InnerException;
            if (e != null)
            {
                es.Append("INNER EXCEPTION: ");
            }
        }
        Logger.WriteLine(es.ToString());
    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        // If the dummy page is hit, then it means we want to add another item
        // in cache
        if (HttpContext.Current.Request.Url.ToString() == DummyPageUrl)
        {
            // Add the item in cache and when succesful, do the work.
            RegisterCacheEntry();
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        
    }

    void Session_End(object sender, EventArgs e) 
    {
    }

    private void RegisterCacheEntry()
    {
        // Prevent duplicate key addition
        if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return;

        
        HttpContext.Current.Cache.Add(DummyCacheItemKey, "Index", null, DateTime.MaxValue,
            TimeSpan.FromMinutes(1440), CacheItemPriority.NotRemovable,
            new CacheItemRemovedCallback(CacheItemRemovedCallback));
    }

    public void CacheItemRemovedCallback(
            string key,
            object value,
            CacheItemRemovedReason reason
            )
    {
        // Index data
        DoWork();
        
        HitPage();
    }
    
    private void HitPage()
    {
        try
        {
            WebClient client = new WebClient();
            client.DownloadData(DummyPageUrl);
        }
        catch (Exception ex)
        {

            MessageBox.Show(ex.Message.ToString());
        }
    }


    private void DoWork()
    {
        IndexData index = new IndexData();
    }

    private void WriteStartTomcat()
    {
        try
        {
            string setCatalina = "set CATALINA_HOME = " + ApplicationPath + "tomcat-solr\\apache-tomcat-5.5.28\r\n";
            string setJava = "set JAVA_HOME = C:\\Program Files\\Java\\jdk1.6.0\r\n";
            string command = "cd " + ApplicationPath + "tomcat-solr\\solr\r\n" + ApplicationPath + "tomcat-solr\\apache-tomcat-5.5.28\\bin\\startup.bat";

            System.IO.StreamWriter file = new System.IO.StreamWriter(ApplicationPath + "tomcat-solr\\tomcatStart.bat");
            file.WriteLine(setCatalina + setJava + command);

            file.Close();
        }
        catch (Exception ex)
        {

        }
    }

</script>
