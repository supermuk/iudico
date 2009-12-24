<%@ Application Language="C#" %>
<%@ Import Namespace="IUDICO.DataModel.Common"%>
<%@ Import Namespace="LEX.CONTROLS"%>
<%@ Import Namespace="App_Code"%>
<%@ Import Namespace="IUDICO.DataModel"%>
<%@ Import Namespace="System.Web.Configuration"%>

<script runat="server">
    
    void Application_Start(object sender, EventArgs e) 
    {
        ServerModel.Initialize(WebConfigurationManager.ConnectionStrings["IUDICO"].ConnectionString, HttpRuntime.Cache);
        PagesReg.RegisterPages(ServerModel.Forms);
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

    void Session_Start(object sender, EventArgs e) 
    {
        
    }

    void Session_End(object sender, EventArgs e) 
    {
    } 
</script>
